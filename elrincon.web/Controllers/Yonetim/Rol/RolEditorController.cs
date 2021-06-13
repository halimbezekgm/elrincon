using elrincon.web.Manager;
using elrincon.web.Models.Yonetim.Rol;
using elrincon.web.Models.SharedModel;
using System;
using System.Data;
using System.Web.Mvc;

namespace elrincon.web.Controllers.Yonetim.Rol
{
    public class RolEditorController : BaseController
    {
        // GET: RolEditor
        public ActionResult Index(int id)
        {
            RolEditorModel model = FillEditorModel(id);
            return View("~/Views/Yonetim/Rol/RolEditor.cshtml", model);
        }

        private RolEditorModel FillEditorModel(int id)
        {
            RolEditorModel model = new RolEditorModel();

            ElInputModel modelRolAdi = new ElInputModel(InputDataType.Text);
            modelRolAdi.Id = "rol_editor_input_rol_adi";
            modelRolAdi.IsForQuery = true;
            model.InputRolAdiModel = modelRolAdi;

            ElInputModel modelRolAciklama = new ElInputModel(InputDataType.Text);
            modelRolAciklama.Id = "rol_editor_input_rol_aciklama";
            modelRolAciklama.IsForQuery = true;
            model.InputRolAciklamaModel = modelRolAciklama;

            ElSelectModel modelRolKart = GetDomainList(26);
            modelRolKart.Id = "rol_editor_select_rol_kart"; 
            model.SelectRolKarti = modelRolKart;

            model.RolId = id;

            if (id != 0)
            {
                ElDbTable table = GetTableRow(id);
                DataRow row = table.Rows[0];
                model.InputRolAdiModel.Value = row["rol_adi"];
                model.InputRolAciklamaModel.Value = row["rol_aciklama"];
                model.SelectRolKarti.SetSelectedCode(CHelper.ToInt32(row["rol_karti"]));
            }

            return model;
        }
        private ElDbTable GetTableRow(int id)
        {
            string sql = "select * from el_roller where id = " + id;
            ElDbTable table = ExecuteTable(sql);
            return table;
        }


        public JsonResult SaveOrUpdate(int rolId, string rolAdi, string rolAciklama, int? rolKartId)
        {
            ElResult result = ControlResult(rolAdi, rolAciklama, rolKartId);

            if (result.HasError())
                return Json(result, JsonRequestBehavior.AllowGet);

            ElDbTable table = GetTable("el_roller");

            try
            {
                RowBufferModel bufferModel = new RowBufferModel();
                bufferModel.Add("rol_adi", CHelper.ToString(rolAdi));
                bufferModel.Add("rol_aciklama", CHelper.ToString(rolAciklama));
                bufferModel.Add("rol_karti", CHelper.ToInt32(rolKartId));

                if (rolId == 0)
                    table.InsertTable(bufferModel);
                else
                    table.UpdateTable(bufferModel, rolId);

                result.SetOk();
            }
            catch (Exception e)
            {
                result.AddErrorMessage("hata : " + e.Message);
                result.SetError();
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private ElResult ControlResult(string rolAdi, string rolAciklama, int? rolKartId)
        {
            ElResult result = new ElResult();

            if (string.IsNullOrWhiteSpace(rolAdi))
            {
                result.AddMessage("Rol Adı Boş Olamaz.");
                result.SetError();
            }

            if (string.IsNullOrWhiteSpace(rolAciklama))
            {
                result.AddMessage("Rol Açıklama Boş Olamaz.");
                result.SetError();
            }

            if (!rolKartId.HasValue || rolKartId == 0)
            {
                result.AddMessage("Rol Açıklama Boş Olamaz.");
                result.SetError();
            }

            return result;
        }
    }
}