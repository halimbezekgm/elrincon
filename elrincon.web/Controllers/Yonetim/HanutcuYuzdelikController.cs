using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using elrincon.web.Controllers.PersonelBilgileri.Hanutcu;
using elrincon.web.Manager;
using elrincon.web.Models.PersonelBilgileri.Hanutcu;
using elrincon.web.Models.SharedModel;

namespace elrincon.web.Controllers.Yonetim
{
    public class HanutcuYuzdelikController : HanutcuBaseController
    {
        // GET: PersonelYuzdelik
        public ActionResult Index()
        {
        
            List<KardanYuzdelikOranlariModel> modelKardanOrani  = new List<KardanYuzdelikOranlariModel>();

            ElDbTable table = GetTable();

            foreach (DataRow dataRow in table.Rows)
            {
                if (!dataRow.IsNull("urunTipi"))
                    FillEmptyYuzdelikModel(modelKardanOrani, dataRow);
            }

            FillEmptyYuzdelikModel(modelKardanOrani, null);

            return View("/Views/Shared/KardanYuzdelikOranlariHanutcu.cshtml", modelKardanOrani);
        }

        private ElDbTable GetTable()
        {
            string sql =
                "select *,muhasebe_kodu as muhasebe_kodu_oran,ROW_NUMBER() OVER (ORDER BY yuzdelik_tip, urunTipi ) AS RowNum " +
                "from el_kardan_yuzdelik_oranlari " +
                "where yuzdelik_tip in(2,3) and personel_id = -2 ";

            ElDbTable table = ExecuteTable(sql);

            return table;
        }

        public JsonResult SaveOrUpdate(List<KardanYuzdelikOraniSaveModel> model)
        {
            ElResult result = new ElResult();
            //ElResult result = ControlResult(model);

            //if (result.HasError())
            //    return Json(result, JsonRequestBehavior.AllowGet);
             
            ElDbTable tableYuzdelik = GetTable("el_kardan_yuzdelik_oranlari");

            try
            { 
                tableYuzdelik.BeginTransaction();
                 
                InsertKardanYuzdelikOranlari(model, tableYuzdelik);
                 
                tableYuzdelik.CommitTransaction();

                result.SetOk();
            }
            catch (Exception e)
            { 
                tableYuzdelik.RoolbackTransaction();

                result.AddErrorMessage("hata : " + e.Message);
                result.SetError();
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private void InsertKardanYuzdelikOranlari(List<KardanYuzdelikOraniSaveModel> model, ElDbTable tableYuzdelik)
        {
            tableYuzdelik.Delete("yuzdelik_tip", 2);
            tableYuzdelik.Delete("yuzdelik_tip", 3);

            foreach (KardanYuzdelikOraniSaveModel yuzdelikOraniModel in model)
            {
                if (!yuzdelikOraniModel.UrunTipi.HasValue)
                    continue;

                RowBufferModel bufferModel = new RowBufferModel();
                bufferModel.Add("personel_id", CHelper.ToInt32(-2));
                bufferModel.Add("yuzdelik_tip", CHelper.ToInt32(yuzdelikOraniModel.YuzdeTip));
                bufferModel.Add("urunTipi", CHelper.ToInt32(yuzdelikOraniModel.UrunTipi));
                bufferModel.Add("normalSatisParite1", CHelper.ToDouble(yuzdelikOraniModel.NarmalSatisParite1));
                bufferModel.Add("normalSatisParite2", CHelper.ToDouble(yuzdelikOraniModel.NarmalSatisParite2));
                bufferModel.Add("normalSatisYuzdelik", CHelper.ToDouble(yuzdelikOraniModel.NarmalSatisYuzdeligi));
                bufferModel.Add("kapiSatisParite1", CHelper.ToDouble(yuzdelikOraniModel.KapiSatisParite1));
                bufferModel.Add("kapiSatisParite2", CHelper.ToDouble(yuzdelikOraniModel.KapiSatisParite2));
                bufferModel.Add("kapiSatisYuzdelik", CHelper.ToDouble(yuzdelikOraniModel.KapiSatisYuzdeligi));
                bufferModel.Add("internetSatisParite1", CHelper.ToDouble(yuzdelikOraniModel.InternetSatisParite1));
                bufferModel.Add("internetSatisParite2", CHelper.ToDouble(yuzdelikOraniModel.InternetSatisParite2));
                bufferModel.Add("internetSatisYuzdelik", CHelper.ToDouble(yuzdelikOraniModel.InternetSatisYuzdeligi));
                bufferModel.Add("yurtDisiSatisParite1", CHelper.ToDouble(yuzdelikOraniModel.YurtDisiSatisParite1));
                bufferModel.Add("yurtDisiSatisParite2", CHelper.ToDouble(yuzdelikOraniModel.YurtDisiSatisParite2));
                bufferModel.Add("yurtDisiSatisYuzdelik", CHelper.ToDouble(yuzdelikOraniModel.YurtDisiSatisYuzdeligi));
                bufferModel.Add("genelSatisParite1", CHelper.ToDouble(yuzdelikOraniModel.GenelCiroSatisParite1));
                bufferModel.Add("genelSatisParite2", CHelper.ToDouble(yuzdelikOraniModel.GenelCiroSatisParite2));
                bufferModel.Add("genelSatisYuzdelik", CHelper.ToDouble(yuzdelikOraniModel.GenelCiroSatisYuzdeligi));

                tableYuzdelik.InsertTable(bufferModel);
            }
        }
    }
}