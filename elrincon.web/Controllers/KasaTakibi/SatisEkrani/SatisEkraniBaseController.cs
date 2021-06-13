using System;
using System.Collections.Generic;
using System.Data;
using elrincon.web.Manager;
using elrincon.web.Models.KasaTakibi.SatisEkrani;
using elrincon.web.Models.SharedModel;
using elrincon.web.Models.Yonetim.SatisEkrani;

namespace elrincon.web.Controllers.KasaTakibi.SatisEkrani
{
    public class SatisEkraniBaseController : BaseController
    {

        public SatisEkraniEditorModel FillModel(int? id, string inputId)
        {
            SatisEkraniEditorModel modelEditor = new SatisEkraniEditorModel();
            modelEditor.SatisOdemeEditorModel = new SatisOdemeModel();

            ElInputModel inputTarihModel = new ElInputModel(DateTime.Now,InputDataType.DateTime);
            inputTarihModel.Id = "satisekrani_editor_input_tarih" + inputId;
            modelEditor.InputTarihModel = inputTarihModel;
            inputTarihModel.IsForQuery = true;

            ElInputModel adSoyadModel = new ElInputModel(InputDataType.Text);
            adSoyadModel.Id = "satisekrani_editor_input_InputAdSoyadModel" + inputId;
            modelEditor.InputAdSoyadModel = adSoyadModel;
            adSoyadModel.IsForQuery = true;
            
            ElInputModel editorInputSatisNoModel = new ElInputModel(InputDataType.Integer);
            editorInputSatisNoModel.Id = "satisekrani_editor_input_editorInputSatisNoModel" + inputId;
            modelEditor.InputSatisNoModel = editorInputSatisNoModel;
            editorInputSatisNoModel.IsForQuery = true;

            ElInputModel editorSatisTutariModel = new ElInputModel(InputDataType.Many);
            editorSatisTutariModel.Id = "satisekrani_editor_editorSatisTutariModel" + inputId;
            modelEditor.SatisTutariModel = editorSatisTutariModel;
            editorSatisTutariModel.IsForQuery = true;

            ElInputModel editorEtikekToplamiModel = new ElInputModel(InputDataType.Double);
            editorEtikekToplamiModel.Id = "satisekrani_editor_editorEtikekToplamiModel" + inputId;
            modelEditor.EtikekToplamiModel = editorEtikekToplamiModel;
            editorEtikekToplamiModel.IsForQuery = true;
            editorEtikekToplamiModel.Disabled = true;
            
            ElInputModel editorSatisParitesiModel = new ElInputModel(InputDataType.Double);//todo : otomatik dolacak; satış fiyetıyla etiket fiyatı oranlarından
            editorSatisParitesiModel.Id = "satisekrani_editor_editorSatisParitesiModel" + inputId;
            modelEditor.SatisParitesiModel= editorSatisParitesiModel;
            editorSatisParitesiModel.IsForQuery = true;
            editorSatisParitesiModel.Disabled = true;
            
            ElInputModel editorKargoUcretiModel = new ElInputModel(InputDataType.Double);
            editorKargoUcretiModel.Id = "satisekrani_editor_editorKargoUcretiModel" + inputId;
            modelEditor.KargoUcretiModel= editorKargoUcretiModel;
            editorKargoUcretiModel.IsForQuery = true;
            editorKargoUcretiModel.Disabled = true;

            DataRow[] tableRows = GetTable("el_liste_deger").Select("liste_id = 21");
            ElSelectModel inputUlkesiModel = new ElSelectModel(tableRows, "deger", "kod");
            inputUlkesiModel.Id = "satisekrani_editor_select_inputUlkesiModel" + inputId ;
            inputUlkesiModel.IsForQuery = true;
            modelEditor.UlkeModel = inputUlkesiModel;

            DataRow[] cirotableRows = GetTable("el_liste_deger").Select("liste_id = 25");
            ElSelectModel ciroDahilmiModel = new ElSelectModel(cirotableRows, "deger", "kod");
            ciroDahilmiModel.Id = "satisekrani_editor_select_iciroDahilmiModel" + inputId ;
            ciroDahilmiModel.IsForQuery = true; 
            ciroDahilmiModel.SetSelectedCode(1);
            modelEditor.CiroDahilmiModel = ciroDahilmiModel;

            DataRow[] paraCinsi = GetTable("el_liste_deger").Select("liste_id = 5");
            ElSelectModel editorSatisParaCinsiModel = new ElSelectModel(paraCinsi, "deger", "kod");
            editorSatisParaCinsiModel.Id = "satisekrani_editor_select_editorSatisParaCinsiModel" + inputId ;
            editorSatisParaCinsiModel.IsForQuery = true;
            modelEditor.SatisParaCinsiModel = editorSatisParaCinsiModel;

            ElSelectModel editorEtiketToplamiParaCinsiModel = new ElSelectModel(paraCinsi, "deger", "kod");
            editorEtiketToplamiParaCinsiModel.Id = "satisekrani_editor_select_editorSatisParaCinsiModel" + inputId ;
            editorEtiketToplamiParaCinsiModel.IsForQuery = true;
            editorEtiketToplamiParaCinsiModel.Disabled = true;
            modelEditor.EtiketToplamiParaCinsiModel = editorEtiketToplamiParaCinsiModel;

            ElSelectModel editorKargoUcretParaCinsiModel = new ElSelectModel(paraCinsi, "deger", "kod");
            editorKargoUcretParaCinsiModel.Id = "satisekrani_editor_select_editorKargoUcretParaCinsiModel" + inputId ;
            editorKargoUcretParaCinsiModel.IsForQuery = true;
            editorKargoUcretParaCinsiModel.Disabled = true;
            editorKargoUcretParaCinsiModel.SetSelectedCode(2);
            modelEditor.KargoUcretParaCinsiModel = editorKargoUcretParaCinsiModel;

            DataRow[] rezervasyonBilgi = GetTableRezervasyonList();
            ElSelectModel editorRezervasyonBilgiModel = new ElSelectModel(rezervasyonBilgi, "deger", "kod");
            editorRezervasyonBilgiModel.Id = "satisekrani_editor_select_editorRezervasyonBilgiModel" + inputId ;
            editorRezervasyonBilgiModel.IsForQuery = true;
            modelEditor.RezervasyonBilgiModel = editorRezervasyonBilgiModel;

            DataRow[] tableRowsSube = GetTable("el_liste_deger").Select("liste_id = 2");
            ElSelectModel editorSubeModel = new ElSelectModel(tableRowsSube, "deger", "kod");
            editorSubeModel.Id = "satisekrani_editor_select_inputSube" + inputId ;
            editorSubeModel.IsForQuery = true;
            modelEditor.SubeModel = editorSubeModel;

           // DataRow[] tableRowsSube = GetTable("el_liste_deger").Select("liste_id = 1");
            ElSelectModel editorKontratNoModel = new ElSelectModel();//new ElSelectModel(tableRowsSube, "deger", "kod");
            editorKontratNoModel.Id = "satisekrani_editor_select_editorKontratNo" + inputId ;
            editorKontratNoModel.IsForQuery = true;
            modelEditor.KontratNoModel = editorKontratNoModel;

            DataRow[] rehberData = GetTable("el_liste_deger").Select("liste_id = 22");
            ElSelectModel editorSatisSekliModel = new ElSelectModel(rehberData, "deger", "kod");
            editorSatisSekliModel.Id = "satisekrani_editor_select_editorSatisSekliModel" + inputId ;
            editorSatisSekliModel.IsForQuery = true;
            editorSatisSekliModel.SetSelectedCode(1);
            modelEditor.SatisSekliModel = editorSatisSekliModel;

            DataRow[] odemeSekli = GetTable("el_liste_deger").Select("liste_id = 3");
            ElSelectModel editorOdemeSekli = new ElSelectModel(odemeSekli, "deger", "kod");
            editorOdemeSekli.Id = "satisekrani_editor_select_inputOdemeSekliModel" + inputId ;
            editorOdemeSekli.IsForQuery = true;
            editorOdemeSekli.SetSelectedCode(1);
            modelEditor.OdemeSekli = editorOdemeSekli;

            //odeme list
            modelEditor.SatisOdemeEditorModel.SatisTipiMiktariModel = FillSatipTipiMiktarar(id, inputId);

            //taksit list 
            modelEditor.SatisOdemeEditorModel.SatisTaksitModel = FillTaksitModel(id,inputId);

            //list stok 
            modelEditor.StokListModel = FillStokListModel(id,inputId);

            //list personel 
            modelEditor.SatisPersonelleriList = FillPersonelListModel(id,inputId);

            ElInputModel aciklamaModel = new ElInputModel(InputDataType.TextArea);
            aciklamaModel.Id = "satisekrani_editor_input_aciklama" + inputId;
            modelEditor.InputAciklamaModel = aciklamaModel;
            aciklamaModel.IsForQuery = true;

            if (id.HasValue && id > 0)
            {
                ElDbTable table = GetTableRow(id.Value);

                DataRow row = table.Rows[0];

                editorKargoUcretiModel.Value = row["kargo_ucreti"];
                editorKargoUcretParaCinsiModel.SetSelectedCode(Convert.ToInt32(row["kargo_ucreti_para_cinsi"]));
                inputTarihModel.Value = row["satis_tarihi"];
                adSoyadModel.Value = row["alan_ad_soyad"];
                editorInputSatisNoModel.Value = row["id"];
                editorSatisTutariModel.Value = row["satis_tutari"];
                editorSatisParitesiModel.Value = row["satis_paritesi"];
                editorSatisParaCinsiModel.SetSelectedCode(Convert.ToInt32(row["satis_para_cinsi"]));
                editorEtikekToplamiModel.Value = row["etiket_toplami"];
                editorEtiketToplamiParaCinsiModel.SetSelectedCode(Convert.ToInt32(row["etiket_toplami_para_cinsi"]));
                aciklamaModel.Value = row["aciklama"];
                //editorKontratNoModel.SetSelectedCode(Convert.ToInt32(row["ulkesi"]));
                editorSatisSekliModel.SetSelectedCode(Convert.ToInt32(row["satis_tipi"]));
                editorSubeModel.SetSelectedCode(Convert.ToInt32(row["sube_id"]));
                editorRezervasyonBilgiModel.SetSelectedCode(Convert.ToInt32(row["rezervasyon_bilgi"]));
                editorOdemeSekli.SetSelectedCode(Convert.ToInt32(row["odeme_sekli"]));
                inputUlkesiModel.SetSelectedCode(Convert.ToInt32(row["ulkesi"]));

                modelEditor.SatisOdemeEditorModel.OdemeTipi = Convert.ToInt32(row["odeme_sekli"]);

            }
            else
            {
                string sql = "select top 1 * from el_satis order by id desc ";
                ElDbTable table = ExecuteTable(sql);
                int lastid = 1;
                
                if (table.Rows.Count > 0  )
                    lastid =  Convert.ToInt32(table.Rows[0]["id"])+ 1;
                editorInputSatisNoModel.Value = lastid ;

                modelEditor.SatisOdemeEditorModel.OdemeTipi = 1;//peşin
            }

            return modelEditor;
        }

