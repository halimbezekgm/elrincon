using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using elrincon.web.Manager;
using elrincon.web.Models.PersonelBilgileri;
using elrincon.web.Models.PersonelBilgileri.Personel;
using elrincon.web.Models.SharedModel;
using elrincon.web.Models.SharedModel.GridModel;

namespace elrincon.web.Controllers.PersonelBilgileri.Personel
{
    public class PersonelListeController : PersonelBaseController
    {
        // GET: PersonelListe
        public ActionResult Index()
        {
            PersonelListeModel model = new PersonelListeModel();
            model.GridModel = FillGridModel(FillGridCustomProperties());
            model.PersonelEditorModel = FillEditorModel();

            return View("~/Views/PersonelBilgileri/Personel/PersonelListe.cshtml",model);
        }

        private PersonelEditorModel FillEditorModel()
        {
            return FillModel(null, "_for_query");
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
            customProperties.Add("maas_miktari", "");

            return customProperties;
        }


        private ElDataGridModel FillGridModel(CustomProperties customProperties)
        {

            ElDbTable dataTable = GetBilgiler(customProperties);
            ElDataGridModel dataGridModel = new ElDataGridModel("personel_liste_grid_id");

            dataGridModel.GridTable = new DataGridTable(dataTable, "id");
            dataGridModel.GridParameter.CustomProperties = customProperties;


            GridRow row = new GridRow("Adı", "adi", "25%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Soyadı", "soyadi", "25%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Telefon no", "tel_no1", "15%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Şubesi", "subesi_adi", "15%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Bölümü", "bolumu", "10%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            ActionButton buttonEdit = new ActionButton("edit", @Url.Content("~/Content/images/edit.png"), "edit_personel_listesi_cliced");
            ActionButton buttonDelete = new ActionButton("edit", @Url.Content("~/Content/images/delete.png"), "delete_personel_listesi_cliced");

            dataGridModel.ActionButtons.Add(buttonEdit);
            dataGridModel.ActionButtons.Add(buttonDelete);
            dataGridModel.ActionButtonWith = "10%";

            dataGridModel.PageSize = 10;

            dataGridModel.DataUrl = Url.Action("GetGridBody");

            return dataGridModel;
        }

        private ElDbTable GetBilgiler(CustomProperties customProperties)
        {
            string sql = "select a.*, l.deger as subesi_adi, l2.deger as bolumu from el_personel a " +
                         "left join el_liste_deger l on l.kod = a.subesi and l.liste_id = 2 " +
                         "left join el_liste_deger l2 on l2.kod = a.bolum and l2.liste_id = 1 " +
                         " where personel_tipi = 1";
          
            if (customProperties.ContainsKey("adi") && !string.IsNullOrWhiteSpace(customProperties["adi"]))
            {
                sql += " and adi like '" + customProperties["adi"] +"'";
            }

            if (customProperties.ContainsKey("soyadi") && !string.IsNullOrWhiteSpace(customProperties["soyadi"]))
            {
                sql += " and soyadi like '" + customProperties["soyadi"] +"'";
            }

            if (customProperties.ContainsKey("tc_no") && !string.IsNullOrWhiteSpace(customProperties["tc_no"]))
            {
                sql += " and tc_no like '" + customProperties["tc_no"] +"'";
            }

            if (customProperties.ContainsKey("tel_no1") && !string.IsNullOrWhiteSpace(customProperties["tel_no1"]))
            {
                sql += " and tel_no1 like '" + customProperties["tel_no1"] +"'";
            }

            if (customProperties.ContainsKey("subesi") && !string.IsNullOrWhiteSpace(customProperties["subesi"]))
            {
                sql += " and subesi = " + customProperties["subesi"] +"";
            }

            if (customProperties.ContainsKey("bolumu") && !string.IsNullOrWhiteSpace(customProperties["bolumu"]))
            {
                sql += " and bolum = " + customProperties["bolumu"] +"";
            }

            if (customProperties.ContainsKey("maas_tipi") && !string.IsNullOrWhiteSpace(customProperties["maas_tipi"]))
            {
                sql += " and maas_tipi = " + customProperties["maas_tipi"] +"";
            }

            if (customProperties.ContainsKey("maas_miktari") && !string.IsNullOrWhiteSpace(customProperties["maas_miktari"]))
            {
                sql += " and maas_miktari = " + customProperties["maas_miktari"] +"";
            }
             
            ElDbTable table = ExecuteTable(sql);
            table.KeyField = "id";

            return table; 
        }

        /// <summary>
        /// Grid body 
        /// </summary>
        public ActionResult GetGridBody(GridParameter parameter)
        {
            ElDataGridModel model = FillGridModel(parameter.CustomProperties);
           // model.SetParameter(parameter);

            if (!string.IsNullOrWhiteSpace(parameter.OrderBy))
            {
                string orderby = parameter.OrderBy;
                orderby = orderby.Remove(orderby.Length - 2, 2);

                model.GridTable.GridTable.Orderby= orderby;
            }
            //else
            //    model.GridTable.GridTable.Orderby = " order by id asc";

            //model.GridTable.GridTable.OffsetFetch = Convert.ToInt32(parameter.PageSize);
            //model.GridTable.GridTable.Offset = (Convert.ToInt32(parameter.PageNumber)-1) * Convert.ToInt32(parameter.PageSize);

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