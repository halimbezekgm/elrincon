using System;
using System.Web.Mvc;
using elrincon.web.Manager;
using elrincon.web.Models.KasaTakibi.FisGirisi;
using elrincon.web.Models.SharedModel;

namespace elrincon.web.Controllers.KasaTakibi.FisGirisi
{
    public class FisGirisiEditorController : FisGirisiBaseController
    {
        // GET: Fiş GirisiEditor
        public ActionResult Index(int? id)
        {
            FisGirisiEditorModel modelEditor =  FillModel(id,"");
            modelEditor.Id = id;

            return View("~/Views/KasaTakibi/FisGirisi/FisGirisiEditor.cshtml", modelEditor);
        }
        
        public JsonResult SaveOrUpdate(FisGirisiSaveModel model)
        {
            ElResult result = ControlResult(model);

            if (result.HasError())
                return Json(result, JsonRequestBehavior.AllowGet);

            ElDbTable table = GetTable("el_fis_girisi");
            ElDbTable tableYuzdelik = GetTable("el_kardan_yuzdelik_oranlari_fis_girisi"); 
            
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
         

        private void InsertKardanYuzdelikOranlari(FisGirisiSaveModel model ,ElDbTable tableYuzdelik)
        {
            tableYuzdelik.Delete("fis_girisi_id", model.Id);

            foreach (KardanYuzdelikOraniSaveModel yuzdelikOraniModel in model.SaveYuzdelikOraniModels)
            {
                if(!yuzdelikOraniModel.UrunTipi.HasValue)
                    continue;
                
                RowBufferModel bufferModel = new RowBufferModel();
                bufferModel.Add("fis_girisi_id", CHelper.ToInt32(model.Id));
                bufferModel.Add("urunTipi", CHelper.ToInt32(yuzdelikOraniModel.UrunTipi));
                bufferModel.Add("yuzdelik_tip", CHelper.ToInt32(yuzdelikOraniModel.YuzdeTip)); 
                bufferModel.Add("normalSatisYuzdelik", CHelper.ToDouble(yuzdelikOraniModel.NarmalSatisYuzdeligi)); 
                bufferModel.Add("muhasebe_kodu", CHelper.ToInt32(yuzdelikOraniModel.MuhasebeKodu));

                tableYuzdelik.InsertTable(bufferModel);
            }
        }

        private ElResult ControlResult(FisGirisiSaveModel model)
        {
            ElResult result = new ElResult();
            
            if (string.IsNullOrWhiteSpace(model.Ad))
                result.AddMessage("Bir hata oluştu daha sonra tekrar deneyiniz.");
             
            if (!model.Subesi.HasValue)
                result.AddMessage("Şubesi Boş Olamaz.");

            if (!string.IsNullOrWhiteSpace(model.MailAdresi) && ControlSameRecord("el_fis_girisi", "mail_adres",model.MailAdresi,model.Id)) 
                result.AddMessage("Bu mail adresi daha önce kullanılmış! ");
            
            if (!string.IsNullOrWhiteSpace(result.message))
                result.SetError();
             
            if (model.MuhasebeKodu.HasValue && ControlSameRecord("el_fis_girisi", "muhasebe_kodu", model.MuhasebeKodu.ToString(),model.Id)) 
                result.AddMessage("Bu Muhasebe kodu daha önce kullanılmış! ");
             
            if (!string.IsNullOrWhiteSpace(result.message))
                result.SetError();

            return result;
        }
    }
}