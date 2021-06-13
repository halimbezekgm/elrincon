using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Web.Mvc;
using elrincon.web.Manager;
using elrincon.web.Models.SharedModel;
using elrincon.web.Models.Yonetim.MuhasebeTanimlari;

namespace elrincon.web.Controllers.Yonetim.MusabeTanimlari
{
    public class MuhasebeTanimlariController : BaseController
    {
        // GET: MuhasebeTanimlari
        public ActionResult Index()
        {
            List<MuhasebeKayitlariModel> model = new List<MuhasebeKayitlariModel>();
            FillModel(model);
            return View("~/views/Yonetim/muhasebetanimlari/muhasebetanimlari.cshtml", model);
        }

        private void FillModel(List<MuhasebeKayitlariModel> models)
        {
            ElDbTable dataTable = GetBilgiler();

            foreach (DataRow row in dataTable.Rows)
            {
                MuhasebeKayitlariModel model = new MuhasebeKayitlariModel();
                model.Id = Convert.ToInt32(row["id"]);
                model.MuhasebeKasaKodu = new ElInputModel(row["kasa_adi"],InputDataType.Text);
                model.MuhasebeTanimlariBilgiKodlari = GetMuhasebeTanimlariBilgiKodlari(model.Id);

                models.Add(model);
            }
        }

        private List<MuhasebeTanimlariBilgiKodu> GetMuhasebeTanimlariBilgiKodlari(int Id)
        {
            List<MuhasebeTanimlariBilgiKodu> models = new List<MuhasebeTanimlariBilgiKodu>();

            ElDbTable dataTable = GetBilgilerBilgi(Id);

            foreach (DataRow row in dataTable.Rows)
            {
                MuhasebeTanimlariBilgiKodu model = new MuhasebeTanimlariBilgiKodu();

                model.DetayId = Convert.ToInt32(row["id"]);
                model.MuhasebeKasaBilgiKodu = new ElInputModel(row["kada_bilgi_adi"],InputDataType.Text);
                model.MuhasebeTanimlariBilgiDetayKodlari = GetMuhasebeTanimlariBilgiDetayKodlari(model.DetayId);
                models.Add(model);
            }

            if (dataTable.Rows.Count <= 0)
            {
                MuhasebeTanimlariBilgiKodu model = new MuhasebeTanimlariBilgiKodu();

                model.DetayId = 0;
                model.MuhasebeKasaBilgiKodu = new ElInputModel(InputDataType.Text);
                model.MuhasebeTanimlariBilgiDetayKodlari = GetMuhasebeTanimlariBilgiDetayKodlari(0);
                models.Add(model);
            }

            return models;
        }

        private List<MuhasebeTanimlariBilgiDetayKodu> GetMuhasebeTanimlariBilgiDetayKodlari(int detayId)
        {
            List<MuhasebeTanimlariBilgiDetayKodu> modeList = new List<MuhasebeTanimlariBilgiDetayKodu>();
          
            ElDbTable dataTable = GetBilgilerBilgiDetay(detayId);

            foreach (DataRow row in dataTable.Rows)
            {
                MuhasebeTanimlariBilgiDetayKodu bilgiDetayKodu = new MuhasebeTanimlariBilgiDetayKodu();

                bilgiDetayKodu.MuhasebeKasaBilgiDetayKodu = new ElInputModel(row["kasa_bilgi_detay_adi"],InputDataType.Text);
                modeList.Add(bilgiDetayKodu);
            }

            if (detayId == 0 || dataTable.Rows.Count <=0 )
            {
                MuhasebeTanimlariBilgiDetayKodu bilgiDetayKodu = new MuhasebeTanimlariBilgiDetayKodu();

                bilgiDetayKodu.MuhasebeKasaBilgiDetayKodu = new ElInputModel(InputDataType.Text);
                modeList.Add(bilgiDetayKodu);

                return modeList;
            }


            return modeList;
        }

        private ElDbTable GetBilgiler()
        {
            string sql = "select * from el_hesap_plani ";
            ElDbTable table = ExecuteTable(sql);

            return table;
        }
        
        private ElDbTable GetBilgilerBilgi(int parentid)
        {
            string sql = "select * from el_muhasebe_tanimlari_kasa_bilgi where kasa_id = " + parentid ;

            ElDbTable table = ExecuteTable(sql);

            return table;
        }

