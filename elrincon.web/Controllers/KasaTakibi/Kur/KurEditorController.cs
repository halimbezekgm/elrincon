using elrincon.web.Manager;
using elrincon.web.Models.KasaTakibi.Kur;
using elrincon.web.Models.SharedModel;
using elrincon.web.Models.Yonetim.Rol;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace elrincon.web.Controllers.KasaTakibi.Kur
{
    public class KurEditorController : BaseController
    {
        // GET: KurEditor
        public ActionResult Index(int? id, DateTime? queryDate)
        {
            /*
             * Bugüne dair döviz kurunu çekmek için ilgili url bilgisi aşağıdadır.
                http://www.tcmb.gov.tr/kurlar/today.xml
                İstediğimiz herhangi bir güne ait kurları da yine aşağıdaki linkten çekebiliriz. Burada verdiğiniz tarih eğer hafta sonu ise, sistem hata verebilir. Bunun için girilen tarih hafta sonu ise günü geçmiş Cuma günü ayarlayabilirsiniz.
                http://www.tcmb.gov.tr/kurlar/yyyyMM/ddMMyyyy.xml
                Gelelim buradaki bize sunulan xml döviz kuru bilgisini C# tarafında nasıl okuyacağımıza.. Aşağıda bununla ilgili örnek kodu görebilirsiniz.
                string exchangeRate = “http://www.tcmb.gov.tr/kurlar/today.xml";
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(exchangeRate);

                string usd = xmlDoc.SelectSingleNode(“Tarih_Date/Currency[@Kod=’USD’]/BanknoteSelling”).InnerXml;
                Aynı şekilde url adresini herhangi bir günün tarihi olarak ayarladığımızda da yukarıdaki örnekle ilgili tarihin döviz kurunu çekebiliriz.

             */
            KurEditorModel model = FillEditorModel(id, queryDate);
            return View("~/Views/KasaTakibi/Kur/KurEditor.cshtml", model);
        }

        private KurEditorModel FillEditorModel(int? _id, DateTime? queryDate)
        {
            KurEditorModel model = new KurEditorModel();
            model.IsForDateQuery = false;
            ViewBag.Title = "Günlük Kur Ekle";
            int id;
            if (_id == null)
            {
                model.IsForDateQuery = true;
                ViewBag.Title = "Günlük Kur Sorgula";
                if (!queryDate.HasValue)
                    queryDate = DateTime.Today;
                

                id = 0;
            }
            else
                id = _id.Value;
        
            ElInputModel USD = new ElInputModel(InputDataType.Double);
            USD.Id = "kur_editor_input_USD";
            USD.IsForQuery = true;
            model.InputUSDModel = USD; 

            ElInputModel EUR = new ElInputModel(InputDataType.Double);
            EUR.Id = "kur_editor_input_EUR";
            EUR.IsForQuery = true;
            model.InputEURModel = EUR; 

            ElInputModel GBP = new ElInputModel(InputDataType.Double);
            GBP.Id = "kur_editor_input_GBP";
            GBP.IsForQuery = true;
            model.InputGBPModel = GBP; 

            ElInputModel CHF = new ElInputModel(InputDataType.Double);
            CHF.Id = "kur_editor_input_CHF";
            CHF.IsForQuery = true;
            model.InputCHFModel = CHF; 

            ElInputModel AUD = new ElInputModel(InputDataType.Double);
            AUD.Id = "kur_editor_input_AUD";
            AUD.IsForQuery = true;
            model.InputAUDModel = AUD; 

            ElInputModel CAD = new ElInputModel(InputDataType.Double);
            CAD.Id = "kur_editor_input_CAD";
            CAD.IsForQuery = true;
            model.InputCADModel = CAD; 

            ElInputModel JPY = new ElInputModel(InputDataType.Double);
            JPY.Id = "kur_editor_input_JPY";
            JPY.IsForQuery = true;
            model.InputJPYModel = JPY; 

            ElInputModel CNY = new ElInputModel(InputDataType.Double);
            CNY.Id = "kur_editor_input_CNY";
            CNY.IsForQuery = true;
            model.InputCNYModel = CNY; 

            ElInputModel ALTIN = new ElInputModel(InputDataType.Double);
            ALTIN.Id = "kur_editor_input_ALTIN";
            ALTIN.IsForQuery = true;
            model.InputALTINModel = ALTIN; 

            ElInputModel pesoMxn = new ElInputModel(InputDataType.Double);
            pesoMxn.Id = "kur_editor_input_pesoMxn";
            pesoMxn.IsForQuery = true;
            model.InputPESOMXNModel = pesoMxn; 

            ElInputModel pesoChi = new ElInputModel(InputDataType.Double);
            pesoChi.Id = "kur_editor_input_pesoChi";
            pesoChi.IsForQuery = true;
            model.InputPESOCHIModel = pesoChi; 


            DateTime tarih = DateTime.Today;
            DateTime kayitTarih = DateTime.Today;
            if (queryDate.HasValue)
            {
                tarih = queryDate.Value;
                kayitTarih = queryDate.Value;
            }

            model.KurEditorBaslik = DateTime.Now;
            if (tarih.DayOfWeek == DayOfWeek.Saturday)
                tarih = kayitTarih.AddDays(-1);
            if (tarih.DayOfWeek == DayOfWeek.Sunday)
                tarih = kayitTarih.AddDays(-2);

            var xmlDoc = new XmlDocument();
            try
            {
                string exchangeRate = "http://www.tcmb.gov.tr/kurlar/" + tarih.ToString("yyyyMM") + "/" + tarih.ToString("ddMMyyyy") + ".xml";
                 xmlDoc = new XmlDocument();
                xmlDoc.Load(exchangeRate);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
         

            if (id != 0)
            {
                ElDbTable table = GetTableRow(id);
                DataRow row = table.Rows[0];
                model.InputUSDModel.Value = row["USD"]; 
                model.InputEURModel.Value = row["EUR"]; 
                model.InputGBPModel.Value = row["GBP"]; 
                model.InputCHFModel.Value = row["CHF"]; 
                model.InputAUDModel.Value = row["AUD"]; 
                model.InputCADModel.Value = row["CAD"]; 
                model.InputJPYModel.Value = row["JPY"]; 
                model.InputCNYModel.Value = row["CNY"]; 
                model.InputALTINModel.Value = row["ALTIN"];
                model.KurEditorBaslik = (DateTime) row["tarih"];

                kayitTarih = Convert.ToDateTime(row["tarih"]);
            }
            else
            {
                model.InputUSDModel.Value = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod ='USD']/BanknoteSelling")?.InnerXml;
                model.InputEURModel.Value = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod ='EUR']/BanknoteSelling")?.InnerXml;
                model.InputGBPModel.Value = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod ='GBP']/BanknoteSelling")?.InnerXml;
                model.InputCHFModel.Value = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod ='CHF']/BanknoteSelling")?.InnerXml;
                model.InputAUDModel.Value = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod ='AUD']/BanknoteSelling")?.InnerXml;
                model.InputCADModel.Value = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod ='CAD']/BanknoteSelling")?.InnerXml;
                model.InputJPYModel.Value = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod ='JPY']/BanknoteSelling")?.InnerXml;
                model.InputCNYModel.Value = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod ='CNY']/BanknoteSelling")?.InnerXml;
            }

            //tarih seçimi. Eğer cumartesi veya pazarsa cumanın kurlarını al
            
            model.AnlikUSDModel = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod ='USD']/BanknoteSelling")?.InnerXml;
            model.AnlikEURModel = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod ='EUR']/BanknoteSelling")?.InnerXml;
            model.AnlikGBPModel = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod ='GBP']/BanknoteSelling")?.InnerXml;
            model.AnlikCHFModel = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod ='CHF']/BanknoteSelling")?.InnerXml;
            model.AnlikAUDModel = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod ='AUD']/BanknoteSelling")?.InnerXml;
            model.AnlikCADModel = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod ='CAD']/BanknoteSelling")?.InnerXml;
            model.AnlikJPYModel = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod ='JPY']/BanknoteSelling")?.InnerXml;
            model.AnlikCNYModel = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod ='CNY']/BanknoteSelling")?.InnerXml; 

            model.KurEditorBaslik = tarih.Date;
            model.KurEditorKayitBaslik = kayitTarih.Date;

            if (!queryDate.HasValue)
                queryDate = kayitTarih;

            model.QueryDate = new ElInputModel(queryDate,InputDataType.Date);
            model.QueryDate.Id = "input_query_date";

            model.id = id;

            return model;
        }
        private ElDbTable GetTableRow(int id)
        {
            string sql = "select * from el_kurlar where id = " + id;
            ElDbTable table = ExecuteTable(sql);
            return table;
        }


        public JsonResult SaveOrUpdate(KurSaveModel saveModel)
        {
            ElResult result = ControlResult(saveModel);

            if (result.HasError())
                return Json(result, JsonRequestBehavior.AllowGet);

            ElDbTable table = GetTable("el_kurlar");

            try
            {
                RowBufferModel bufferModel = new RowBufferModel();
                bufferModel.Add("USD", CHelper.ToDouble(saveModel.USD.ToString()));
                bufferModel.Add("EUR", CHelper.ToDouble(saveModel.EUR.ToString()));
                bufferModel.Add("GBP", CHelper.ToDouble(saveModel.GBP.ToString()));
                bufferModel.Add("CHF", CHelper.ToDouble(saveModel.CHF.ToString()));
                bufferModel.Add("AUD", CHelper.ToDouble(saveModel.AUD.ToString()));
                bufferModel.Add("CAD", CHelper.ToDouble(saveModel.CAD.ToString()));
                bufferModel.Add("JPY", CHelper.ToDouble(saveModel.JPY.ToString()));
                bufferModel.Add("CNY", CHelper.ToDouble(saveModel.CNY.ToString()));
                bufferModel.Add("PCHI", CHelper.ToDouble(saveModel.PESOCHI.ToString()));
                bufferModel.Add("PMXN", CHelper.ToDouble(saveModel.PESOMXN.ToString()));
                bufferModel.Add("ALTIN", CHelper.ToDouble(saveModel.ALTIN.ToString().Replace(".", ",")));

                if (saveModel.id == 0)
                {
                    bufferModel.Add("tarih", CHelper.ToDateTime(saveModel.Tarih));

                    table.InsertTable(bufferModel);

                }
                else
                    table.UpdateTable(bufferModel, saveModel.id);

                result.SetOk();
            }
            catch (Exception e)
            {
                result.AddErrorMessage("hata : " + e.Message);
                result.SetError();
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private ElResult ControlResult(KurSaveModel saveModel)
        {
            ElResult result = new ElResult();

            if(saveModel.id == 0)
            {
                string sql = "select * from el_kurlar where tarih = '" + saveModel.Tarih.ToString("yyyy-MM-dd") + "'";
                ElDbTable table = ExecuteTable(sql);

                if (table.Rows.Count > 0)
                    result.AddMessage("Bugüne ait kayıt zaten oluşturulmuş.");
            }

            if (saveModel.USD == 0)
                result.AddMessage("USD Boş Olamaz."); 

            if (saveModel.EUR == 0)
                result.AddMessage("EUR Boş Olamaz."); 

            if (saveModel.GBP == 0)
                result.AddMessage("GBP Boş Olamaz."); 

            if (saveModel.CHF == 0)
                result.AddMessage("CHF Boş Olamaz."); 

            if (saveModel.AUD == 0)
                result.AddMessage("AUD Boş Olamaz."); 

            if (saveModel.CAD == 0)
                result.AddMessage("CAD Boş Olamaz."); 

            if (saveModel.JPY == 0)
                result.AddMessage("JPY Boş Olamaz."); 

            if (saveModel.CNY == 0)
                result.AddMessage("CNY Boş Olamaz."); 

            if (saveModel.ALTIN == 0)
                result.AddMessage("ALTIN Boş Olamaz."); 

            return result;
        }
    }
}