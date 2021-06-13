using elrincon.web.Manager;
using elrincon.web.Models.SharedModel;
using elrincon.web.Models.Yonetim.Yetki;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace elrincon.web.Controllers.Yonetim.Yetki
{
    public class YetkiEditorController : BaseController
    {
        // GET: YetkiEditor
        public ActionResult Index(int id)
        {
            List<YetkiEditorModel> modelList = FillEditorModel(id);
            return View("~/Views/Yonetim/Yetki/YetkiEditor.cshtml", modelList);
        }

        private List<YetkiEditorModel> FillEditorModel(int id)
        {
            List<YetkiEditorModel> modelList = new List<YetkiEditorModel>();

            string sql =
                "select a.deger as kartAdi,a.kod as kartId,b.*, " +
                "case b.goruntule_durum when 1 then 'checked' else '' end as goruntuleBaslik, " +
                "case b.ekle_durum when 1 then 'checked' else '' end as ekleBaslik, " +
                "case b.guncelle_durum when 1 then 'checked' else '' end as guncelleBaslik, " +
                "case b.sil_durum when 1 then 'checked' else '' end as silBaslik " +
                "from el_liste_deger a " +
                "left join ( " +
                "select a.*,b.kullanici_adi from el_yetkiler a " +
                "left join el_kullanicilar b on a.kullanici_id = b.id " +
                "where b.id = " + id + ") as b on a.kod = b.kart_id " +
                "where a.liste_id = 26 ";
            ElDbTable table = ExecuteTable(sql); 

            foreach(DataRow item in table.Rows)
            {
                YetkiEditorModel model = new YetkiEditorModel();
                model.KullaniciId = id;
                model.KullaniciAdi = CHelper.ToString(item["kullanici_adi"]);
                model.KartId = CHelper.ToInt32(item["kartId"]);
                model.KartAdi = CHelper.ToString(item["kartAdi"]);
                model.GoruntuleDurum = CHelper.ToString(item["goruntuleBaslik"]);
                model.EkleDurum = CHelper.ToString(item["ekleBaslik"]);
                model.GuncelleDurum = CHelper.ToString(item["guncelleBaslik"]);
                model.SilDurum = CHelper.ToString(item["silBaslik"]); 
                modelList.Add(model);
            }

            return modelList;
        }

        public JsonResult SaveYetki(List<YetkiSaveModel> saveModel)
        {
            ElResult result = new ElResult();
            if (result.HasError())
                return Json(result, JsonRequestBehavior.AllowGet);

            //önceki yetkileri temizle
            ElDbTable silYetkiTable = GetTable("el_yetkiler");
            silYetkiTable.Delete("kullanici_id", saveModel[0].KullaniciId);

            ElDbTable table = GetTable("el_yetkiler");
            try
            {
                foreach (YetkiSaveModel item in saveModel)
                {
                    RowBufferModel buffer = new RowBufferModel();
                    buffer.Add("kart_id", item.KartId);
                    buffer.Add("kullanici_id", item.KullaniciId);
                    buffer.Add("goruntule_durum", item.GoruntuleDurum);
                    buffer.Add("ekle_durum", item.EkleDurum);
                    buffer.Add("guncelle_durum", item.GuncelleDurum);
                    buffer.Add("sil_durum", item.SilDurum); 
                    table.InsertTable(buffer);
                }
                result.SetOk();
            }
            catch (Exception e)
            {
                result.AddErrorMessage("hata : " + e.Message);
                result.SetError();
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}