        //ödemeleri doldur
        private List<SatisOdemeTipiMiktariModel> FillSatipTipiMiktarar(int? id, string inputId)
        {

            //editorSatisPesinOdenen.Value = Convert.ToDouble(row["odeme_pesin_miktari"]);
            //editorPesinOdenenCinsiModel.SetSelectedCode(Convert.ToInt32(row["odeme_pesin_para_cinsi"]));
            List<SatisOdemeTipiMiktariModel> modelList = new List<SatisOdemeTipiMiktariModel>();
            DataRow[] paraCinsi = GetTable("el_liste_deger").Select("liste_id = 5");

            if (!id.HasValue)
            {
                FillEmtyOdemeTipiModel(modelList, paraCinsi);
                return modelList;
            }
            
            string sql = "select * from el_satis_alinan_odeme where satis_id =" + id;
            ElDbTable table = ExecuteTable(sql);


            foreach (DataRow dataRow in table.Rows)
            {
                SatisOdemeTipiMiktariModel modelEditor = new SatisOdemeTipiMiktariModel();

                DataRow[] tableRowsMuhasebeGiderHesabi = GetTableMuhasebeGiderHesabi();
                ElSelectModel modelMuhasebeGiderHesabi = new ElSelectModel(tableRowsMuhasebeGiderHesabi, "names", "id");
                modelEditor.OdemeTipiCinsi = modelMuhasebeGiderHesabi;

                ElInputModel editorSatisPesinOdenen = new ElInputModel(InputDataType.Many);
                editorSatisPesinOdenen.Id = "satisekrani_editor_editorSatisPesinOdenenModel" + inputId;
                modelEditor.OdemeMiktari = editorSatisPesinOdenen;

                ElSelectModel editorPesinOdenenCinsiModel = new ElSelectModel(paraCinsi, "deger", "kod");
                editorPesinOdenenCinsiModel.Id = "satisekrani_editor_select_editorPesinOdenenCinsiModel" + inputId;
                editorPesinOdenenCinsiModel.IsForQuery = true;
                modelEditor.OdemeParaCinsi = editorPesinOdenenCinsiModel; 

                modelList.Add(modelEditor);
            }

            if (table.Rows.Count == 0)
                FillEmtyOdemeTipiModel(modelList, paraCinsi);

            return modelList;
        }

