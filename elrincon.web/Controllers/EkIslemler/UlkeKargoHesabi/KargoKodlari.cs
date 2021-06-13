using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using System.Xml;
using elrincon.web.Manager;
using elrincon.web.Models.EkIslemler.UlkeKargoHesabi;
using elrincon.web.Models.SharedModel;

namespace elrincon.web.Controllers.EkIslemler.UlkeKargoHesabi
{
    public class KargoKodlariController : BaseController
    {
        // GET: KargoKodlari
        public ActionResult Index()
        {
            List<KargoKodlariModel> model = FillEditorModel();
            return View("~/Views/EkIslemler/UlkeKargoHesabi/KargoKodlari.cshtml", model);
        }

        private List<KargoKodlariModel> FillEditorModel()
        {
            List<KargoKodlariModel> modellist = new List<KargoKodlariModel>();
            ViewBag.Title = "Ülke Kargo Kodları";

            string sql = "select l.kod as ulkekod ,l.deger as ulkeadi, k.* " +
                         "from el_ulke_kargo_kodlari k " +
                         "right join el_liste_deger l on  l.kod = k.ulke_id where liste_id = 21 ";
            ElDbTable table = ExecuteTable(sql);

            foreach (DataRow row in table.Rows)
            {

                KargoKodlariModel model= new KargoKodlariModel();
                
                model.id = (int) row["ulkekod"];

                ElInputModel ulkeAdi = new ElInputModel(row["ulkeadi"].ToString(),InputDataType.Text);
                model.InputUlkeAdiModel = ulkeAdi;
                model.InputUlkeAdiModel.Disabled = true;

                ElInputModel dhlKod = new ElInputModel(row["dhl"].ToString(),InputDataType.Integer);
                model.InputDHLModel = dhlKod;
                
                ElInputModel upsModel = new ElInputModel(row["ups"].ToString(),InputDataType.Integer);
                model.InputUPSModel = upsModel;
                
                ElInputModel tntModel = new ElInputModel(row["tnt"].ToString(),InputDataType.Integer);
                model.InputTNTModel = tntModel;
                
                modellist.Add(model);
            }


            return modellist;
        } 


        public JsonResult SaveOrUpdate(List<KargoHesabiSaveModel> saveModels)
        {
            ElResult result = new ElResult();//ControlResult(saveModels);

            if (result.HasError())
                return Json(result, JsonRequestBehavior.AllowGet);

            ElDbTable table = GetTable("el_ulke_kargo_kodlari");

            //string sqlTrunc = "TRUNCATE TABLE " + yourTableName
            //SqlCommand cmd = new SqlCommand(sqlTrunc, conn);
            //cmd.ExecuteNonQuery();

            try
            {
                table.BeginTransaction();

                table.Delete("delete_id",1);

                foreach (KargoHesabiSaveModel saveModel in saveModels)
                {
                    if(saveModel.dhl == 0 && saveModel.tnt == 0 && saveModel.ups == 0 ) 
                        continue;

                    RowBufferModel bufferModel = new RowBufferModel();
                    
                    bufferModel.Add("ulke_id", CHelper.ToInt32(saveModel.id));
                    //bufferModel.Add("ulke_adi", CHelper.ToString(saveModel.adi));
                    bufferModel.Add("dhl", CHelper.ToInt32(saveModel.dhl));
                    bufferModel.Add("ups", CHelper.ToInt32(saveModel.ups));
                    bufferModel.Add("tnt", CHelper.ToInt32(saveModel.tnt));
                    bufferModel.Add("delete_id", 1);
                    
                    table.InsertTable(bufferModel);

                    
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

        private ElResult ControlResult(KargoHesabiSaveModel saveModel)
        {
            ElResult result = new ElResult();
            result.SetOk();
            
            return result;
        }
    }
}