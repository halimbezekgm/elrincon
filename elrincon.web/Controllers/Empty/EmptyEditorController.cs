using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using elrincon.web.Manager;
using elrincon.web.Models.EmptyModel;
using elrincon.web.Models.SharedModel;

namespace elrincon.web.Controllers.Empty
{
    public class EmptyEditorController : BaseController
    {
        // GET: EmptyEditor
        public ActionResult Index(int? id)
        {
            EmtyEditorModel modelEditor =  FillModel(id);
            modelEditor.Id = id;

            return View("~/Views/Empty/EmptyEditor.cshtml", modelEditor);
        }

        private EmtyEditorModel FillModel(int? id)
        {
            EmtyEditorModel modelEditor = new EmtyEditorModel();
            ElInputModel inputModel = new ElInputModel(InputDataType.Text);
            inputModel.Id = "test_model_input";
            modelEditor.InputModel = inputModel;
            
            ElInputModel inputSifreModel = new ElInputModel(InputDataType.Text);
            inputSifreModel.Id = "test_model_input_sifre";
            modelEditor.InputSifreModel= inputSifreModel;

            if (id.HasValue && id > 0)
            {
                DataRow row = GetTableRow(id.Value);
                
                inputModel.Value = row["kullanici_adi"];
                inputSifreModel.Value = row["kullanici_sifre"];
            }
             
            return modelEditor;
        }

        private DataRow GetTableRow(int id)
        {
            ElDbTable table = GetTable("el_kullanicilar");
            DataRow row = table.Select("id="+id).First();

            return row;
        }

        public JsonResult SaveOrUpdate(int? id,string kullaniciAdi, string kullaniciSifre)
        {
            ElResult result = ControlResult(kullaniciAdi,kullaniciSifre);

            if (result.HasError())
                return Json(result, JsonRequestBehavior.AllowGet);

            ElDbTable table = GetTable("el_kullanicilar");
            try
            {
                RowBufferModel bufferModel = new RowBufferModel();
                bufferModel.Add("kullanici_adi",kullaniciAdi);
                bufferModel.Add("kullanici_sifre",kullaniciSifre);

                 if (!id.HasValue || id.Value == 0)
                     table.InsertTable(bufferModel);
                 else
                     table.UpdateTable(bufferModel, id.Value);

                 result.SetOk();
            }
            catch (Exception e)
            {
                result.AddErrorMessage("hata : " + e.Message);
                result.SetError();
            }
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private ElResult ControlResult(string kullaniciAdi, string kullaniciSifre)
        {
            ElResult result = new ElResult();
            
            if (string.IsNullOrWhiteSpace(kullaniciSifre))
                result.AddMessage("Kullanıcı Şifre Boş Olamaz.");

            if (string.IsNullOrWhiteSpace(kullaniciAdi))
                result.AddMessage("Kullanıcı Adı Boş Olamaz.");

            if(!string.IsNullOrWhiteSpace(result.message))
                result.SetError();

            return result;
        }
    }
}