        //taksitleri doldur
        private List<SatisOdemeTaksitModel> FillTaksitModel(int? id, string inputId)
        {
            List<SatisOdemeTaksitModel> modelList = new List<SatisOdemeTaksitModel>();
            DataRow[] paraCinsi = GetTable("el_liste_deger").Select("liste_id = 5");

            if (!id.HasValue)
            {
                FillEmtyTaksitModel(modelList, paraCinsi);
                return modelList;
            }
            
            string sql = "select * from el_satis_taksitler where satis_id =" + id;
            ElDbTable table = ExecuteTable(sql);


            foreach (DataRow dataRow in table.Rows)
            {
                SatisOdemeTaksitModel taksit = new SatisOdemeTaksitModel();


                ElInputModel editorSatisPesinOdenen = new ElInputModel(dataRow["taksit_miktari"].ToString(), InputDataType.Many);
                taksit.OdenecekMiktari = editorSatisPesinOdenen;

                ElSelectModel editorPesinOdenenCinsiModel = new ElSelectModel(paraCinsi, "deger", "kod");
                taksit.OdenecekParaCinsi = editorPesinOdenenCinsiModel;
                editorPesinOdenenCinsiModel.SetSelectedCode(Convert.ToInt32(dataRow["taksit_para_cinsi"]));

                modelList.Add(taksit);
            }

            if (table.Rows.Count == 0)
                FillEmtyTaksitModel(modelList, paraCinsi);

            return modelList;
        }

