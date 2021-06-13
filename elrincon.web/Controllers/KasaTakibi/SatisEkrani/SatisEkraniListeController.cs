using System;
using System.Web.Mvc;
using elrincon.web.Manager;
using elrincon.web.Models.KasaTakibi.SatisEkrani;
using elrincon.web.Models.SharedModel;
using elrincon.web.Models.SharedModel.GridModel;
using elrincon.web.Models.Yonetim.SatisEkrani;

namespace elrincon.web.Controllers.KasaTakibi.SatisEkrani
{
    public class SatisEkraniListeController : SatisEkraniBaseController
    {
        // GET: PersonelListe
        public ActionResult Index()
        {
            SatisEkraniListeModel model = new SatisEkraniListeModel();
            model.GridModel = FillGridModel(FillGridCustomProperties());
            model.SatisEkraniEditorModel = FillEditorModel();

            return View("~/Views/KasaTakibi/SatisEkrani/SatisEkraniListe.cshtml", model);
        }

        private SatisEkraniEditorModel FillEditorModel()
        {
            return FillModel(null, "_for_query");
        }

        private CustomProperties FillGridCustomProperties()
        {
            CustomProperties customProperties = new CustomProperties();
            customProperties.Add("rezervasyon_no", "");
            customProperties.Add("acenta", "");
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
            ElDataGridModel dataGridModel = new ElDataGridModel("satisekrani_liste_grid_id");

            dataGridModel.GridTable = new DataGridTable(dataTable, "id");
            dataGridModel.GridParameter.CustomProperties = customProperties;


            GridRow row = new GridRow("Satış Tarihi", "satis_tarihi", "15%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Satış Tutarı", "satis_tutari", "10%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("S.P", "satis_para_tipi", "5%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Alan Ad/Soyad", "alan_ad_soyad", "12%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Ülkesi", "ulke_adi", "10%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Ödeme şekli", "odeme_sekil_adi", "10%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Kargo Ucreti", "kargo_ucreti", "8%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);
             
            row = new GridRow("Aciklama", "aciklama", "20%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            ActionButton buttonEdit = new ActionButton("edit", @Url.Content("~/Content/images/edit.png"), "edit_satisekrani_listesi_cliced");
            ActionButton buttonDelete = new ActionButton("edit", @Url.Content("~/Content/images/delete.png"), "delete_satisekrani_listesi_cliced");

            dataGridModel.ActionButtons.Add(buttonEdit);
            dataGridModel.ActionButtons.Add(buttonDelete);
            dataGridModel.ActionButtonWith = "10%";

            dataGridModel.PageSize = 10;

            dataGridModel.DataUrl = Url.Action("GetGridBody");

            return dataGridModel;
        }

        private ElDbTable GetBilgiler(CustomProperties customProperties)
        {
            string sql = "select a.id,satis_tarihi, satis_tutari , satis_para_cinsi, alan_ad_soyad,aciklama, l.deger as satis_para_tipi,l1.deger as ulke_adi," +
                         " l2.deger as odeme_sekil_adi, ulkesi, odeme_sekli, kargo_ucreti " +
                         "from el_satis a " +
                         "left join el_liste_deger l on l.kod=a.satis_para_cinsi and l.liste_id = 5 " +
                         "left join el_liste_deger l1 on l1.kod=a.ulkesi and l1.liste_id = 21 " +
                         "left join el_liste_deger l2 on l2.kod=a.odeme_sekli and l2.liste_id = 3 ";
          
            //if (customProperties.ContainsKey("adi") && !string.IsNullOrWhiteSpace(customProperties["adi"]))
            //{
            //    sql += " and adi like '" + customProperties["adi"] +"'";
            //}

            //if (customProperties.ContainsKey("soyadi") && !string.IsNullOrWhiteSpace(customProperties["soyadi"]))
            //{
            //    sql += " and soyadi like '" + customProperties["soyadi"] +"'";
            //}

            //if (customProperties.ContainsKey("tc_no") && !string.IsNullOrWhiteSpace(customProperties["tc_no"]))
            //{
            //    sql += " and tc_no like '" + customProperties["tc_no"] +"'";
            //}

            //if (customProperties.ContainsKey("tel_no1") && !string.IsNullOrWhiteSpace(customProperties["tel_no1"]))
            //{
            //    sql += " and tel_no1 like '" + customProperties["tel_no1"] +"'";
            //}

            //if (customProperties.ContainsKey("subesi") && !string.IsNullOrWhiteSpace(customProperties["subesi"]))
            //{
            //    sql += " and subesi = " + customProperties["subesi"] +"";
            //}

            //if (customProperties.ContainsKey("bolumu") && !string.IsNullOrWhiteSpace(customProperties["bolumu"]))
            //{
            //    sql += " and bolum = " + customProperties["bolumu"] +"";
            //}

            //if (customProperties.ContainsKey("maas_tipi") && !string.IsNullOrWhiteSpace(customProperties["maas_tipi"]))
            //{
            //    sql += " and maas_tipi = " + customProperties["maas_tipi"] +"";
            //}

            //if (customProperties.ContainsKey("maas_miktari") && !string.IsNullOrWhiteSpace(customProperties["maas_miktari"]))
            //{
            //    sql += " and maas_miktari = " + customProperties["maas_miktari"] +"";
            //}
             
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
                ElDbTable db = GetTable("el_satis");
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