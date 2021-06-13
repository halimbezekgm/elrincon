using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using elrincon.web.Manager;
using elrincon.web.Models.FirmaDisiKayitlar.Acenta;
using elrincon.web.Models.SharedModel;

namespace elrincon.web.Controllers.FirmaDisiKayitlar.Acenta
{
    public class AcentaEditorController : AcentaBaseController
    {
        // GET: AcentaEditor
        public ActionResult Index(int? id)
        {
            AcentaEditorModel modelEditor =  FillModel(id,"");
            modelEditor.Id = id;

            return View("~/Views/FirmaDisiKayitlar/Acenta/AcentaEditor.cshtml", modelEditor);
        }

        public JsonResult SaveOrUpdate(AcentaSaveModel model)
        {
            ElResult result = ControlResult(model);

            if (result.HasError())
                return Json(result, JsonRequestBehavior.AllowGet);

            ElDbTable table = GetTable("el_acenta");
            ElDbTable tableYuzdelik = GetTable("el_kardan_yuzdelik_oranlari_acenta");

            try
            {
                table.BeginTransaction();
                tableYuzdelik.BeginTransaction();

                RowBufferModel bufferModel = new RowBufferModel();
                bufferModel.Add("adi", CHelper.ToString(model.Ad));
                bufferModel.Add("sehir", CHelper.ToString(model.Sehir));
                bufferModel.Add("subesi", CHelper.ToInt32(model.Subesi));
                bufferModel.Add("tel_no1", CHelper.ToString(model.TelNo1));
                bufferModel.Add("tel_no2", CHelper.ToString(model.TelNo2));
                bufferModel.Add("ilgili_kisi1", CHelper.ToString(model.IlgiliKisi1));
                bufferModel.Add("ilgili_kisi2", CHelper.ToString(model.IlgiliKisi2));
                bufferModel.Add("mail_adres", CHelper.ToString(model.MailAdresi));
                bufferModel.Add("adres", CHelper.ToString(model.Adres));
                bufferModel.Add("muhasebe_kodu", CHelper.ToString(model.MuhasebeKodu));
                bufferModel.Add("doviz_kodu", CHelper.ToInt32(model.DovizKodu));
                bufferModel.Add("fax_no", CHelper.ToString(model.InputFaxModel));
                bufferModel.Add("posta_kodu", CHelper.ToString(model.InputPostaKoduModel));
                bufferModel.Add("ulke", CHelper.ToInt32(model.SelectUlkeModel));
                bufferModel.Add("aciklama", CHelper.ToString(model.Aciklama));

                int? perid = model.Id;
                if (!model.Id.HasValue || model.Id.Value == 0)
                    perid = table.InsertTable(bufferModel);
                else
                    table.UpdateTable(bufferModel, model.Id.Value);

                InsertKardanYuzdelikOranlari(model, tableYuzdelik);

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
         
        private void InsertKardanYuzdelikOranlari(AcentaSaveModel model, ElDbTable tableYuzdelik)
        {
            tableYuzdelik.Delete("acenta_id", model.Id);

            foreach (KardanYuzdelikOraniSaveModel yuzdelikOraniModel in model.SaveYuzdelikOraniModels)
            {
                if (!yuzdelikOraniModel.UrunTipi.HasValue)
                    continue;

                RowBufferModel bufferModel = new RowBufferModel();
                bufferModel.Add("acenta_id", CHelper.ToInt32(model.Id));
                bufferModel.Add("urunTipi", CHelper.ToInt32(yuzdelikOraniModel.UrunTipi));
                bufferModel.Add("yuzdelik_tip", CHelper.ToInt32(yuzdelikOraniModel.YuzdeTip));
                //bufferModel.Add("normalSatisParite1", CHelper.ToDouble(yuzdelikOraniModel.NarmalSatisParite1));
                //bufferModel.Add("normalSatisParite2", CHelper.ToDouble(yuzdelikOraniModel.NarmalSatisParite2));
                bufferModel.Add("normalSatisYuzdelik", CHelper.ToDouble(yuzdelikOraniModel.NarmalSatisYuzdeligi));
                //bufferModel.Add("kapiSatisParite1", CHelper.ToDouble(yuzdelikOraniModel.KapiSatisParite1));
                //bufferModel.Add("kapiSatisParite2", CHelper.ToDouble(yuzdelikOraniModel.KapiSatisParite2));
                //bufferModel.Add("kapiSatisYuzdelik", CHelper.ToDouble(yuzdelikOraniModel.KapiSatisYuzdeligi));
                //bufferModel.Add("internetSatisParite1", CHelper.ToDouble(yuzdelikOraniModel.InternetSatisParite1));
                //bufferModel.Add("internetSatisParite2", CHelper.ToDouble(yuzdelikOraniModel.InternetSatisParite2));
                //bufferModel.Add("internetSatisYuzdelik", CHelper.ToDouble(yuzdelikOraniModel.InternetSatisYuzdeligi));
                //bufferModel.Add("yurtDisiSatisParite1", CHelper.ToDouble(yuzdelikOraniModel.YurtDisiSatisParite1));
                //bufferModel.Add("yurtDisiSatisParite2", CHelper.ToDouble(yuzdelikOraniModel.YurtDisiSatisParite2));
                //bufferModel.Add("yurtDisiSatisYuzdelik", CHelper.ToDouble(yuzdelikOraniModel.YurtDisiSatisYuzdeligi));
                //bufferModel.Add("genelSatisParite1", CHelper.ToDouble(yuzdelikOraniModel.GenelCiroSatisParite1));
                //bufferModel.Add("genelSatisParite2", CHelper.ToDouble(yuzdelikOraniModel.GenelCiroSatisParite2));
                //bufferModel.Add("genelSatisYuzdelik", CHelper.ToDouble(yuzdelikOraniModel.GenelCiroSatisYuzdeligi));

                tableYuzdelik.InsertTable(bufferModel);
            }
        }

        private ElResult ControlResult(AcentaSaveModel model)
        {
            ElResult result = new ElResult();

            if (string.IsNullOrWhiteSpace(model.Ad))
                result.AddMessage("Acente Adı Boş Olamaz.");

            if (string.IsNullOrWhiteSpace(model.Sehir))
                result.AddMessage("Acente Şehir Bilgisi Boş Olamaz.");

            if (string.IsNullOrWhiteSpace(model.TelNo1))
                result.AddMessage("Telefon No1 Boş Olamaz.");

            if (!model.Subesi.HasValue)
                result.AddMessage("Şubesi Boş Olamaz.");

            if (!string.IsNullOrWhiteSpace(model.MailAdresi) && ControlSameRecord("el_acenta", "mail_adres", model.MailAdresi, model.Id))
                result.AddMessage("Bu mail adresi daha önce kullanılmış! ");

            if (!string.IsNullOrWhiteSpace(result.message))
                result.SetError();

            if (!string.IsNullOrWhiteSpace(model.MuhasebeKodu) && ControlSameRecord("el_acenta", "muhasebe_kodu", model.MuhasebeKodu, model.Id))
                result.AddMessage("Bu Muhasebe kodu daha önce kullanılmış! ");

            if (!string.IsNullOrWhiteSpace(result.message))
                result.SetError();

            return result;
        }
    }
}