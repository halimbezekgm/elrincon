using elrincon.web.Manager;
using elrincon.web.Models.SharedModel;
using elrincon.web.Models.SharedModel.GridModel;
using System.Web.Mvc;

namespace elrincon.web.Controllers.Yonetim.Yetki
{
    public class YetkiListeController : BaseController
    {
        // GET: YetkiListe
        public ActionResult Index()
        {
            ElDataGridModel model = new ElDataGridModel();
            model = FillGridModel();
            return View("~/Views/Yonetim/Yetki/YetkiListe.cshtml", model);
        }

        private ElDataGridModel FillGridModel()
        {
            ElDbTable dataTable = GetBilgiler();
            ElDataGridModel dataGridModel = new ElDataGridModel("yetki_liste_grid_id");
            dataGridModel.GridTable = new DataGridTable(dataTable, "id");
            dataGridModel.GridParameter.CustomProperties = new CustomProperties();

            GridRow row = new GridRow("Kullanıcı Adı", "kullanici_adi", "75%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row); 

            ActionButton buttonEdit = new ActionButton("edit", @Url.Content("~/Content/images/edit.png"), "edit_yetki_listesi_clicked");

            dataGridModel.ActionButtons.Add(buttonEdit);
            dataGridModel.ActionButtonWith = "25%";
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
            string sql = "select * from el_kullanicilar";
            ElDbTable table = ExecuteTable(sql);
            table.KeyField = "id";
            return table;
        } 
    }
}