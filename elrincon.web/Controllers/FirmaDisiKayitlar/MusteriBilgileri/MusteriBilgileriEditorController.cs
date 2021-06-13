using System;
using System.Web.Mvc; 
using elrincon.web.Manager;
using elrincon.web.Models.FirmaDisiKayitlar.MusteriBilgileri;
using elrincon.web.Models.SharedModel; 
namespace elrincon.web.Controllers.FirmaDisiKayitlar.MusteriBilgileri
{
    public class MusteriBilgileriEditorController : MusteriBilgileriBaseController
    {
        // GET: PersonelEditor
        public ActionResult Index(int? id)
        {
            MusteriBilgileriEditorModel modelEditor =  FillModel(id,"");
            modelEditor.Id = id;

            return View("~/Views/FirmaDisiKAyitlar/MusteriBilgileri/MusteriBilgileriEditor.cshtml", modelEditor);
        }
        
        public JsonResult SaveOrUpdate(MusteriBilgileriSaveModel model)
        {
            ElResult result = ControlResult(model);

            if (result.HasError())
                return Json(result, JsonRequestBehavior.AllowGet);

            ElDbTable table = GetTable("el_musteri_bilgileri"); 
            
            try
            { 
                RowBufferModel bufferModel = new RowBufferModel();
                bufferModel.Add("tarih", DateTime.Today.ToString("yyyy-MM-dd"));
                bufferModel.Add("adi", CHelper.ToString(model.InputAdModel));
                bufferModel.Add("soyadi", CHelper.ToString(model.InputSoyadModel));
                bufferModel.Add("adres", CHelper.ToString(model.InputAdresModel));
                bufferModel.Add("sehir", CHelper.ToString(model.InputSehirModel));
                bufferModel.Add("eyalet", CHelper.ToString(model.InputEyaletModel));
                bufferModel.Add("posta_kodu", CHelper.ToInt32(model.InputPostaKoduModel));
                bufferModel.Add("ulkesi", CHelper.ToInt32(model.SelectUlkesiModel));
                bufferModel.Add("vergi_no", CHelper.ToString(model.InputVergiNoModel));
                bufferModel.Add("tax_office", CHelper.ToString(model.InputTaxOfficeModel));
                bufferModel.Add("telefon", CHelper.ToString(model.InputTelefonModel));
                bufferModel.Add("mobil_tel", CHelper.ToString(model.InputTelefonCepModel));
                bufferModel.Add("fax", CHelper.ToString(model.InputFaxModel));
                bufferModel.Add("email", CHelper.ToString(model.InputEmailModel));

                int? perid = model.Id;
                if (!model.Id.HasValue || model.Id.Value == 0)
                   perid = table.InsertTable(bufferModel);
                else
                    table.UpdateTable(bufferModel, model.Id.Value); 
                 
                result.SetOk();
            }
            catch (Exception e)
            {  
                result.AddErrorMessage("hata : " + e.Message);
                result.SetError();
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
         
        private ElResult ControlResult(MusteriBilgileriSaveModel model)
        {
            ElResult result = new ElResult();
            
            if (string.IsNullOrWhiteSpace(model.InputAdModel))
                result.AddMessage("Musteri Adı Boş Olamaz.");
            
            if (string.IsNullOrWhiteSpace(model.InputSoyadModel))
                result.AddMessage("Müsteri Soyadı Boş Olamaz.");

            if (string.IsNullOrWhiteSpace(model.InputTelefonCepModel))
                result.AddMessage("Telefon No Boş Olamaz.");
             
            if (!string.IsNullOrWhiteSpace(result.message))
                result.SetError();

            return result;
        }
    }
}