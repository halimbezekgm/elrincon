using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using elrincon.web.Manager;
using elrincon.web.Models.FirmaDisiKayitlar.Rehber;
using elrincon.web.Models.SharedModel;

namespace elrincon.web.Controllers.FirmaDisiKayitlar.Rehber
{
    public class RehberDisEditorController : RehberDisBaseController
    {
        // GET: RehberEditor
        public ActionResult Index(int? id)
        {
            RehberDisEditorModel modelEditor =  FillModel(id,"");
            modelEditor.Id = id;

            return View("~/Views/FirmaDisiKayitlar/Rehber/RehberDisEditor.cshtml", modelEditor);
        }
        
        public JsonResult SaveOrUpdate(RehberDisSaveModel model)
        {
            ElResult result = ControlResult(model);

            if (result.HasError())
                return Json(result, JsonRequestBehavior.AllowGet);

            ElDbTable table = GetTable("el_rehber");
            ElDbTable tableYuzdelik = GetTable("el_kardan_yuzdelik_oranlari_rehber"); 
            
            try
            {
                table.BeginTransaction();
                tableYuzdelik.BeginTransaction(); 

                RowBufferModel bufferModel = new RowBufferModel();
                bufferModel.Add("adi", CHelper.ToString(model.Ad));
                bufferModel.Add("soyadi", CHelper.ToString(model.Soyad)); 
                bufferModel.Add("subesi", CHelper.ToInt32(model.Subesi)); 
                bufferModel.Add("tel_no1", CHelper.ToString(model.TelNo1));
                bufferModel.Add("tel_no2", CHelper.ToString(model.TelNo2));
                bufferModel.Add("mail_adres", CHelper.ToString(model.MailAdresi));
                bufferModel.Add("adres", CHelper.ToString(model.Adres)); 
                bufferModel.Add("muhasebe_kodu", CHelper.ToInt32(model.MuhasebeKodu)); 
                bufferModel.Add("doviz_kodu", CHelper.ToInt32(model.DovizKodu));
                bufferModel.Add("fax_no", CHelper.ToString(model.InputFaxModel));
                bufferModel.Add("posta_kodu", CHelper.ToString(model.InputPostaKoduModel));
                bufferModel.Add("ulke", CHelper.ToString(model.SelectUlkeModel));
                bufferModel.Add("aciklama", CHelper.ToString(model.Aciklama));

                int? perid = model.Id;
                if (!model.Id.HasValue || model.Id.Value == 0)
                   perid = table.InsertTable(bufferModel);
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
         

        private void InsertKardanYuzdelikOranlari(RehberDisSaveModel model ,ElDbTable tableYuzdelik)
        {
            tableYuzdelik.Delete("rehber_id", model.Id);

            foreach (KardanYuzdelikOraniSaveModel yuzdelikOraniModel in model.SaveYuzdelikOraniModels)
            {
                if(!yuzdelikOraniModel.UrunTipi.HasValue)
                    continue;
                
                RowBufferModel bufferModel = new RowBufferModel();
                bufferModel.Add("rehber_id", CHelper.ToInt32(model.Id));
                bufferModel.Add("urunTipi", CHelper.ToInt32(yuzdelikOraniModel.UrunTipi));
                bufferModel.Add("yuzdelik_tip", CHelper.ToInt32(yuzdelikOraniModel.YuzdeTip)); 
                bufferModel.Add("normalSatisYuzdelik", CHelper.ToDouble(yuzdelikOraniModel.NarmalSatisYuzdeligi)); 
                bufferModel.Add("muhasebe_kodu", CHelper.ToInt32(yuzdelikOraniModel.MuhasebeKodu));

                tableYuzdelik.InsertTable(bufferModel);
            }
        }

        private ElResult ControlResult(RehberDisSaveModel model)
        {
            ElResult result = new ElResult();
            
            if (string.IsNullOrWhiteSpace(model.Ad))
                result.AddMessage("Rehber Adı Boş Olamaz.");
            
            if (string.IsNullOrWhiteSpace(model.Soyad))
                result.AddMessage("Rehber Soyadı Boş Olamaz.");

            if (string.IsNullOrWhiteSpace(model.TelNo1))
                result.AddMessage("Telefon No1 Boş Olamaz.");

            if (!model.Subesi.HasValue)
                result.AddMessage("Şubesi Boş Olamaz.");

            if (!string.IsNullOrWhiteSpace(model.MailAdresi) && ControlSameRecord("el_rehber", "mail_adres",model.MailAdresi,model.Id)) 
                result.AddMessage("Bu mail adresi daha önce kullanılmış! ");
            
            if (!string.IsNullOrWhiteSpace(result.message))
                result.SetError();
             
            if (model.MuhasebeKodu.HasValue && ControlSameRecord("el_rehber", "muhasebe_kodu", model.MuhasebeKodu.ToString(),model.Id)) 
                result.AddMessage("Bu Muhasebe kodu daha önce kullanılmış! ");
             
            if (!string.IsNullOrWhiteSpace(result.message))
                result.SetError();

            return result;
        }
    }
}