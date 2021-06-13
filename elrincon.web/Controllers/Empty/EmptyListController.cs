using System;
using System.Data;
using System.Web.Mvc;
using elrincon.web.Manager;
using elrincon.web.Models.SharedModel;
using elrincon.web.Models.SharedModel.GridModel;

namespace elrincon.web.Controllers.Empty
{
    public class EmptyListController : BaseController
    {
        // GET: Empty
        public ActionResult Index()
        { 
            ElDataGridModel dataGridModel= FillGridModel(FillGridCustomProperties());

            return View("~/views/empty/emptyList.cshtml",dataGridModel);
        }

        private CustomProperties FillGridCustomProperties()
        {
            CustomProperties customProperties = new CustomProperties();
            customProperties.Add("kullanici_adi","");

            return customProperties;
        }

        private ElDataGridModel FillGridModel(CustomProperties customProperties)
        {
            ElDataGridModel dataGridModel = new ElDataGridModel("empty_grid_id");

            ElDbTable dataTable = GetBilgiler();
            dataGridModel.GridTable = new DataGridTable(dataTable, "id");
            dataGridModel.GridParameter.CustomProperties = customProperties;


            GridRow row = new GridRow("Adı", "kullanici_adi","25%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            row = new GridRow("Şifre", "kullanici_sifre", "50%", InputDataType.Text);
            row.IsSortuble = true;
            dataGridModel.GridRows.Add(row);

            ActionButton buttonEdit = new ActionButton("edit",@Url.Content("~/Content/images/edit.png"),"edit_empty_cliced");
            ActionButton buttonDelete = new ActionButton("edit",@Url.Content("~/Content/images/delete.png"),"delete_empty_cliced");
            
            dataGridModel.ActionButtons.Add(buttonEdit);
            dataGridModel.ActionButtons.Add(buttonDelete);
            dataGridModel.ActionButtonWith = "10%";

            dataGridModel.DataUrl = Url.Action("GetGridBody");

            return dataGridModel;
        }

        private ElDbTable GetBilgiler()
        {
            ElDbTable table = GetTable("el_kullanicilar");
            
            return table;
        }
         
        /// <summary>
        /// Grid'in filtre, sort gibi olaylarda değişimi için...
        /// </summary>
        public ActionResult GetGridBody(GridParameter parameter)
        {
            ElDataGridModel model = FillGridModel(parameter.CustomProperties);
            //model.SetParameter(parameter);

            if(!string.IsNullOrWhiteSpace(parameter.OrderBy))
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
                ElDbTable db = GetTable("el_kullanicilar");
                db.Delete("id", id);
            }
            catch (Exception e)
            {
                result.SetError();
                result.AddErrorMessage(e.Message);
            }

            return Json(result,JsonRequestBehavior.AllowGet);
        }
    }
}