        private ElDbTable GetBilgilerBilgiDetay(int parentid)
        {
            string sql = "select * from el_muhasebe_tanimlari_kasa_bilgi_detay where kasa_bilgi_id = " + parentid ;

            ElDbTable table = ExecuteTable(sql);

            return table;
        }

        public JsonResult SaveOrUpdate(MuhasebeTanimSaveParentModel saveModel)
        {
            ElResult result = new ElResult(); //ControlResult(kullaniciAdi, kullaniciSifre);

            if (result.HasError())
                return Json(result, JsonRequestBehavior.AllowGet);

            if (saveModel == null)
                return Json(result, JsonRequestBehavior.AllowGet); 

            ElDbTable table = GetTable("el_hesap_plani");
            ElDbTable tableBilgi = GetTable("el_muhasebe_tanimlari_kasa_bilgi");
            ElDbTable tableBilgiDetay = GetTable("el_muhasebe_tanimlari_kasa_bilgi_detay");
            try
            {
               // table.BeginTransaction();
                tableBilgi.BeginTransaction();
                tableBilgiDetay.BeginTransaction();

                tableBilgiDetay.Delete(tableBilgiDetay.TableName);
                foreach (MuhasebeTanimSaveModel model in saveModel.SaveModels)
                {

                    //RowBufferModel bufferModel = new RowBufferModel();
                    //bufferModel.Add("kasa_adi", model.MuhasebeKasaKodu);

                    InsertBilgi(tableBilgi, tableBilgiDetay, model);

                    //table.InsertTable(bufferModel);
                }

               // table.CommitTransaction();
                tableBilgi.CommitTransaction();
                tableBilgiDetay.CommitTransaction();

                result.SetOk();

            }
            catch (Exception e)
            {
                table.RoolbackTransaction();
                tableBilgi.RoolbackTransaction();
                tableBilgiDetay.RoolbackTransaction();

                result.AddErrorMessage("hata : " + e.Message);
                result.SetError();
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private void InsertBilgi(ElDbTable tableBilgi,ElDbTable tableBilgiDetay, MuhasebeTanimSaveModel model)
        {
            tableBilgi.Delete("kasa_id", model.Id);

            foreach (MuhasebeTanimlariSaveBilgiKodu bilgiKodu in model.MuhasebeTanimlariBilgiKodlari)
            {
                if (bilgiKodu.MuhasebeKasaBilgiKodu==null)
                {
                    continue;
                }

                RowBufferModel bufferModel = new RowBufferModel();
                bufferModel.Add("kasa_id", model.Id);
                bufferModel.Add("kada_bilgi_adi", bilgiKodu.MuhasebeKasaBilgiKodu);
                int bilgiId = tableBilgi.InsertTable(bufferModel);

                InsertBilgiDetay(tableBilgiDetay, bilgiKodu, bilgiId);
                
            }
        }

        private void InsertBilgiDetay(ElDbTable tableBilgiDetay, MuhasebeTanimlariSaveBilgiKodu saveModel,int bilgiIdNew)
        {
            if (saveModel.DetayId == null)
                saveModel.DetayId = 0;
            //tableBilgiDetay.Delete("id", saveModel.DetayId);
           // tableBilgiDetay.Delete(tableBilgiDetay.TableName);

            foreach (MuhasebeTanimlariSaveBilgiDetayKodu modelDetayKodu in saveModel.MuhasebeTanimlariBilgiDetayKodlari)
            {
                if(modelDetayKodu.MuhasebeKasaBilgiDetayKodu==null)
                    continue;

                RowBufferModel bufferModel = new RowBufferModel();
                bufferModel.Add("kasa_bilgi_id", bilgiIdNew);
                bufferModel.Add("kasa_bilgi_detay_adi", modelDetayKodu.MuhasebeKasaBilgiDetayKodu);

                tableBilgiDetay.InsertTable(bufferModel);
            }
        }

        private ElResult ControlResult(string kullaniciAdi, string kullaniciSifre)
        {
            ElResult result = new ElResult();

            if (string.IsNullOrWhiteSpace(kullaniciSifre))
                result.AddMessage("Kullanıcı Şifre Boş Olamaz.");

            if (string.IsNullOrWhiteSpace(kullaniciAdi))
                result.AddMessage("Kullanıcı Adı Boş Olamaz.");

            if (!string.IsNullOrWhiteSpace(result.message))
                result.SetError();

            return result;
        }
    }
}