        private void FillEmtyTaksitModel(List<SatisOdemeTaksitModel> modelList, DataRow[] paraCinsi)
        {
            SatisOdemeTaksitModel taksit = new SatisOdemeTaksitModel();

            ElInputModel editorSatisPesinOdenen = new ElInputModel(InputDataType.Many);
            taksit.OdenecekMiktari = editorSatisPesinOdenen;

            ElSelectModel editorPesinOdenenCinsiModel = new ElSelectModel(paraCinsi, "deger", "kod");
            taksit.OdenecekParaCinsi = editorPesinOdenenCinsiModel;

            modelList.Add(taksit);
        }

        private void FillEmtyOdemeTipiModel(List<SatisOdemeTipiMiktariModel> modelList, DataRow[] paraCinsi)
        {
            SatisOdemeTipiMiktariModel modelEditor = new SatisOdemeTipiMiktariModel();

            DataRow[] tableRowsMuhasebeGiderHesabi = GetTableMuhasebeGiderHesabi();
            ElSelectModel modelMuhasebeGiderHesabi = new ElSelectModel(tableRowsMuhasebeGiderHesabi, "names", "id");
            modelEditor.OdemeTipiCinsi = modelMuhasebeGiderHesabi;

            ElInputModel editorSatisPesinOdenen = new ElInputModel(InputDataType.Many);
            modelEditor.OdemeMiktari = editorSatisPesinOdenen;

            ElSelectModel editorPesinOdenenCinsiModel = new ElSelectModel(paraCinsi, "deger", "kod");
            modelEditor.OdemeParaCinsi = editorPesinOdenenCinsiModel;

            modelList.Add(modelEditor);
        }

