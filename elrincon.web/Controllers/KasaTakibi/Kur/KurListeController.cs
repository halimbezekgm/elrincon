using elrincon.web.Manager;
using elrincon.web.Models.KasaTakibi.Kur;
using elrincon.web.Models.SharedModel;
using elrincon.web.Models.SharedModel.GridModel;
using System;
using System.Web.Mvc;

namespace elrincon.web.Controllers.KasaTakibi.Kur
{
    public class KurListeController : BaseController
    {
        // GET: KurListe
        public ActionResult Index()
        {
            KurListeModel model = new KurListeModel(); 
            model.gridModel = FillGridModel(FillGridCustomProperties());
            DateTime starTime = Convert.ToDateTime("01.01."+DateTime.Today.Year);
            model.sorguBasTarih = new ElInputModel(starTime, InputDataType.Date);
            model.sorguBasTarih.Id = "kur_liste_sorgu_baslangic_tarih_input_id";
            model.sorguBitTarih = new ElInputModel(DateTime.Today.AddDays(+1), InputDataType.Date);
            model.sorguBitTarih.Id = "kur_liste_sorgu_bitis_tarih_input_id";
            return View("~/Views/KasaTakibi/Kur/KurListe.cshtml", model);
        }

        private CustomProperties FillGridCustomProperties()
        {
            DateTime starTime = Convert.ToDateTime("01.01." + DateTime.Today.Year);
            CustomProperties customProperties = new CustomProperties();
            customProperties.Add("sorguBasTarih", starTime.ToString("yyyy-MM-dd"));
            customProperties.Add("sorguBitTarih", DateTime.Today.AddDays(+1).ToString("yyyy-MM-dd")); 

            return customProperties;
        }

        private ElDataGridModel FillGridModel(CustomProperties customProperties)
        {
            ElDbTable dataTable = GetBilgiler(customProperties);
            ElDataGridModel dataGridModel = new ElDataGridModel("kur_liste_grid_id");
            dataGridModel.GridTable = new DataGridTable(dataTable, "id");
            dataGridModel.GridParameter.CustomProperties = customProperties;

            GridRow row = new GridRow("USD", "USD", "4%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("EUR", "EUR", "4%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("GBP", "GBP", "4%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("CHF", "CHF", "4%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("AUD", "AUD", "4%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("CAD", "CAD", "4%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("JPY", "JPY", "3%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("CNY", "CNY", "4%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row); 

            row = new GridRow("P.CHI", "PCHI", "4%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("P.MXN", "PMXN", "4%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("EUR/USD", "EUR/USD", "6%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("GBP/USD", "GBP/USD", "6%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("CHF/USD", "CHF/USD", "6%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("USD/AUD", "USD/AUD", "6%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("USD/CAD", "USD/CAD", "6%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("USD/JPY", "USD/JPY", "6%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("USD/CNY", "USD/CNY", "6.5%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row); 

            row = new GridRow("Tarih", "tarih", "10%", InputDataType.Date);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            ActionButton buttonEdit = new ActionButton("edit", @Url.Content("~/Content/images/edit.png"), "edit_kur_listesi_clicked");
            ActionButton buttonDelete = new ActionButton("edit", @Url.Content("~/Content/images/delete.png"), "delete_kur_listesi_clicked");

            dataGridModel.ActionButtons.Add(buttonEdit);
            dataGridModel.ActionButtons.Add(buttonDelete);
            dataGridModel.ActionButtonWith = "8%";
            dataGridModel.PageSize = 8;
            dataGridModel.DataUrl = Url.Action("GetGridBody");
            return dataGridModel;
        }


        public ActionResult GetGridBody(GridParameter parameter)
        {
            if (parameter.CustomProperties == null)
                parameter.CustomProperties = FillGridCustomProperties();

            ElDataGridModel model = FillGridModel(parameter.CustomProperties);

            if (!string.IsNullOrWhiteSpace(parameter.OrderBy))
            {
                string orderby = parameter.OrderBy;
                orderby = orderby.Remove(orderby.Length - 2, 2);

                model.GridTable.GridTable.Orderby = orderby;
            }

            return PartialView("ElGrid/ElDataGridBody", model);
        }

        private ElDbTable GetBilgiler(CustomProperties customProperties)
        {
            string sql = "select * ," +
                "FORMAT(CONVERT(DECIMAL(15, 3), EUR) / CONVERT(DECIMAL(15, 3), USD), 'N3') as 'EUR/USD' ," +
                "FORMAT(CONVERT(DECIMAL(15, 3), GBP) / CONVERT(DECIMAL(15, 3), USD), 'N3') as 'GBP/USD' ," +
                "FORMAT(CONVERT(DECIMAL(15, 3), CHF) / CONVERT(DECIMAL(15, 3), USD), 'N3') as 'CHF/USD' ," +
                "FORMAT(CONVERT(DECIMAL(15, 3), USD) / CONVERT(DECIMAL(15, 3), AUD), 'N3') as 'USD/AUD' ," +
                "FORMAT(CONVERT(DECIMAL(15, 3), USD) / CONVERT(DECIMAL(15, 3), CAD), 'N3') as 'USD/CAD' ," +
                "FORMAT(CONVERT(DECIMAL(15, 3), USD) / CONVERT(DECIMAL(15, 3), JPY), 'N3') as 'USD/JPY' ," +
                "FORMAT(CONVERT(DECIMAL(15, 3), USD) / CONVERT(DECIMAL(15, 3), CNY), 'N3') as 'USD/CNY'" +
                "from el_kurlar";
            sql += " where tarih between '" + customProperties["sorguBasTarih"] + "' and  '" + customProperties["sorguBitTarih"] + "'";

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
                ElDbTable db = GetTable("el_kurlar");
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