using System.Data;
using System.Web.Mvc;
using elrincon.web.Manager;
using elrincon.web.Models.EkIslemler.UlkeKargoHesabi;
using elrincon.web.Models.SharedModel;

namespace elrincon.web.Controllers.EkIslemler.UlkeKargoHesabi
{
    public class KargoHesabiController : BaseController
    {
        // GET: KargoHesabi
        public ActionResult Index()
        {
            KargoHesabiModel model = FillEditorModel();
            return View("~/Views/EkIslemler/UlkeKargoHesabi/KargoHesabi.cshtml", model);
        }

        private KargoHesabiModel FillEditorModel()
        {
            KargoHesabiModel model = new KargoHesabiModel();
            
            model.Ulkeler = new ElSelectModel(GetTable("el_liste_deger").Select("liste_id = 21 "),"deger","kod");
            model.Ulkeler.Id = "ulkeler_select_id";

            model.KargoFirmalari = new ElSelectModel();
            model.KargoFirmalari.Add(new CodeValueDic(1,"DHL"));
            model.KargoFirmalari.Add(new CodeValueDic(2,"UPS"));
            model.KargoFirmalari.Add(new CodeValueDic(3,"TNT"));
            model.KargoFirmalari.Id = "kargo_firma_id";

            model.Miktar = new ElInputModel(InputDataType.Integer);
            model.Miktar.Id = "miktar_input_id";

            model.Sonuc = new ElInputModel(InputDataType.Integer);
            model.Sonuc.Disabled = true;
            model.Sonuc.Id = "sonuc_input_id";

            return model;
        }
        private ElDbTable GetTableRow(int id)
        {
            string sql = "select * from el_kurlar where id = " + id;
            ElDbTable table = ExecuteTable(sql);
            return table;
        }

        public JsonResult KargoHesapla(int? ulkeId, int? kargoFilma, double? agirlik)
        {
            //model.KargoFirmalari.Add(new CodeValueDic(1, "DHL"));
            //model.KargoFirmalari.Add(new CodeValueDic(2, "UPS"));
            //model.KargoFirmalari.Add(new CodeValueDic(3, "TNT"));
            ElResult result = CheckAndResult( ulkeId, kargoFilma, agirlik);

            if (result.HasError())
                return Json(result, JsonRequestBehavior.AllowGet);

            string sql = "select * from el_ulke_kargo_kodlari where ulke_id = " + ulkeId;
            ElDbTable table = ExecuteTable(sql);

            int kargoFirmaCarpani = 0;
            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                if (kargoFilma == 1)
                    kargoFirmaCarpani = (int) row["dhl"];
                else if (kargoFilma == 2)
                    kargoFirmaCarpani = (int) row["ups"];
                else if (kargoFilma == 3)    
                    kargoFirmaCarpani = (int) row["tnt"];
            }

            double sonuc = kargoFirmaCarpani * agirlik.Value;

            return Json(data: new {sonuc=sonuc}, JsonRequestBehavior.AllowGet);
        }

        private ElResult CheckAndResult(int? ulkeId, int? kargoFilma, double? agirlik)
        {
            ElResult result = new ElResult();

            if (!ulkeId.HasValue)
                result.AddMessage("Ulke Boş Olmaz..");

            if (!kargoFilma.HasValue)
                result.AddMessage("Kargo Firması Boş Olmaz..");

            if (!agirlik.HasValue)
                result.AddMessage("Ağırlık Boş Olmaz..");

            if (!string.IsNullOrWhiteSpace(result.message))
                result.SetError();

            return result;
        }
    }
}