        //stok liste modelini doldurur
        private List<StokList> FillStokListModel(int? id, string inputId)
        {
            List<StokList>  stokLists = new List<StokList>();
            
            StokList stok = new StokList();

            if (!id.HasValue)
                id = -1;
            string sql = "select * from el_satis_stok_listesi where satis_id = " + id;
            
            ElDbTable table = ExecuteTable(sql);
            if (table.Rows.Count > 0)
                FillFromDbSatisStokBilgi(stok,stokLists,id,inputId,table);
            else FillEmptySatisStokBilgi(stok, stokLists, id, inputId);
          
            return stokLists;
        }

        //empty stok list
        private void FillEmptySatisStokBilgi(StokList stok, List<StokList> stokLists, int? id, string inputId)
        {
            DataRow[] odemeSekli = GetTableStokList();
            ElSelectModel stokNoModel = new ElSelectModel(odemeSekli, "deger", "kod");
            //stokNoModel.Id = "satisekrani_editor_select_stokNoModel" + inputId;
            stokNoModel.Class = "satisekrani_editor_select_stokNoModelClass";
            stokNoModel.IsForQuery = true;
            stok.StokNoModel = stokNoModel;

            ElInputModel alisTarihiModel = new ElInputModel(InputDataType.Text);
            //alisTarihiModel.Id = "satisekrani_editor_stokAlisTarihiModel" + inputId;
            stok.StokUrunAdiModel = alisTarihiModel;
            alisTarihiModel.IsForQuery = true;
            alisTarihiModel.Disabled = true;

            ElInputModel fiyatiModel = new ElInputModel(InputDataType.Double);
            //fiyatiModel.Id = "satisekrani_editor_stokfiyatiModel" + inputId;
            stok.StokFiyatiModel = fiyatiModel;
            fiyatiModel.IsForQuery = true;
            fiyatiModel.Disabled = true;

            ElInputModel enModel = new ElInputModel(InputDataType.Double);
            //enModel.Id = "satisekrani_editor_stokEnModel" + inputId;
            stok.StokEnModel = enModel;
            enModel.IsForQuery = true;
            enModel.Disabled = true;

            ElInputModel boyModel = new ElInputModel(InputDataType.Double);
            //boyModel.Id = "satisekrani_editor_stokBoyModel" + inputId;
            stok.StokBoyModel = boyModel;
            boyModel.IsForQuery = true;
            boyModel.Disabled = true;

            stokLists.Add(stok);
        }

