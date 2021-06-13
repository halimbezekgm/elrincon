using System;
using System.Data;
using System.Drawing.Drawing2D;
using System.Web.Mvc;
using elrincon.web.Manager;
using elrincon.web.Models.PersonelBilgileri;
using elrincon.web.Models.PersonelBilgileri.Hanutcu;
using elrincon.web.Models.SharedModel;
using elrincon.web.Models.SharedModel.GridModel;

namespace elrincon.web.Controllers.PersonelBilgileri.Hanutcu
{
    public class HanutcuListeController : HanutcuBaseController
    {
        // GET: PersonelListe
        public ActionResult Index()
        {
            HanutcuListeModel model = new HanutcuListeModel();
            model.GridModel = FillGridModel(FillGridCustomProperties());
            model.HanutcuEditorModel = FillModel(null, "_for_query");

            return View("~/Views/PersonelBilgileri/Hanutcu/HanutcuListe.cshtml",model);
        }

        private CustomProperties FillGridCustomProperties()
        {
            CustomProperties customProperties = new CustomProperties();
            customProperties.Add("adi", "");
            customProperties.Add("soyadi", "");
            customProperties.Add("tc_no", "");
            customProperties.Add("tel_no1", "");
            customProperties.Add("subesi", "");
            customProperties.Add("maas_tipi", "");

            return customProperties;
        }


        private ElDataGridModel FillGridModel(CustomProperties customProperties)
        {
            ElDataGridModel dataGridModel = new ElDataGridModel("hanutcu_liste_grid_id");

            ElDbTable dataTable = GetBilgiler(customProperties);
            dataGridModel.GridTable = new DataGridTable(dataTable, "id");
            dataGridModel.GridParameter.CustomProperties = customProperties;


            GridRow row = new GridRow("Adı", "adi", "25%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Soyadı", "soyadi", "25%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);
            
            row = new GridRow("Telefon no", "tel_no1", "20%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Şubesi", "subesi_adi", "20%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);
            
            ActionButton buttonEdit = new ActionButton("edit", @Url.Content("~/Content/images/edit.png"), "edit_hanutcu_listesi_cliced");
            ActionButton buttonDelete = new ActionButton("edit", @Url.Content("~/Content/images/delete.png"), "delete_hanutcu_listesi_cliced");

            dataGridModel.ActionButtons.Add(buttonEdit);
            dataGridModel.ActionButtons.Add(buttonDelete);
            dataGridModel.ActionButtonWith = "10%";

            dataGridModel.DataUrl = Url.Action("GetGridBody");

            return dataGridModel;
        }

        private ElDbTable GetBilgiler(CustomProperties customProperties)
        {
            string sql = "select a.*, l.deger as subesi_adi from el_personel a  " +
                         "left join el_liste_deger l on l.kod = a.subesi and l.liste_id = 2 " +
                         " where a.personel_tipi = 2 ";

            if (customProperties.ContainsKey("adi") && !string.IsNullOrWhiteSpace(customProperties["adi"]))
            {
                sql += " and adi like '" + customProperties["adi"] + "'";
            }

            if (customProperties.ContainsKey("soyadi") && !string.IsNullOrWhiteSpace(customProperties["soyadi"]))
            {
                sql += " and soyadi like '" + customProperties["soyadi"] + "'";
            }

            if (customProperties.ContainsKey("tc_no") && !string.IsNullOrWhiteSpace(customProperties["tc_no"]))
            {
                sql += " and tc_no like '" + customProperties["tc_no"] + "'";
            }

            if (customProperties.ContainsKey("tel_no1") && !string.IsNullOrWhiteSpace(customProperties["tel_no1"]))
            {
                sql += " and tel_no1 like '" + customProperties["tel_no1"] + "'";
            }

            if (customProperties.ContainsKey("subesi") && !string.IsNullOrWhiteSpace(customProperties["subesi"]))
            {
                sql += " and subesi = " + customProperties["subesi"] + "";
            }
             
            if (customProperties.ContainsKey("maas_tipi") && !string.IsNullOrWhiteSpace(customProperties["maas_tipi"]))
            {
                sql += " and maas_tipi = " + customProperties["maas_tipi"] + "";
            }
             
            ElDbTable table = ExecuteTable(sql);

            return table;
        }

        /// <summary>
        /// Grid body 
        /// </summary>
        public ActionResult GetGridBody(GridParameter parameter)
        {
            ElDataGridModel model = FillGridModel(parameter.CustomProperties);
            //model.SetParameter(parameter);

            if (!string.IsNullOrWhiteSpace(parameter.OrderBy))
            {
                string orderby = parameter.OrderBy;
                orderby = orderby.Remove(orderby.Length - 2, 2);
                model.GridTable.GridTable.DefaultView.Sort = orderby;
                model.GridTable.GridTable = (ElDbTable) model.GridTable.GridTable.DefaultView.ToTable();
            }

            return PartialView("ElGrid/ElDataGridBody", model);
        }

        public JsonResult DeleteRow(int id)
        {
            ElResult result = new ElResult();
            result.SetOk();

            try
            {
                ElDbTable db = GetTable("el_personel");
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