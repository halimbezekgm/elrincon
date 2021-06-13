using System.Data;
using System.Linq;
using System.Web.Mvc;
using elrincon.web.Manager;
using elrincon.web.Models.SharedModel;
using elrincon.web.Models.SharedModel.GridModel;

namespace elrincon.web.Controllers.Yonetim.DomainListesi
{
    public class DomainListeBaseController : BaseController
    {

        public ElDataGridModel FillModel(int id, CustomProperties properties )
        {
            properties.Add("id",id.ToString());
            return FillGridModel(properties);
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

        private ElDataGridModel FillGridModel(CustomProperties properties)
        {
            ElDbTable dataTable = GetBilgiler(properties);
            ElDataGridModel dataGridModel = new ElDataGridModel("domain_child_liste_grid_id");
            dataGridModel.GridTable = new DataGridTable(dataTable, "id");
            dataGridModel.GridParameter.CustomProperties = properties;

            GridRow row = new GridRow("Kodu", "kod", "25%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Değeri", "deger", "40%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            ActionButton buttonEdit = new ActionButton("Düzenle", @Url.Content("~/Content/images/edit.png"), "edit_domain_editor_cliced");
            ActionButton buttonDelete = new ActionButton("Sil", @Url.Content("~/Content/images/delete.png"), "delete_domain_editor_cliced");

            dataGridModel.ActionButtons.Add(buttonEdit);
            dataGridModel.ActionButtons.Add(buttonDelete);
            dataGridModel.ActionButtonWith = "35%";

            dataGridModel.PageSize = 15;

            dataGridModel.DataUrl = Url.Action("GetGridBody");

            return dataGridModel;
        }

        private ElDbTable GetBilgiler(CustomProperties customProperties)
        {
            string sql = "select * from el_liste_deger where 1=1 ";

            if (customProperties.ContainsKey("id") && !string.IsNullOrWhiteSpace(customProperties["id"]))
            {
                sql += " and liste_id = " + customProperties["id"] + "";
            }

            ElDbTable table = ExecuteTable(sql);
            table.KeyField = "id";

            return table;
        }


        public DataRow GetTableDomainListeRow(int id)
        {                
            string sql = "select * from el_liste where liste_id = " + id;

            ElDbTable table = ExecuteTable(sql);

            DataRow row = table.Rows[0];

            return row;
        }

    }
}