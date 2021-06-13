using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using elrincon.web.Manager;
using elrincon.web.Models.PersonelBilgileri.Personel;
using elrincon.web.Models.SharedModel;

namespace elrincon.web.Controllers.PersonelBilgileri.Personel
{
    public class PersonelBaseController : BaseController
    {

        public PersonelEditorModel FillModel(int? id, string inputId)
        {
            PersonelEditorModel modelEditor = new PersonelEditorModel();
            modelEditor.ModelKardanOrani = new List<KardanYuzdelikOranlariModel>();

            ElInputModel modelAdi = new ElInputModel(InputDataType.Text);
            modelAdi.Id = "personel_editor_input_adi" + inputId;
            modelEditor.InputAdiModel = modelAdi;
            modelAdi.IsForQuery = true;

            ElInputModel modelSoyadi = new ElInputModel(InputDataType.Text);
            modelSoyadi.Id = "personel_editor_input_soyadi" + inputId;
            modelSoyadi.IsForQuery = true;
            modelEditor.InputSoyadiModel = modelSoyadi;

            ElInputModel modelTc = new ElInputModel(InputDataType.Text);
            modelTc.Id = "personel_editor_input_tc_no" + inputId;
            modelTc.IsForQuery = true;
            modelEditor.InputTcModel = modelTc;

            ElInputModel modelTelNo1 = new ElInputModel(InputDataType.Phone);
            modelTelNo1.Id = "personel_editor_input_tel_no1" + inputId;
            modelTelNo1.IsForQuery = true;
            modelEditor.InputTelNo1Model = modelTelNo1;

            ElInputModel modelTelNo2 = new ElInputModel(InputDataType.Phone);
            modelTelNo2.Id = "personel_editor_input_tel_no2" + inputId;
            modelEditor.InputTelNo2Model = modelTelNo2;

            ElInputModel modelGirisTarihi = new ElInputModel(InputDataType.Date);
            modelGirisTarihi.Id = "personel_editor_input_giris_tarihi" + inputId;
            modelGirisTarihi.Value = DateTime.Today;
            modelEditor.InputIseGirisTarihiModel = modelGirisTarihi;

            ElInputModel modelKullaniciAdi = new ElInputModel(InputDataType.Text);
            modelKullaniciAdi.Id = "personel_editor_input_kullanici_adi" + inputId;
            modelEditor.InputKullaniciAdiModel = modelKullaniciAdi;

            ElInputModel modelSifresi = new ElInputModel(InputDataType.Text);
            modelSifresi.Id = "personel_editor_input_sifre" + inputId ;
            modelEditor.InputSifresiModel = modelSifresi;

            ElInputModel modelMailAd = new ElInputModel(InputDataType.Text);
            modelMailAd.Id = "personel_editor_input_mail_adres" + inputId ;
            modelEditor.InputMailAdresiModel = modelMailAd;

            ElInputModel modelAdres = new ElInputModel(InputDataType.Text);
            modelAdres.Id = "personel_editor_input_adres" + inputId ;
            modelEditor.InputAdresiModel = modelAdres;

            DataRow[] tableRows = GetTable("el_liste_deger").Select("liste_id = 1");
            ElSelectModel selectBolumModel = new ElSelectModel(tableRows, "deger", "kod");
            selectBolumModel.Id = "personel_editor_model_select_bolum_id" + inputId ;
            selectBolumModel.IsForQuery = true;
            modelEditor.SelectBolumModel = selectBolumModel;

            DataRow[] tableRowsBolum = GetTable("el_liste_deger").Select("liste_id = 2");
            ElSelectModel selectSubeModel = new ElSelectModel(tableRowsBolum, "deger", "kod");
            selectSubeModel.Id = "personel_editor_model_select_sube_id" + inputId ;
            selectSubeModel.IsForQuery = true;
            modelEditor.SelectSubeModel = selectSubeModel;

            DataRow[] tableRowsMuhasebeGiderHesabi = GetTableMuhasebeGiderHesabi();
            ElSelectModel modelMuhasebeGiderHesabi = new ElSelectModel(tableRowsMuhasebeGiderHesabi, "names", "id");
            modelMuhasebeGiderHesabi.Id = "personel_editor_input_muhasebe_gider_hesabi" + inputId ;
            modelEditor.SelectMuhasebeMaasGiderHesabiModel = modelMuhasebeGiderHesabi;

            DataRow[] tableRowsMuhasebeGiderHesabi1 =
                 tableRowsMuhasebeGiderHesabi.Where(i => i[""].ToString().StartsWith("335")) as DataRow[] ;
//                tableRowsMuhasebeGiderHesabi.Where(r => searchList.Any(f => r.StartsWith(f))) as DataRow[];
            ElSelectModel modelMuhasebeKodu = new ElSelectModel(tableRowsMuhasebeGiderHesabi, "names", "id");
            modelMuhasebeKodu.Id = "personel_editor_input_muhasebe_kodu" + inputId ;
            modelEditor.SelectMuhasebeKoduModel = modelMuhasebeKodu;
             
            ElInputModel aciklama = new ElInputModel(InputDataType.TextArea);
            aciklama.Id = "personel_editor_input_aciklama" + inputId ;
            modelEditor.InputAciklamaModel = aciklama;

            DataRow[] rowsMaasTipi = GetTable("el_liste_deger").Select("liste_id = 4");
            ElSelectModel modelMaasTipi = new ElSelectModel(rowsMaasTipi, "deger", "kod");
            modelMaasTipi.Id = "personel_editor_model_select_maas_tipi" + inputId ;
            modelMaasTipi.IsForQuery = true;
            modelEditor.SelectMaasTipiModel = modelMaasTipi;

            ElInputModel modelMaasMiktari = new ElInputModel(InputDataType.Text);
            modelMaasMiktari.Id = "personel_editor_input_maas_miktari" + inputId ;
            modelMaasMiktari.IsForQuery = true;
            modelEditor.InputMaasMiktariModel = modelMaasMiktari;

            DataRow[] rowsDovizKodu = GetTable("el_liste_deger").Select("liste_id = 5");
            ElSelectModel modelDovizKodu = new ElSelectModel(rowsDovizKodu, "deger", "kod");
            modelDovizKodu.Id = "personel_editor_model_select_maas_doviz_kodu" + inputId ;
            modelEditor.SelectDovizKoduModel = modelDovizKodu;


            if (id.HasValue && id > 0)
            {
                ElDbTable table = GetTableRow(id.Value);

                DataRow row = table.Rows[0];

                modelAdi.Value = row["adi"];
                modelSoyadi.Value = row["soyadi"];
                modelKullaniciAdi.Value = row["kullanici_adi"];
                modelSifresi.Value = row["sifre"];
                modelTc.Value = row["tc_no"];
                modelTelNo1.Value = row["tel_no1"];
                modelTelNo2.Value = row["tel_no2"];
                modelMailAd.Value = row["mail_adres"];
                modelAdres.Value = row["adres"];
                modelMuhasebeGiderHesabi.SetSelectedCode(Convert.ToInt32(row["muhasebe_maas_gider_hesabi"]));//
                modelMuhasebeKodu.SetSelectedCode(Convert.ToInt32(row["muhasebe_kodu"]));
                modelMaasMiktari.Value = row["maas_miktari"];
                modelGirisTarihi.Value = row["ise_giris_tarihi"];
                aciklama.Value = row["aciklama"];
                modelDovizKodu.SetSelectedCode(Convert.ToInt32(row["doviz_kodu"]));
                selectBolumModel.SetSelectedCode(Convert.ToInt32(row["bolum"]));
                selectSubeModel.SetSelectedCode(Convert.ToInt32(row["subesi"]));
                modelMaasTipi.SetSelectedCode(Convert.ToInt32(row["maas_tipi"]));

                if (table.Rows.Count == 1)
                {
                    string tip = "1";
                    FillYuzdelikDefaultModel(modelEditor.ModelKardanOrani, tip, -2);
                }
                else
                {
                    foreach (DataRow dataRow in table.Rows)
                    {
                        if (!dataRow.IsNull("urunTipi"))
                            FillEmptyYuzdelikModel(modelEditor.ModelKardanOrani, dataRow);
                    }
                }
            }
            else
            {
                string tip = "1";
                FillYuzdelikDefaultModel(modelEditor.ModelKardanOrani, tip,-2);
            }

            FillEmptyYuzdelikModel(modelEditor.ModelKardanOrani, null);

            return modelEditor;
        }


        private ElDbTable GetTableRow(int id)
        {
            string sql =
                "select p.*, [personel_id],[urunTipi] ,[normalSatisParite1],yuzdelik_tip ,[normalSatisParite2],[normalSatisYuzdelik],[kapiSatisParite1],[kapiSatisParite2] " +
                ",[kapiSatisYuzdelik],[internetSatisParite1],[internetSatisParite2],[internetSatisYuzdelik],[yurtDisiSatisParite1],[yurtDisiSatisParite2] " +
                ",[yurtDisiSatisYuzdelik],[genelSatisParite1],[genelSatisParite2],[genelSatisYuzdelik],o.muhasebe_kodu as muhasebe_kodu_oran " +
                "from  el_personel p " +
                "left join el_kardan_yuzdelik_oranlari o on o.personel_id= p.id " +
                "where p.id = " + id;

            ElDbTable table = ExecuteTable(sql);

            //DataTable table = GetTable("el_personel");
            //DataRow row = table.Select("id=" + id).First();

            //return row;

            return table;
        }

    }
}