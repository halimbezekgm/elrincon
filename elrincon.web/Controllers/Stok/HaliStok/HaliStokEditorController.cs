using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using elrincon.web.Manager;
using elrincon.web.Models.PersonelBilgileri.Personel;
using elrincon.web.Models.SharedModel;
using elrincon.web.Models.Stok.HaliStok;

namespace elrincon.web.Controllers.Stok.HaliStok
{
    public class HaliStokEditorController : HaliStokBaseController
    {
        // GET: HaliStokEditor
        public ActionResult Index(int? id, int tip)
        {
            HaliStokEditorModel modelEditor = FillModel(id, "");
            modelEditor.Id = id;
            if(tip == 2)
                return View("~/Views/Stok/HaliStok/HaliStokEditorChangeSube.cshtml", modelEditor);
            
            return View("~/Views/Stok/HaliStok/HaliStokEditor.cshtml", modelEditor);

        }

        public JsonResult SaveOrUpdate(HaliStokSaveModel model)
        {
            ElResult result = ControlResult(model);

            if (result.HasError())
                return Json(result, JsonRequestBehavior.AllowGet);
            double s = 45.5;

            ElDbTable table = GetTable("el_hali_stok");

            try
            {

                RowBufferModel bufferModel = new RowBufferModel();
                bufferModel.Add("tarih", CHelper.ToDateTime(model.Tarih));
                bufferModel.Add("stok_no", CHelper.ToString(model.StokNo));
                bufferModel.Add("subesi", CHelper.ToInt32(model.Sube));
                bufferModel.Add("urun_cesidi", CHelper.ToInt32(model.UrunCesidi));
                bufferModel.Add("ana_grup", CHelper.ToInt32(model.AnaGrubu));
                bufferModel.Add("mensei", CHelper.ToInt32(model.Mensei));
                bufferModel.Add("uretim_tipi", CHelper.ToInt32(model.UretimTipi));
                bufferModel.Add("material", CHelper.ToInt32(model.Material));
                bufferModel.Add("olcu_tip", CHelper.ToInt32(model.OlcuTip));
                bufferModel.Add("dugum_tip", CHelper.ToInt32(model.DugumTip));
                bufferModel.Add("olcu_boy", CHelper.ToDouble(model.OlcuBoy));
                bufferModel.Add("olcu_en", CHelper.ToDouble(model.OlcuEn));
                bufferModel.Add("olcu_mt2", CHelper.ToDouble(model.OlcuMt2.Replace(".",",")));
                bufferModel.Add("inc_olcu_boy", CHelper.ToDouble(model.IncOlcuBoy.Replace(".", ",")));
                bufferModel.Add("inc_olcu_en", CHelper.ToDouble(model.IncOlcuEn.Replace(".", ",")));
                bufferModel.Add("inc_olcu_mt2", CHelper.ToDouble(model.IncOlcuMt2.Replace(".", ",")));
                bufferModel.Add("dugum_sayisi", CHelper.ToDouble(model.DugumSayisi));
                bufferModel.Add("renk", CHelper.ToString(model.Renk));
                bufferModel.Add("satici", CHelper.ToInt32(model.Satici));
                bufferModel.Add("firma", CHelper.ToString(model.FirmaKdu));
                bufferModel.Add("toplam_maliyet", Math.Round(CHelper.ToDouble(model.ToplamMaliyet),2));
                bufferModel.Add("birim_fiyat", Math.Round(CHelper.ToDouble(model.birimFiyat), 2));
                bufferModel.Add("etiket_carpani", Math.Round(CHelper.ToDouble(model.EtiketCarpani), 2));
                bufferModel.Add("adet_fiyat", Math.Round(CHelper.ToDouble(model.AdetFiyat), 2));
                bufferModel.Add("etiket_fiyat", Math.Round(CHelper.ToDouble(model.EtiketFiyat), 2));
                bufferModel.Add("tamir_yikama", Math.Round(CHelper.ToDouble(model.TamirYikama), 2));
                bufferModel.Add("satis_carpani", Math.Round(CHelper.ToDouble(model.SatisCarpani), 2));
                bufferModel.Add("satis_fiyati", Math.Round(CHelper.ToDouble(model.SatisFiyati), 2));
                bufferModel.Add("aciklama", CHelper.ToString(model.Aciklama));
                bufferModel.Add("konsinye_pesin", CHelper.ToInt32(model.KonsinyePesin));
                bufferModel.Add("konsinye_verilen_id", CHelper.ToInt32(model.KonsinyeVerilen));

                if (!model.Id.HasValue || model.Id.Value == 0)
                     table.InsertTable(bufferModel);
                else
                    table.UpdateTable(bufferModel, model.Id.Value);

                result.SetOk();
            }
            catch (Exception e)
            {
                result.AddErrorMessage("hata : " + e.Message);
                result.SetError();
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
         

        private ElResult ControlResult(HaliStokSaveModel model)
        {
            ElResult result = new ElResult();

            if (string.IsNullOrWhiteSpace(model.StokNo))
                result.AddMessage("Stok Numarası Boş Olamaz.");

            if (string.IsNullOrWhiteSpace(model.Tarih))
                result.AddMessage("Tarih Boş Olamaz.");
            
            if (!model.Sube.HasValue)
                result.AddMessage("Şubesi Boş Olamaz.");

            if (!model.AnaGrubu.HasValue)
                result.AddMessage("Ana Grubu Boş Olamaz.");

            if (!string.IsNullOrWhiteSpace(model.StokNo) && ControlSameRecord("el_hali_stok", "stok_no", model.StokNo, model.Id))
                result.AddMessage("Bu Stok Numarası daha önce kullanılmış! ");

            int boy = Convert.ToInt32(model.OlcuBoy);
            int en = Convert.ToInt32(model.OlcuEn);
            if (boy < en)
                result.AddMessage("Boyu eninden küçük olamaz! ");
             
             
            if (!string.IsNullOrWhiteSpace(result.message))
                result.SetError();

            return result;
        }

        public JsonResult GetStokNo(int haliorKilim, int? id)
        {
            string sql = "select top 1 * from el_hali_stok where urun_cesidi = " + haliorKilim + " order by stok_no desc ";
            string sqlKontrol = "select top 1 * from el_hali_stok where id = " + id;

            ElDbTable table = ExecuteTable(sql);
            ElDbTable tableKontrol = ExecuteTable(sqlKontrol);


            int lastid = haliorKilim == 1 ? 100001 : 200001;
            if (table.Rows.Count > 0)
                lastid = Convert.ToInt32(table.Rows[0]["stok_no"]) + 1;

            if (tableKontrol.Rows.Count > 0 && haliorKilim == Convert.ToInt32(tableKontrol.Rows[0]["urun_cesidi"]))
                lastid = Convert.ToInt32(tableKontrol.Rows[0]["stok_no"]);

            return Json(lastid, JsonRequestBehavior.AllowGet);
        }
    }
}