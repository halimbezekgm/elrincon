using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using elrincon.web.Manager;
using elrincon.web.Models.PersonelBilgileri.Personel;
using elrincon.web.Models.SharedModel;
using elrincon.web.Models.Yonetim.Rezervasyon;

namespace elrincon.web.Controllers.Yonetim.Rezervasyon
{
    public class RezervasyonEditorController : RezervasyonBaseController
    {
        // GET: PersonelEditor
        public ActionResult Index(int? id)
        {
            RezervasyonEditorModel modelEditor =  FillModel(id,"");
            modelEditor.Id = id;

            return View("~/Views/Yonetim/Rezervasyon/rezervasyonEditor.cshtml", modelEditor);
        }
        
        public JsonResult SaveOrUpdate(RezervasyonSaveModel model)
        {
            ElResult result = ControlResult(model);

            if (result.HasError())
                return Json(result, JsonRequestBehavior.AllowGet);

            ElDbTable table = GetTable("el_rezervasyon"); 
            
            try
            { 
                RowBufferModel bufferModel = new RowBufferModel();
                bufferModel.Add("tarih", model.InputTarihModel.Value.Date.ToString("yyyy-MM-dd"));
                bufferModel.Add("rezervasyon_no", CHelper.ToString(model.InputRezeryonNoModel));
                bufferModel.Add("acenta", CHelper.ToInt32(model.InputAcentaModel));
                bufferModel.Add("rehber_1", CHelper.ToInt32(model.Rehber1Model));
                bufferModel.Add("rehber_2", CHelper.ToInt32(model.Rehber2Model));
                bufferModel.Add("hanutcu_1", CHelper.ToInt32(model.Hanutcu1Model));
                bufferModel.Add("hanutcu_2", CHelper.ToInt32(model.Hanutcu2Model));
                bufferModel.Add("servis", CHelper.ToInt32(model.InputServisPersoneliModel));
                bufferModel.Add("kisi_sayisi", CHelper.ToInt32(model.InputKisiSayisiModel));
                bufferModel.Add("ulkesi", CHelper.ToInt32(model.InputUlkesiModel));
                bufferModel.Add("aciklama", CHelper.ToString(model.InputAciklamaModel));

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
         
        private ElResult ControlResult(RezervasyonSaveModel model)
        {
            ElResult result = new ElResult();
            
            if (!model.InputTarihModel.HasValue)
                result.AddMessage("Rezervasyon Tarihi Boş Olamaz.");
            
            if (string.IsNullOrWhiteSpace(model.InputRezeryonNoModel))
                result.AddMessage("Reazervasyon No Boş Olamaz.");

            if (!model.InputAcentaModel.HasValue)
                result.AddMessage("Acente Boş Olamaz.");

            if (!model.Rehber1Model.HasValue)
                result.AddMessage("Rehber 1 Boş Olamaz.");
             
            if (!string.IsNullOrWhiteSpace(result.message))
                result.SetError();

            return result;
        }
    }
}