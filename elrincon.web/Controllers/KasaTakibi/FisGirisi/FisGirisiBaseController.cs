using System;
using System.Collections.Generic;
using System.Data;
using elrincon.web.Manager;
using elrincon.web.Models.KasaTakibi.FisGirisi;
using elrincon.web.Models.SharedModel;

namespace elrincon.web.Controllers.KasaTakibi.FisGirisi
{
    public class FisGirisiBaseController : BaseController
    {

        public FisGirisiEditorModel FillModel(int? id, string inputId)
        {
            FisGirisiEditorModel modelEditor = new FisGirisiEditorModel();
            modelEditor.ModelKardanOrani = new List<KardanYuzdelikOranlariModel>();

            ElInputModel modelAdi = new ElInputModel(InputDataType.Text);
            modelAdi.Id = "fis_girisi_editor_input_adi" + inputId;
            modelEditor.InputAdiModel = modelAdi;
            modelAdi.IsForQuery = true;

            ElInputModel modelSoyadi = new ElInputModel(InputDataType.Text);
            modelSoyadi.Id = "fis_girisi_editor_input_soyadi" + inputId;
            modelSoyadi.IsForQuery = true;
            modelEditor.InputSoyadiModel = modelSoyadi; 

            ElInputModel modelTelNo1 = new ElInputModel(InputDataType.Phone);
            modelTelNo1.Id = "fis_girisi_editor_input_tel_no1" + inputId;
            modelTelNo1.IsForQuery = true;
            modelEditor.InputTelNo1Model = modelTelNo1;
            
            ElInputModel inputFaxModel = new ElInputModel(InputDataType.Phone);
            inputFaxModel.Id = "fis_girisi_editor_input_fax" + inputId;
            inputFaxModel.IsForQuery = true;
            modelEditor.InputFaxModel = inputFaxModel;

            ElInputModel inputPostaKoduModel = new ElInputModel(InputDataType.Text);
            inputPostaKoduModel.Id = "fis_girisi_editor_input_posta_kodu" + inputId;
            inputPostaKoduModel.IsForQuery = true;
            modelEditor.InputPostaKoduModel = inputPostaKoduModel;

            ElInputModel modelTelNo2 = new ElInputModel(InputDataType.Phone);
            modelTelNo2.Id = "fis_girisi_editor_input_tel_no2" + inputId;
            modelEditor.InputTelNo2Model = modelTelNo2;
             
            ElInputModel modelMailAd = new ElInputModel(InputDataType.Text);
            modelMailAd.Id = "fis_girisi_editor_input_mail_adres" + inputId ;
            modelEditor.InputMailAdresiModel = modelMailAd;

            ElInputModel modelAdres = new ElInputModel(InputDataType.Text);
            modelAdres.Id = "fis_girisi_editor_input_adres" + inputId ;
            modelEditor.InputAdresiModel = modelAdres;
             
            DataRow[] tableRowssube = GetTable("el_liste_deger").Select("liste_id = 2");
            ElSelectModel selectSubeModel = new ElSelectModel(tableRowssube, "deger", "kod");
            selectSubeModel.Id = "fis_girisi_editor_model_select_sube_id" + inputId ;
            selectSubeModel.IsForQuery = true;
            modelEditor.SelectSubeModel = selectSubeModel;

            DataRow[] tableRowsUlke = GetTable("el_liste_deger").Select("liste_id = 5");
            ElSelectModel selectUlkeModel = new ElSelectModel(tableRowsUlke, "deger", "kod");
            selectUlkeModel.Id = "fis_girisi_editor_model_select_ulke_id" + inputId ;
            selectUlkeModel.IsForQuery = true;
            modelEditor.SelectUlkeModel = selectUlkeModel;

            DataRow[] tableRowsMuhasebeGiderHesabi = GetTableMuhasebeGiderHesabi();
            ElSelectModel modelMuhasebeGiderHesabi = new ElSelectModel(tableRowsMuhasebeGiderHesabi, "names", "id");
            modelMuhasebeGiderHesabi.Id = "fis_girisi_editor_input_muhasebe_kodu" + inputId;
            modelEditor.SelectMuhasebeMaasGiderHesabiModel = modelMuhasebeGiderHesabi;
             
            ElInputModel aciklama = new ElInputModel(InputDataType.TextArea);
            aciklama.Id = "fis_girisi_editor_input_aciklama" + inputId ;
            modelEditor.InputAciklamaModel = aciklama;
             
            DataRow[] rowsDovizKodu = GetTable("el_liste_deger").Select("liste_id = 5");
            ElSelectModel modelDovizKodu = new ElSelectModel(rowsDovizKodu, "deger", "kod");
            modelDovizKodu.Id = "fis_girisi_editor_model_select_maas_doviz_kodu" + inputId ;
            modelEditor.SelectDovizKoduModel = modelDovizKodu;


            if (id.HasValue && id > 0)
            {
                ElDbTable table = GetTableRow(id.Value);

                DataRow row = table.Rows[0];

                modelAdi.Value = row["adi"];
                modelSoyadi.Value = row["soyadi"];  
                modelTelNo1.Value = row["tel_no1"];
                inputFaxModel.Value = row["fax_no"];
                inputPostaKoduModel.Value = row["posta_kodu"];
                modelTelNo2.Value = row["tel_no2"];
                modelMailAd.Value = row["mail_adres"];
                modelAdres.Value = row["adres"];
                modelMuhasebeGiderHesabi.SetSelectedCode(Convert.ToInt32(row["muhasebe_kodu"]));
                aciklama.Value = row["aciklama"];
                modelDovizKodu.SetSelectedCode(Convert.ToInt32(row["doviz_kodu"]));
                selectSubeModel.SetSelectedCode(Convert.ToInt32(row["subesi"])); 
                selectUlkeModel.SetSelectedCode(Convert.ToInt32(row["ulke"])); 
                 
            } 
             

            return modelEditor;
        }

        private ElDbTable GetTableRow(int id)
        {
            string sql =
                "select p.* from  el_fis_girisi p " +
                "where p.id = " + id;

            ElDbTable table = ExecuteTable(sql);

            //DataTable table = GetTable("el_fis_girisi");
            //DataRow row = table.Select("id=" + id).First();

            //return row;

            return table;
        }

    }
}