        //full stok
        private void FillFromDbSatisStokBilgi(StokList stok, List<StokList> stokLists, int? id, string inputId,
            ElDbTable table)
        {
            foreach (DataRow tableRow in table.Rows)
            {
                stok = new StokList();

                DataRow[] stokList = GetTableStokList();
                ElSelectModel stokNoModel = new ElSelectModel(stokList, "deger", "kod");
                //stokNoModel.Id = "satisekrani_editor_select_stokNoModel" + inputId;
                stokNoModel.Class = "satisekrani_editor_select_stokNoModelClass";
                stokNoModel.IsForQuery = true; 
                stokNoModel.SetSelectedCode(Convert.ToInt32(tableRow["stok_id"]));
                stok.StokNoModel = stokNoModel;

                ElInputModel alisTarihiModel = new ElInputModel(InputDataType.Text);
                //alisTarihiModel.Id = "satisekrani_editor_stokAlisTarihiModel" + inputId;
                stok.StokUrunAdiModel = alisTarihiModel;
                alisTarihiModel.Value = Convert.ToDateTime(tableRow["stok_alis_tarihi"]);
                alisTarihiModel.IsForQuery = true;
                alisTarihiModel.Disabled = true;

                ElInputModel fiyatiModel = new ElInputModel(InputDataType.Double);
                //fiyatiModel.Id = "satisekrani_editor_stokfiyatiModel" + inputId;
                stok.StokFiyatiModel = fiyatiModel;
                fiyatiModel.Value = Convert.ToDouble(tableRow["stok_fiyati"]);
                fiyatiModel.IsForQuery = true;
                fiyatiModel.Disabled = true;

                ElInputModel enModel = new ElInputModel(InputDataType.Double);
                //enModel.Id = "satisekrani_editor_stokEnModel" + inputId;
                stok.StokEnModel = enModel;
                enModel.Value = Convert.ToDouble(tableRow["stok_en"]);
                enModel.IsForQuery = true;
                enModel.Disabled = true;

                ElInputModel boyModel = new ElInputModel(InputDataType.Double);
                //boyModel.Id = "satisekrani_editor_stokBoyModel" + inputId;
                stok.StokBoyModel = boyModel;
                boyModel.Value = Convert.ToDouble(tableRow["stok_boy"]);
                boyModel.IsForQuery = true;
                boyModel.Disabled = true;

                stokLists.Add(stok);
            }
           
        }

        //personel lstesini dolduru
        private List<SatisPersonelleri> FillPersonelListModel(int? id, string inputId)
        {
            List<SatisPersonelleri>  personelListModel = new List<SatisPersonelleri>();

            for (int i = 0; i < 10; i++)
            {
                SatisPersonelleri personel = new SatisPersonelleri();

                DataRow[] personelFilterListe = GetTableHanutcuList(i);
                ElSelectModel personeltipBilgiModel = new ElSelectModel(personelFilterListe, "deger", "kod");
                personeltipBilgiModel.Id = "satisekrani_editor_select_personeltipBilgiModel_"+ i + inputId;
                personeltipBilgiModel.Class = "satisekrani_editor_select_personelyuzdelik";
                personeltipBilgiModel.IsForQuery = true;
                personel.PersoneltipBilgiModel = personeltipBilgiModel;
               
                ElInputModel haliYuzdelikModel = new ElInputModel(InputDataType.Double);
                //haliYuzdelikModel.Id = "satisekrani_editor_haliYuzdelikModel" + inputId;
                personel.HaliYuzdelikModel = haliYuzdelikModel;
                haliYuzdelikModel.IsForQuery = true;

                ElInputModel pirlantaYuzdelikModel = new ElInputModel(InputDataType.Double);
                //pirlantaYuzdelikModel.Id = "satisekrani_editor_pirlantaYuzdelikModel" + inputId;
                personel.PirlantaYuzdelikModel = pirlantaYuzdelikModel;
                pirlantaYuzdelikModel.IsForQuery = true;

                ElInputModel fanteziYuzdelikModel = new ElInputModel(InputDataType.Double);
                //fanteziYuzdelikModel.Id = "satisekrani_editor_fanteziYuzdelikModel" + inputId;
                personel.FanteziYuzdelikModel = fanteziYuzdelikModel;
                fanteziYuzdelikModel.IsForQuery = true;

                ElInputModel altınYuzdelikModel = new ElInputModel(InputDataType.Double);
                //altınYuzdelikModel.Id = "satisekrani_editor_altınYuzdelikModel" + inputId;
                personel.AltınYuzdelikModel = altınYuzdelikModel;
                altınYuzdelikModel.IsForQuery = true;

                ElInputModel seramikYuzdelikModel = new ElInputModel(InputDataType.Double);
                //seramikYuzdelikModel.Id = "satisekrani_editor_seramikYuzdelikModel" + inputId;
                personel.SeramikYuzdelikModel = seramikYuzdelikModel;
                seramikYuzdelikModel.IsForQuery = true;

                ElInputModel gumusYuzdelikModel = new ElInputModel(InputDataType.Double);
                //gumusYuzdelikModel.Id = "satisekrani_editor_gumusYuzdelikModel" + inputId;
                personel.GumusYuzdelikModel = gumusYuzdelikModel;
                gumusYuzdelikModel.IsForQuery = true;

                ElInputModel deriYuzdelikModel = new ElInputModel(InputDataType.Double);
                //deriYuzdelikModel.Id = "satisekrani_editor_deriYuzdelikModel" + inputId;
                personel.DeriYuzdelikModel = deriYuzdelikModel;
                deriYuzdelikModel.IsForQuery = true;

                ElInputModel hediyelikYuzdelikModel = new ElInputModel(InputDataType.Double);
                //hediyelikYuzdelikModel.Id = "satisekrani_editor_hediyelikYuzdelikModel" + inputId;
                personel.HediyelikYuzdelikModel = hediyelikYuzdelikModel;
                hediyelikYuzdelikModel.IsForQuery = true;

                personelListModel.Add(personel);
            }

            return personelListModel;
        }


