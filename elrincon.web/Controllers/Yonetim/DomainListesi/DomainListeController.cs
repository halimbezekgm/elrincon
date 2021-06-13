using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using elrincon.web.Controllers.PersonelBilgileri.Personel;
using elrincon.web.Manager;
using elrincon.web.Models.PersonelBilgileri;
using elrincon.web.Models.PersonelBilgileri.Personel;
using elrincon.web.Models.SharedModel;
using elrincon.web.Models.SharedModel.GridModel;
using elrincon.web.Models.Yonetim;

namespace elrincon.web.Controllers.Yonetim.DomainListesi
{
    public class DomainListeController : DomainListeBaseController
    {
        // GET: DomainListe
        public ActionResult Index()
        {
            DomainListeModel model = new DomainListeModel();
            model.GridModel = FillGridModel(FillGridCustomProperties());
            //model.PersonelEditorModel = FillEditorModel();

            return View("~/Views/Yonetim/DomainListe/DomainListe.cshtml", model);
        }
         

        private CustomProperties FillGridCustomProperties()
        {
            CustomProperties customProperties = new CustomProperties();
            customProperties.Add("adi", ""); 
            return customProperties;
        }

        /// <summary>
        /// Grid body 
        /// </summary>
        public ActionResult GetGridBodyList(GridParameter parameter)
        {
            ElDataGridModel model = FillGridModel(parameter.CustomProperties);
            // model.SetParameter(parameter);

            if (!string.IsNullOrWhiteSpace(parameter.OrderBy))
            {
                string orderby = parameter.OrderBy;
                orderby = orderby.Remove(orderby.Length - 2, 2);

                model.GridTable.GridTable.Orderby = orderby;
            }
            //else
            //    model.GridTable.GridTable.Orderby = " order by id asc";

            //model.GridTable.GridTable.OffsetFetch = Convert.ToInt32(parameter.PageSize);
            //model.GridTable.GridTable.Offset = (Convert.ToInt32(parameter.PageNumber)-1) * Convert.ToInt32(parameter.PageSize);

            return PartialView("ElGrid/ElDataGridBody", model);
        }

        private ElDataGridModel FillGridModel(CustomProperties customProperties)
        {

            ElDbTable dataTable = GetBilgiler(customProperties);
            ElDataGridModel dataGridModel = new ElDataGridModel("domain_liste_grid_id");

            dataGridModel.GridTable = new DataGridTable(dataTable, "liste_id");
            dataGridModel.GridParameter.CustomProperties = customProperties;


            GridRow row = new GridRow("Liste Adı", "liste_adi", "75%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row); 

            ActionButton buttonEdit = new ActionButton("Düzenle", @Url.Content("~/Content/images/edit.png"), "edit_domain_listesi_cliced");
            ActionButton buttonDelete = new ActionButton("Sil", @Url.Content("~/Content/images/delete.png"), "delete_domain_listesi_cliced");

            dataGridModel.ActionButtons.Add(buttonEdit);
            dataGridModel.ActionButtons.Add(buttonDelete);
            dataGridModel.ActionButtonWith = "25%";

            dataGridModel.PageSize = 50; 

            dataGridModel.DataUrl = Url.Action("GetGridBodyList");

            return dataGridModel;
        }

        private ElDbTable GetBilgiler(CustomProperties customProperties)
        {
            string sql = "select * from el_liste ";

            //if (customProperties.ContainsKey("adi") && !string.IsNullOrWhiteSpace(customProperties["adi"]))
            //{
            //    sql += " and adi like '" + customProperties["adi"] + "'";
            //}

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
                ElDbTable db = GetTable("el_liste");
                db.Delete("liste_id", id);
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