using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using elrincon.web.Manager;
using elrincon.web.Models.KasaTakibi.SatisEkrani;
using elrincon.web.Models.SharedModel;
using elrincon.web.Models.Yonetim.SatisEkrani;

namespace elrincon.web.Controllers.KasaTakibi.SatisEkrani
{
    public class SatisEkraniEditorController : SatisEkraniBaseController
    {
        // GET: PersonelEditor
        public ActionResult Index(int? id)
        {
            SatisEkraniEditorModel modelEditor =  FillModel(id,"");
            modelEditor.Id = id;

            return View("~/Views/KasaTakibi/SatisEkrani/satisekraniEditor.cshtml", modelEditor);
        }
        
        public JsonResult SaveOrUpdate(SatisEkraniSaveModel model)
        {
            ElResult result = ControlResult(model);

            return null;
            if (result.HasError())
                return Json(result, JsonRequestBehavior.AllowGet);

            ElDbTable table = GetTable("el_satis"); 
            ElDbTable tablepersoneller = GetTable("el_satis_personelleri"); 
            ElDbTable tablestoklar = GetTable("el_satis_stok_listesi"); 
            ElDbTable tablealinanOdeme = GetTable("el_satis_alinan_odeme"); 
            ElDbTable tabletaksitler = GetTable("el_satis_taksitler"); 
            
            try
            { 
                table.BeginTransaction();
                tablepersoneller.BeginTransaction();
                tablestoklar.BeginTransaction();
                tabletaksitler.BeginTransaction();
                tablealinanOdeme.BeginTransaction();

                RowBufferModel bufferModel = new RowBufferModel(); 
                bufferModel.Add("islem_tarihi",DateTime.Now);
                bufferModel.Add("satis_tarihi",CHelper.ToDateTime(model.InputTarihModel));
                bufferModel.Add("sube_id", CHelper.ToInt32(model.SubeModel)); 
                bufferModel.Add("kontrat_no", CHelper.ToInt32(model.KontratNoModel)); 
                bufferModel.Add("satis_tutari", CHelper.ToDouble(model.SatisTutariModel)); 
                bufferModel.Add("satis_para_cinsi", CHelper.ToInt32(model.SatisParaCinsiModel)); 
                bufferModel.Add("etiket_toplami", CHelper.ToDouble(model.EtikekToplamiModel)); 
                bufferModel.Add("etiket_toplami_para_cinsi", CHelper.ToInt32(model.EtiketToplamiParaCinsiModel)); 
                bufferModel.Add("satis_paritesi", CHelper.ToDouble(model.SatisParitesiModel)); 
                bufferModel.Add("rezervasyon_bilgi", CHelper.ToDouble(model.RezervasyonBilgiModel)); 
                bufferModel.Add("satis_tipi", CHelper.ToDouble(model.SatisSekliModel)); 
                bufferModel.Add("alan_ad_soyad", CHelper.ToString(model.InputAdSoyadModel)); 
                bufferModel.Add("ulkesi", CHelper.ToInt32(model.UlkeModel)); 
                bufferModel.Add("aciklama", CHelper.ToString(model.InputAciklamaModel)); 
                bufferModel.Add("odeme_sekli", CHelper.ToInt32(model.OdemeSekli)); 
                bufferModel.Add("odeme_pesin_miktari", CHelper.ToDouble(model.OdemePesinMiktari)); 
                bufferModel.Add("odeme_pesin_para_cinsi", CHelper.ToInt32(model.OdemePesinMiktariCinsi)); 
                bufferModel.Add("kargo_ucreti", CHelper.ToDouble(model.KargoUcretiModel)); 
                bufferModel.Add("kargo_ucreti_para_cinsi", CHelper.ToInt32(model.KargoUcretParaCinsiModel));
                int? satisId = model.Id;
                if (!model.Id.HasValue || model.Id.Value == 0)
                   satisId = table.InsertTable(bufferModel);
                else
                    table.UpdateTable(bufferModel, model.Id.Value);
                
                InsertToSatisPersonelleri(satisId, tablepersoneller ,model.SatisPersonelleriList);
                InsertToSatisStoklari(satisId, tablestoklar ,model.StokListModel);
                InsertToSatisOdemeler(satisId, tablealinanOdeme ,model.AlinanOdemeler);
                InsertToSatisTaksitler(satisId, tabletaksitler ,model.TaksitList);

                table.CommitTransaction();
                tablepersoneller.CommitTransaction();
                tablestoklar.CommitTransaction();
                tablealinanOdeme.CommitTransaction();
                tabletaksitler.CommitTransaction();
                result.SetOk();
            }
            catch (Exception e)
            {
                table.RoolbackTransaction();
                tablepersoneller.RoolbackTransaction();
                tablealinanOdeme.RoolbackTransaction();
                tablestoklar.RoolbackTransaction();
                tabletaksitler.RoolbackTransaction();

                result.AddErrorMessage("hata : " + e.Message);
                result.SetError();
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private void InsertToSatisTaksitler(int? satisId, ElDbTable tabletaksitler, List<TaksitListesi> modelTaksitList)
        {
            tabletaksitler.Delete("satis_id", satisId);
            if(modelTaksitList == null)
                return;

            foreach (TaksitListesi save in modelTaksitList)
            {
                RowBufferModel bufferModel = new RowBufferModel();
                bufferModel.Add("taksit_miktari", CHelper.ToDouble(save.TaksitMiktari));
                bufferModel.Add("taksit_para_cinsi", CHelper.ToInt32(save.TaksitOdemeCinsi)); 
                bufferModel.Add("satis_id", satisId); 

                tabletaksitler.InsertTable(bufferModel);
            }
        }

        private void InsertToSatisOdemeler(int? satisId, ElDbTable tableAlinanOdeme, List<AlinanOdeme> modelAlinanOdeme)
        {
            tableAlinanOdeme.Delete("satis_id", satisId);
            if(modelAlinanOdeme == null)
                return;

            foreach (AlinanOdeme save in modelAlinanOdeme)
            {
                RowBufferModel bufferModel = new RowBufferModel();
                bufferModel.Add("odeme_hesap_no", CHelper.ToDouble(save.OdemeSekli));
                bufferModel.Add("odeme_miktari", CHelper.ToInt32(save.Odememiktari)); 
                bufferModel.Add("odeme_doviz_cinsi", CHelper.ToInt32(save.OdemeDovizCinsi)); 
                bufferModel.Add("satis_id", satisId); 

                tableAlinanOdeme.InsertTable(bufferModel);
            }
        }
    
        private void InsertToSatisStoklari(int? satisId, ElDbTable tablestoklar, List<StokListSave> modelStokListModel)
        {
            tablestoklar.Delete("satis_id", satisId);
            foreach (StokListSave save in modelStokListModel)
            {
                if (!save.StokNoModel.HasValue)
                 continue;

                RowBufferModel bufferModel = new RowBufferModel();
                bufferModel.Add("stok_id", save.StokNoModel);
                bufferModel.Add("stok_alis_tarihi", CHelper.ToDateTime(save.StokAlisTarihiModel));
                bufferModel.Add("stok_fiyati", CHelper.ToDouble(save.StokFiyatiModel));
                bufferModel.Add("stok_en", CHelper.ToInt32(save.StokEnModel));
                bufferModel.Add("stok_boy", CHelper.ToInt32(save.StokBoyModel));
                bufferModel.Add("satis_id", satisId);
                
                tablestoklar.InsertTable(bufferModel); 
            }
        }

        private void InsertToSatisPersonelleri(int? satisId, ElDbTable tablepersoneller, List<SatisPersonelleriSave> modelSatisPersonelleriList)
        {

            tablepersoneller.Delete("satis_id", satisId);
            foreach (SatisPersonelleriSave save in modelSatisPersonelleriList)
            {
                
                RowBufferModel bufferModel = new RowBufferModel();
                bufferModel.Add("personel_tip_bilgisi", CHelper.ToInt32(save.PersoneltipBilgiModel));
                bufferModel.Add("hali_yuzdelik_miktari", CHelper.ToDouble(save.HaliYuzdelikModel));
                bufferModel.Add("pirlanta_yuzde_miktari", CHelper.ToDouble(save.PirlantaYuzdelikModel));
                bufferModel.Add("fantezi_yuzde_miktari", CHelper.ToDouble(save.FanteziYuzdelikModel));
                bufferModel.Add("gumus_yuzde_miktari", CHelper.ToDouble(save.GumusYuzdelikModel));
                bufferModel.Add("seramik_yuzde_miktari", CHelper.ToDouble(save.SeramikYuzdelikModel));
                bufferModel.Add("altin_yuzde_miktari", CHelper.ToDouble(save.AltınYuzdelikModel));
                bufferModel.Add("deri_yuzde_miktari", CHelper.ToDouble(save.DeriYuzdelikModel));
                bufferModel.Add("hediyelik_yuzde_miktari", CHelper.ToDouble(save.HediyelikYuzdelikModel));
                bufferModel.Add("satis_id", satisId);

                tablepersoneller.InsertTable(bufferModel);
            }
        }

        private ElResult ControlResult(SatisEkraniSaveModel model)
        {
            ElResult result = new ElResult();
            
            if (!model.InputTarihModel.HasValue)
                result.AddMessage("SatisEkrani Tarihi Boş Olamaz.");
             
             
            if (!model.SatisSekliModel.HasValue)
                result.AddMessage("Satış Şekli Boş Olamaz.");
             
             
            if (!model.OdemeSekli.HasValue)
                result.AddMessage("Ödeme Şekli Boş Olamaz.");
             
             
            if (!string.IsNullOrWhiteSpace(result.message))
                result.SetError();

            return result;
        }

        public ActionResult GetOdemePartial(int odemeTipi)
        {
            
            SatisOdemeModel model = new SatisOdemeModel();
            model.SatisTaksitModel = new List<SatisOdemeTaksitModel>();
            model.SatisTipiMiktariModel = new List<SatisOdemeTipiMiktariModel>();

            //pesin
            SatisOdemeTipiMiktariModel modelOdeme = new SatisOdemeTipiMiktariModel();

            DataRow[] tableRowsMuhasebeGiderHesabi = GetTableMuhasebeGiderHesabi();
            ElSelectModel modelMuhasebeGiderHesabi = new ElSelectModel(tableRowsMuhasebeGiderHesabi, "names", "id");
            modelOdeme.OdemeTipiCinsi = modelMuhasebeGiderHesabi;

            ElInputModel odenecekMiktariPesin = new ElInputModel(InputDataType.Double);
            modelOdeme.OdemeMiktari = odenecekMiktariPesin;
            
            DataRow[] paraCinsi = GetTable("el_liste_deger").Select("liste_id = 5");
            ElSelectModel cinsiModelSatis = new ElSelectModel(paraCinsi, "deger", "kod");
            modelOdeme.OdemeParaCinsi = cinsiModelSatis;
             
            //taksit
            SatisOdemeTaksitModel modelTaksit = new SatisOdemeTaksitModel();

            ElInputModel odenecekMiktari = new ElInputModel(InputDataType.Double);
            odenecekMiktari.Id = "satisekrani_editor_editorodenecekMiktari";
            modelTaksit.OdenecekMiktari = odenecekMiktari;


            ElSelectModel cinsiModel = new ElSelectModel(paraCinsi, "deger", "kod");
            cinsiModel.Id = "satisekrani_editor_select_editorSatisParaCinsiModel";
            modelTaksit.OdenecekParaCinsi = cinsiModel;

            model.OdemeTipi = odemeTipi;
            model.SatisTaksitModel.Add(modelTaksit);
            model.SatisTipiMiktariModel.Add(modelOdeme);

            return PartialView("/views/KasaTakibi/SatisEkrani/SatisOdemeView.cshtml",model);
        }

        public JsonResult GetStokBilgileri(int stokId)
        { 
            DataRow[] stokBilgi = GetTable("el_hali_stok").Select("stok_no = " + stokId);
            string alisTarihi = Convert.ToDateTime(stokBilgi.First()["tarih"]).ToString("yyyy-MM-dd");
            string satisFiyat = stokBilgi.First()["satis_fiyati"].ToString();
            string olcuEn = stokBilgi.First()["olcu_en"].ToString();
            string olcuBoy = stokBilgi.First()["olcu_boy"].ToString();
            
            Dictionary<string, string> yuzdelikDegerleri = new Dictionary<string, string>();
            yuzdelikDegerleri.Add("tarih", alisTarihi);
            yuzdelikDegerleri.Add("fiyat",satisFiyat);
            yuzdelikDegerleri.Add("en",olcuEn);
            yuzdelikDegerleri.Add("boy", olcuBoy);

            return Json(yuzdelikDegerleri, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPerasonelYuzdelik(string inputId, int? id, int? satisSekli,string satisParite)
        {
            
            int i = Convert.ToInt32(inputId.Replace("satisekrani_editor_select_personeltipBilgiModel_", ""));
            ElResult result = ControlResultCalc(inputId,id, satisSekli, satisParite);
            if(result.HasError())
                return Json(result, JsonRequestBehavior.AllowGet);

            string sql = "";
            //satis şekli : 1 normal , 2 kapı, 3 internet , 4 yutdışı 
            string sorguSatisTipi = "normal";
            if (satisSekli == 2)
                sorguSatisTipi = "kapi";
            else if (satisSekli == 3)
                sorguSatisTipi = "internet";
            else if (satisSekli == 4)
                sorguSatisTipi = "yurtDisi";


            //tezgahtar
            if (i == 0 || i == 1)
                sql = "select b.urunTipi, " + sorguSatisTipi + "SatisYuzdelik as yuzde from el_kardan_yuzdelik_oranlari b " +
                      "left join el_liste_deger l on l.kod = b.urunTipi and l.liste_id = 6 " +
                      "where personel_id = "+id+
                      "and('"+ satisParite + "' <= "+ sorguSatisTipi + "SatisParite1 and '" + satisParite + "' >=  " + sorguSatisTipi + "SatisParite2) ";

            //hanutçu
            else if (i == 5 || i == 6)
                sql = "select b.urunTipi, " + sorguSatisTipi + "SatisYuzdelik as yuzde  from el_kardan_yuzdelik_oranlari b " +
                      "left join el_liste_deger l on l.kod = b.urunTipi and l.liste_id = 6 " +
                      "where personel_id = " + id +
                      "and('" + satisParite + "' <= " + sorguSatisTipi + "SatisParite1 and '" + satisParite + "' >=  " + sorguSatisTipi + "SatisParite2) ";

            //acente
            else if (i == 2)
                sql = "select b.urunTipi, " + sorguSatisTipi + "SatisYuzdelik as yuzde  from el_kardan_yuzdelik_oranlari_acenta b " +
                      "left join el_liste_deger l on l.kod = b.urunTipi and l.liste_id = 6 " +
                      "where acenta_id = " + id +
                      "and('" + satisParite + "' <= " + sorguSatisTipi + "SatisParite1 and '" + satisParite + "' >=  " + sorguSatisTipi + "SatisParite2) ";

            //rehber
            //if(i==3 || i==4)
            else sql = "select b.urunTipi, " + sorguSatisTipi + "SatisYuzdelik  as yuzde from el_kardan_yuzdelik_oranlari_rehber b " +
                       "left join el_liste_deger l on l.kod = b.urunTipi and l.liste_id = 6 " +
                       "where rehber_id = " + id +
                       "and('" + satisParite + "' <= " + sorguSatisTipi + "SatisParite1 and '" + satisParite + "' >=  " + sorguSatisTipi + "SatisParite2) ";


            ElDbTable table = ExecuteTable(sql);
            //table.Orderby = "deger";
            //return table.Select();
            Dictionary<string,string> yuzdelikDegerleri = new Dictionary<string, string>();
            for (int j = 1; j < 9; j++)
            {
                string yuzdelik = table.Select("urunTipi = " + j).FirstOrDefault() == null? "0" : table.Select("urunTipi = " + j).FirstOrDefault()["yuzde"].ToString();
                switch (j)
                {
                    case 1:
                    yuzdelikDegerleri.Add("haliYuzde", yuzdelik);
                    break;

                    case 2:
                    yuzdelikDegerleri.Add("pirlantaYuzde", yuzdelik);
                    break;

                    case 3:
                    yuzdelikDegerleri.Add("fanteziYuzde", yuzdelik);
                    break;

                    case 4:
                    yuzdelikDegerleri.Add("gumusYuzde", yuzdelik);
                    break;

                    case 5:
                    yuzdelikDegerleri.Add("altinYuzde", yuzdelik);
                    break;
                    
                    case 6:
                    yuzdelikDegerleri.Add("seramikYuzde", yuzdelik);
                    break;

                    case 7:
                    yuzdelikDegerleri.Add("deriYuzde", yuzdelik);
                    break;

                    case 8:
                    yuzdelikDegerleri.Add("hediyelikYuzde", yuzdelik);
                    break;

                    default: throw null;

                }
            }

            return Json(yuzdelikDegerleri, JsonRequestBehavior.AllowGet);
        }

        private ElResult ControlResultCalc(string inputId, int? id, int? satisSekli, string satisParite)
        {
            ElResult result = new ElResult();

            if (!id.HasValue)
                result.AddMessage("Personel Seçiniz.");
             
            if (string.IsNullOrWhiteSpace(satisParite))
                result.AddMessage("Satış Paritesi boş olamaz.");

            if (!satisSekli.HasValue)
                result.AddMessage("Satış şeklini belirtiniz.");

            if (!string.IsNullOrWhiteSpace(result.message))
                result.SetError();

            return result;
        }

        public ActionResult GetSearchStokModel()
        {
            DataRow[] stokList = GetTableStokList();
            ElSelectModel stokNoModel = new ElSelectModel(stokList, "deger", "kod");
            //stokNoModel.Id = "satisekrani_editor_select_stokNoModel" + inputId;
            stokNoModel.Class = "satisekrani_editor_select_stokNoModelClass";

            return PartialView("ElSearchSelect", stokNoModel);
        }

        public ActionResult GetSearchOdemeSekliModel()
        {
            DataRow[] tableRowsMuhasebeGiderHesabi = GetTableMuhasebeGiderHesabi();
            ElSelectModel modelMuhasebeGiderHesabi = new ElSelectModel(tableRowsMuhasebeGiderHesabi, "names", "id");
            modelMuhasebeGiderHesabi.Class = "satisekrani_editor_select_stokNoModelClass";

            return PartialView("ElSearchSelect", modelMuhasebeGiderHesabi);
        }
    }
}