        private ElDbTable GetTableRow(int id)
        {
            string sql =
                "select * from el_satis where id =" + id;

            ElDbTable table = ExecuteTable(sql);

            //DataTable table = GetTable("el_personel");
            //DataRow row = table.Select("id=" + id).First();

            //return row;

            return table;
        }

        public DataRow[] GetTablRehberList()
        {
            string sql = "select concat(adi, ' ', soyadi) as adi, id " +
                         "from el_rehber where durum != 0 or durum is null ";

            ElDbTable table = ExecuteTable(sql);

            return table.Select();
        }
        
        public DataRow[] GetTableHanutcuList(int i)
        {
            string sql = "";

            //tezgahtar
            if (i == 0 || i==1)
                sql = "select concat(adi, ' ', soyadi) as deger, id as kod " +
                         "from el_personel " +
                         "where (durum != 0 or durum is null)  and bolum = 2 ";
            //hanutçu
            else if(i==5 || i==6)
                sql = "select concat(adi, ' ', soyadi) as deger, id as kod " +
                         "from el_personel " +
                         "where (durum != 0 or durum is null)  and bolum = 0 ";

            //acente
            else if(i==2)
                sql = "select concat(adi, ' ', sehir) as deger, id as kod from el_acenta ";

            //rehber
            //if(i==3 || i==4)
             else sql = "select concat(adi, ' ', soyadi) as deger, id as kod  from el_rehber ";
             

            ElDbTable table = ExecuteTable(sql);
            table.Orderby = "deger";
            return table.Select();
        }

        public DataRow[] GetTableStokList()
        {
            string sql = "select CONCAT(stok_no, ' ', olcu_mt2,'mt2') as deger, stok_no as kod  from el_hali_stok";

            ElDbTable table = ExecuteTable(sql);
            table.Orderby = "kod desc";

            return table.Select();
        }

        public DataRow[] GetTableRezervasyonList()
        {
            string sql = "select concat(acenta, ' ', rezervasyon_no) as deger, rezervasyon_no as kod  from el_rezervasyon ";

            ElDbTable table = ExecuteTable(sql);

            return table.Select();
        }


    }
}