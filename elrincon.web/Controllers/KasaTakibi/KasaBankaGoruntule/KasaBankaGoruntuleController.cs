using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
using elrincon.web.Manager;
using elrincon.web.Models.KasaTakibi.KasaBankaGoruntule;
using elrincon.web.Models.SharedModel;
using elrincon.web.Models.SharedModel.GridModel;

namespace elrincon.web.Controllers.KasaTakibi.KasaBankaGoruntule
{
    public class KasaBankaGoruntuleController : BaseController
    {
        // GET: KasaBankaGoruntule
        public ActionResult Index()
        {
            KasaBankaGoruntuleModel model = new KasaBankaGoruntuleModel();
            model.SorguTarihiBas = new ElInputModel(InputDataType.Date);
            model.SorguTarihiBitis = new ElInputModel(InputDataType.Date);
            model.SorguDevirTarihi = new ElInputModel(InputDataType.Date);
            model.HesapNo = new ElSelectModel();
            model.HesapDetayNo = new ElSelectModel();
            model.DevirMiktariveParaCinsi = new ElInputModel(InputDataType.Text);
            model.DevirMiktariveParaCinsi.Disabled = true;
            model.KasaBankaListGridModel = FillGridModel(FillGridCustomProperties());

            return View("/views/kasatakibi/kasabankagoruntule/KasaBankaGoruntuleView.cshtml",model);
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

            ElDbTable dataTable = GetBilgiler();
            ElDataGridModel dataGridModel = new ElDataGridModel("kasa_banka_goruntulue_liste_grid_id");

            dataGridModel.GridTable = new DataGridTable(dataTable, "id");
            dataGridModel.GridParameter.CustomProperties = customProperties;

            GridRow row = new GridRow("Tarih", "adi", "20%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Giren", "soyadi", "10%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Çıkan", "tel_no1", "15%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Döviz", "subesi_adi", "10%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Açıklama", "ulkesi", "20%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Hesap Adı", "ulkesi", "15%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            ActionButton buttonEdit = new ActionButton("edit", @Url.Content("~/Content/images/edit.png"), "edit_rehber_listesi_cliced");
            ActionButton buttonDelete = new ActionButton("edit", @Url.Content("~/Content/images/delete.png"), "delete_rehber_listesi_cliced");

            dataGridModel.ActionButtons.Add(buttonEdit);
            dataGridModel.ActionButtons.Add(buttonDelete);
            dataGridModel.ActionButtonWith = "10%";

            dataGridModel.PageSize = 10;

            dataGridModel.DataUrl = Url.Action("GetGridBody");

            return dataGridModel;
        }

        private ElDbTable GetBilgiler()
        {
            string sql = "select top 0 a.*, l.deger as subesi_adi, l2.deger as ulkesi " +
                         "from el_rehber a " +
                         "left join el_liste_deger l on l.kod = a.subesi and l.liste_id = 2 " +
                         "left join el_liste_deger l2 on l2.kod = a.ulke and l.liste_id = 21 ";
            //" where rehber_tipi = 1";

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

                model.GridTable.GridTable.Orderby = orderby;
            }
            //else
            //    model.GridTable.GridTable.Orderby = " order by id asc";

            //model.GridTable.GridTable.OffsetFetch = Convert.ToInt32(parameter.PageSize);
            //model.GridTable.GridTable.Offset = (Convert.ToInt32(parameter.PageNumber)-1) * Convert.ToInt32(parameter.PageSize);

            return PartialView("ElGrid/ElDataGridBody", model);
        }
    }
}