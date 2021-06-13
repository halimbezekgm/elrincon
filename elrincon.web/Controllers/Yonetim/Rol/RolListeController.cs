using elrincon.web.Manager;
using elrincon.web.Models.SharedModel;
using elrincon.web.Models.SharedModel.GridModel;
using System;
using System.Web.Mvc;

namespace elrincon.web.Controllers.Yonetim.Rol
{
    public class RolListeController : BaseController
    {
        // GET: RolListe
        public ActionResult Index()
        {
            ElDataGridModel model = new ElDataGridModel();
            model = FillGridModel();
            return View("~/Views/Yonetim/Rol/RolListe.cshtml", model);
        }

        private ElDataGridModel FillGridModel()
        {
            ElDbTable dataTable = GetBilgiler();
            ElDataGridModel dataGridModel = new ElDataGridModel("rol_liste_grid_id");
            dataGridModel.GridTable = new DataGridTable(dataTable, "id");
            dataGridModel.GridParameter.CustomProperties = new CustomProperties();

            GridRow row = new GridRow("Rol Adı", "rol_adi", "30%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Rol Açıklama", "rol_aciklama", "30%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Kart Adı", "kart_adi", "30%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            ActionButton buttonEdit = new ActionButton("edit", @Url.Content("~/Content/images/edit.png"), "edit_rol_listesi_clicked");
            ActionButton buttonDelete = new ActionButton("edit", @Url.Content("~/Content/images/delete.png"), "delete_rol_listesi_clicked");

            dataGridModel.ActionButtons.Add(buttonEdit);
            dataGridModel.ActionButtons.Add(buttonDelete);
            dataGridModel.ActionButtonWith = "10%";
            dataGridModel.PageSize = 8;
            dataGridModel.DataUrl = Url.Action("GetGridBody");
            return dataGridModel;
        }

        public ActionResult GetGridBody(GridParameter parameter)
        {
            ElDataGridModel model = FillGridModel();

            if (!string.IsNullOrWhiteSpace(parameter.OrderBy))
            {
                string orderby = parameter.OrderBy;
                orderby = orderby.Remove(orderby.Length - 2, 2);

                model.GridTable.GridTable.Orderby = orderby;
            }

            return PartialView("ElGrid/ElDataGridBody", model);
        }

        private ElDbTable GetBilgiler()
        {
            string sql = "select a.*,b.deger as kart_adi from el_roller a " +
                "left join el_liste_deger b on a.rol_karti = b.kod and b.liste_id = 26 ";
            ElDbTable table = ExecuteTable(sql);
            table.KeyField = "id";
            return table;
        }
        public JsonResult DeleteRow(int id)
        {
            ElResult result = new ElResult();
            result.SetOk();

            try
            {
                ElDbTable db = GetTable("el_roller");
                db.Delete("id", id);
            }
            catch (Exception e)
            {
                result.SetError();
                result.AddErrorMessage(e.Message);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}