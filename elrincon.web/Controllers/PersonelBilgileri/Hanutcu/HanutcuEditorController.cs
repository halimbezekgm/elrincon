using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using elrincon.web.Manager;
using elrincon.web.Models.PersonelBilgileri.Hanutcu; 
using elrincon.web.Models.SharedModel; 

namespace elrincon.web.Controllers.PersonelBilgileri.Hanutcu
{
    public class HanutcuEditorController : HanutcuBaseController
    {
        // GET: HanutcuEditor
        public ActionResult Index(int? id)
        {
            HanutcuEditorModel modelEditor =  FillModel(id,null);
            modelEditor.Id = id;

            return View("~/Views/PersonelBilgileri/Hanutcu/HanutcuEditor.cshtml", modelEditor);
        }


        public JsonResult SaveOrUpdate(HanutcuSaveModel model)
        {
            ElResult result = ControlResult(model);

            if (result.HasError())
                return Json(result, JsonRequestBehavior.AllowGet);

            ElDbTable table = GetTable("el_personel");
            ElDbTable tableYuzdelik = GetTable("el_kardan_yuzdelik_oranlari");
            
            try
            {
                table.BeginTransaction();
                tableYuzdelik.BeginTransaction();

                RowBufferModel bufferModel = new RowBufferModel();
                bufferModel.Add("adi", CHelper.ToString(model.Ad));
                bufferModel.Add("soyadi", CHelper.ToString(model.Soyad));
                bufferModel.Add("kullanici_adi", CHelper.ToString(model.KullaniciAdi));
                bufferModel.Add("sifre", CHelper.ToString(model.Sifre));
                bufferModel.Add("bolum", CHelper.ToInt32(model.Bolumu));
                bufferModel.Add("subesi", CHelper.ToInt32(model.Subesi));
                bufferModel.Add("tc_no", CHelper.ToString(model.TcNo));
                bufferModel.Add("tel_no1", CHelper.ToString(model.TelNo1));
                bufferModel.Add("tel_no2", CHelper.ToString(model.TelNo2));
                bufferModel.Add("mail_adres", CHelper.ToString(model.MailAdresi));
                bufferModel.Add("adres", CHelper.ToString(model.Adres));
                bufferModel.Add("muhasebe_maas_gider_hesabi", CHelper.ToInt32(model.MuhasebeMaasGiderHesabi));
                bufferModel.Add("muhasebe_kodu", CHelper.ToInt32(model.MuhasebeKodu));
                bufferModel.Add("maas_tipi", CHelper.ToInt32(model.MaasTipi));
                bufferModel.Add("maas_miktari", CHelper.ToDouble(model.MaasMiktari));
                bufferModel.Add("doviz_kodu", CHelper.ToInt32(model.DovizKodu));
                bufferModel.Add("aciklama", CHelper.ToString(model.Aciklama));
                bufferModel.Add("ise_giris_tarihi", model.IseGirisTarihi); 
                bufferModel.Add("personel_tipi", 2); //hanutcu
                 
                if (!model.Id.HasValue || model.Id.Value == 0)
                    table.InsertTable (bufferModel);
                else
                    table.UpdateTable(bufferModel, model.Id.Value);

                InsertKardanYuzdelikOranlari(model,tableYuzdelik);

                table.CommitTransaction();
                tableYuzdelik.CommitTransaction();

                result.SetOk();
            }
            catch (Exception e)
            {
                table.RoolbackTransaction();
                tableYuzdelik.RoolbackTransaction();

                result.AddErrorMessage("hata : " + e.Message);
                result.SetError();
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private void InsertKardanYuzdelikOranlari(HanutcuSaveModel model ,ElDbTable tableYuzdelik)
        {
            tableYuzdelik.Delete("personel_id", model.Id);

            foreach (KardanYuzdelikOraniSaveModel yuzdelikOraniModel in model.SaveYuzdelikOraniModels)
            {
                if(!yuzdelikOraniModel.UrunTipi.HasValue)
                    continue;
                
                RowBufferModel bufferModel = new RowBufferModel();
                bufferModel.Add("personel_id", CHelper.ToInt32(model.Id));
                bufferModel.Add("yuzdelik_tip", CHelper.ToInt32(yuzdelikOraniModel.YuzdeTip));
                bufferModel.Add("urunTipi", CHelper.ToInt32(yuzdelikOraniModel.UrunTipi));
                bufferModel.Add("normalSatisParite1", CHelper.ToDouble(yuzdelikOraniModel.NarmalSatisParite1));
                bufferModel.Add("normalSatisParite2", CHelper.ToDouble(yuzdelikOraniModel.NarmalSatisParite2));
                bufferModel.Add("normalSatisYuzdelik", CHelper.ToDouble(yuzdelikOraniModel.NarmalSatisYuzdeligi));
                bufferModel.Add("kapiSatisParite1", CHelper.ToDouble(yuzdelikOraniModel.KapiSatisParite1));
                bufferModel.Add("kapiSatisParite2", CHelper.ToDouble(yuzdelikOraniModel.KapiSatisParite2));
                bufferModel.Add("kapiSatisYuzdelik", CHelper.ToDouble(yuzdelikOraniModel.KapiSatisYuzdeligi));
                bufferModel.Add("internetSatisParite1", CHelper.ToDouble(yuzdelikOraniModel.InternetSatisParite1));
                bufferModel.Add("internetSatisParite2", CHelper.ToDouble(yuzdelikOraniModel.InternetSatisParite2));
                bufferModel.Add("internetSatisYuzdelik", CHelper.ToDouble(yuzdelikOraniModel.InternetSatisYuzdeligi));
                bufferModel.Add("yurtDisiSatisParite1", CHelper.ToDouble(yuzdelikOraniModel.YurtDisiSatisParite1));
                bufferModel.Add("yurtDisiSatisParite2", CHelper.ToDouble(yuzdelikOraniModel.YurtDisiSatisParite2));
                bufferModel.Add("yurtDisiSatisYuzdelik", CHelper.ToDouble(yuzdelikOraniModel.YurtDisiSatisYuzdeligi));
                bufferModel.Add("genelSatisParite1", CHelper.ToDouble(yuzdelikOraniModel.GenelCiroSatisParite1));
                bufferModel.Add("genelSatisParite2", CHelper.ToDouble(yuzdelikOraniModel.GenelCiroSatisParite2));
                bufferModel.Add("genelSatisYuzdelik", CHelper.ToDouble(yuzdelikOraniModel.GenelCiroSatisYuzdeligi));
                bufferModel.Add("muhasebe_kodu", CHelper.ToInt32(yuzdelikOraniModel.MuhasebeKodu));
                tableYuzdelik.InsertTable(bufferModel);
            }
        }

        private ElResult ControlResult(HanutcuSaveModel model)
        {
            ElResult result = new ElResult();
            
            if (string.IsNullOrWhiteSpace(model.Ad))
                result.AddMessage("Personel Adı Boş Olamaz.");
            
            if (string.IsNullOrWhiteSpace(model.Soyad))
                result.AddMessage("Personel Soyadı Boş Olamaz.");

            if (string.IsNullOrWhiteSpace(model.TelNo1))
                result.AddMessage("Telefon No1 Boş Olamaz.");

            if (!model.Subesi.HasValue)
                result.AddMessage("Şubesi Boş Olamaz.");

            if (!model.IseGirisTarihi.HasValue)
                result.AddMessage("İşe giriş tarihi boş olamaz.");

            if (!model.MuhasebeMaasGiderHesabi.HasValue)
                result.AddMessage("Muhasebe gider hesabını doldurunuz. ");

            if (!string.IsNullOrWhiteSpace(result.message))
                result.SetError();

            if (!string.IsNullOrWhiteSpace(model.KullaniciAdi) && ControlSameRecord("el_personel", "kullanici_adi", model.KullaniciAdi, model.Id))
                result.AddMessage("Bu kullanıcı adı daha önce kullanılmış! ");

            if (!string.IsNullOrWhiteSpace(model.MailAdresi) && ControlSameRecord("el_personel", "mail_adres", model.MailAdresi, model.Id))
                result.AddMessage("Bu mail adresi daha önce kullanılmış! ");

            if (!string.IsNullOrWhiteSpace(model.TcNo) && ControlSameRecord("el_personel", "tc_no", model.TcNo, model.Id))
                result.AddMessage("Bu TC Numarası daha önce kullanılmış! ");

            if (model.MuhasebeMaasGiderHesabi.HasValue && ControlSameRecord("el_personel", "muhasebe_maas_gider_hesabi", model.MuhasebeMaasGiderHesabi.Value.ToString(), model.Id))
                result.AddMessage("Bu Muhasebe maaş gider hesabı daha önce kullanılmış! ");

            if (model.MuhasebeKodu.HasValue && ControlSameRecord("el_personel", "muhasebe_kodu", model.MuhasebeKodu.Value.ToString(), model.Id))
                result.AddMessage("Bu Muhasebe kodu daha önce kullanılmış! ");

            if (!string.IsNullOrWhiteSpace(result.message))
                result.SetError();

            return result;
        }
    }
}