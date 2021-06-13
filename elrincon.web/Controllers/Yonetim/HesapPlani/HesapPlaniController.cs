using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using elrincon.web.Manager;
using elrincon.web.Models.SharedModel;
using elrincon.web.Models.Yonetim.HesapPlani;

namespace elrincon.web.Controllers.Yonetim.HesapPlani
{
    public class HesapPlaniController : BaseController
    {
        // GET: HesapPlani
        public ActionResult Index()
        {
            List<HesapPlaniModel> model = new List<HesapPlaniModel>();
            FillModel(model);
            return View("~/views/yonetim/HesapPlani/HesapPlani.cshtml", model);
        }

        private void FillModel(List<HesapPlaniModel> models)
        {
            ElDbTable dataTable = GetBilgiler();

            foreach (DataRow row in dataTable.Rows)
            {
                HesapPlaniModel model = new HesapPlaniModel();
                model.Id = Convert.ToInt32(row["id"]);
                model.MuhasebeKasaKodu = new ElInputModel(row["kasa_adi"],InputDataType.Text);

                models.Add(model);
            }
        } 

        private ElDbTable GetBilgiler()
        {
            ElDbTable table = GetTable("el_hesap_plani");

            return table;
        } 

        //public JsonResult SaveOrUpdate(HesapPlaniSaveParentModel saveModel)
        //{
        //    ElResult result = new ElResult(); //ControlResult(kullaniciAdi, kullaniciSifre);

        //    if (result.HasError())
        //        return Json(result, JsonRequestBehavior.AllowGet);

        //    if (saveModel == null)
        //        return Json(result, JsonRequestBehavior.AllowGet); 

        //    ElDbTable table = GetTable("el_muhasebe_tanimlari_kasa");
        //    ElDbTable tableBilgi = GetTable("el_muhasebe_tanimlari_kasa_bilgi");
        //    ElDbTable tableBilgiDetay = GetTable("el_muhasebe_tanimlari_kasa_bilgi_detay");
        //    try
        //    {
        //        table.BeginTransaction();
        //        tableBilgi.BeginTransaction();
        //        tableBilgiDetay.BeginTransaction();

        //        //foreach (HesapPlaniSaveModel model in saveModel)
        //        //{

        //        //    RowBufferModel bufferModel = new RowBufferModel();
        //        //    bufferModel.Add("kasa_adi", model.MuhasebeKasaKodu);

        //        //    InsertBilgi(tableBilgi, tableBilgiDetay, model);
                    
        //        //    table.InsertTable(bufferModel);
        //        //}

        //        table.CommitTransaction();
        //        tableBilgi.CommitTransaction();
        //        tableBilgiDetay.CommitTransaction();

        //        result.SetOk();

        //    }
        //    catch (Exception e)
        //    {
        //        table.RoolbackTransaction();
        //        tableBilgi.RoolbackTransaction();
        //        tableBilgiDetay.RoolbackTransaction();

        //        result.AddErrorMessage("hata : " + e.Message);
        //        result.SetError();
        //    }

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        //private void InsertBilgi(ElDbTable tableBilgi,ElDbTable tableBilgiDetay, HesapPlaniSaveModel saveModel)
        //{
        //    RowBufferModel bufferModel = new RowBufferModel();
        //    bufferModel.Add("kada_bilgi_adi", saveModel.MuhasebeKasaKodu);
        //    InsertBilgiDetay(tableBilgiDetay, saveModel);


        //    tableBilgi.InsertTable(bufferModel);
        //}

        //private void InsertBilgiDetay(ElDbTable tableBilgiDetay, HesapPlaniSaveModel saveModel)
        //{
        //    RowBufferModel bufferModel = new RowBufferModel();
        //    bufferModel.Add("kasa_bilgi_detay_adi", saveModel.MuhasebeKasaKodu);
            
        //    tableBilgiDetay.InsertTable(bufferModel);
        //}

        //private ElResult ControlResult(string kullaniciAdi, string kullaniciSifre)
        //{
        //    ElResult result = new ElResult();

        //    if (string.IsNullOrWhiteSpace(kullaniciSifre))
        //        result.AddMessage("Kullanıcı Şifre Boş Olamaz.");

        //    if (string.IsNullOrWhiteSpace(kullaniciAdi))
        //        result.AddMessage("Kullanıcı Adı Boş Olamaz.");

        //    if (!string.IsNullOrWhiteSpace(result.message))
        //        result.SetError();

        //    return result;
        //}
    }
}