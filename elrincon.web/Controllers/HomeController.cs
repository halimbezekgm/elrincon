using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using elrincon.web.Models.SharedModel;

namespace elrincon.web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index(int ? subeId)
        {
            DataRow[] tableRows = GetTable("el_liste_deger").Select("liste_id = 2");
            ElSelectModel selectSubeModel = new ElSelectModel(tableRows, "deger", "kod");
            selectSubeModel.Id = "home_index_sube_id";

            ViewBag.UserSubeId = ElUser.SubeId;
            if (subeId.HasValue)
               ViewBag.UserSubeId = subeId;

            selectSubeModel.SetSelectedCode(ViewBag.UserSubeId);
            ViewBag.UserSubeAdi = GetSubeName(ViewBag.UserSubeId);

            return View(selectSubeModel);
        }

        private string GetSubeName(int userSubeId)
        {
            DataRow[] tableRows = GetTable("el_liste_deger").Select("liste_id = 2 and kod = " + userSubeId);
            
            return tableRows.First()["deger"].ToString();
        }
    }
}