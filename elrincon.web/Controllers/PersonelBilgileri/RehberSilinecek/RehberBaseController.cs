using System;
using System.Collections.Generic;
using System.Data;
using elrincon.web.Manager;
using elrincon.web.Models.PersonelBilgileri.Rehber;
using elrincon.web.Models.RehberBilgileri.Rehber;
using elrincon.web.Models.SharedModel;

namespace elrincon.web.Controllers.PersonelBilgileri.Rehber
{
    public class RehberBaseController : BaseController
    {

        public RehberEditorModel FillModel(int? id, string inputId)
        {
            RehberEditorModel modelEditor = new RehberEditorModel();
            modelEditor.ModelKardanOrani = new List<KardanYuzdelikOranlariModel>();

            ElInputModel modelAdi = new ElInputModel(InputDataType.Text);
            modelAdi.Id = "rehber_editor_input_adi" + inputId;
            modelEditor.InputAdiModel = modelAdi;
            modelAdi.IsForQuery = true;

            ElInputModel modelSoyadi = new ElInputModel(InputDataType.Text);
            modelSoyadi.Id = "rehber_editor_input_soyadi" + inputId;
            modelSoyadi.IsForQuery = true;
            modelEditor.InputSoyadiModel = modelSoyadi; 

            ElInputModel modelTelNo1 = new ElInputModel(InputDataType.Phone);
            modelTelNo1.Id = "rehber_editor_input_tel_no1" + inputId;
            modelTelNo1.IsForQuery = true;
            modelEditor.InputTelNo1Model = modelTelNo1;
            
            ElInputModel inputFaxModel = new ElInputModel(InputDataType.Phone);
            inputFaxModel.Id = "rehber_editor_input_fax" + inputId;
            inputFaxModel.IsForQuery = true;
            modelEditor.InputFaxModel = inputFaxModel;

            ElInputModel inputPostaKoduModel = new ElInputModel(InputDataType.Text);
            inputPostaKoduModel.Id = "rehber_editor_input_posta_kodu" + inputId;
            inputPostaKoduModel.IsForQuery = true;
            modelEditor.InputPostaKoduModel = inputPostaKoduModel;

            ElInputModel modelTelNo2 = new ElInputModel(InputDataType.Phone);
            modelTelNo2.Id = "rehber_editor_input_tel_no2" + inputId;
            modelEditor.InputTelNo2Model = modelTelNo2;
             
            ElInputModel modelMailAd = new ElInputModel(InputDataType.Text);
            modelMailAd.Id = "rehber_editor_input_mail_adres" + inputId ;
            modelEditor.InputMailAdresiModel = modelMailAd;

            ElInputModel modelAdres = new ElInputModel(InputDataType.Text);
            modelAdres.Id = "rehber_editor_input_adres" + inputId ;
            modelEditor.InputAdresiModel = modelAdres;
             
            DataRow[] tableRowssube = GetTable("el_liste_deger").Select("liste_id = 2");
            ElSelectModel selectSubeModel = new ElSelectModel(tableRowssube, "deger", "kod");
            selectSubeModel.Id = "rehber_editor_model_select_sube_id" + inputId ;
            selectSubeModel.IsForQuery = true;
            modelEditor.SelectSubeModel = selectSubeModel;

            DataRow[] tableRowsUlke= GetTable("el_liste_deger").Select("liste_id = 2");
            ElSelectModel selectUlkeModel = new ElSelectModel(tableRowsUlke, "deger", "kod");
            selectUlkeModel.Id = "rehber_editor_model_select_ulke_id" + inputId ;
            selectUlkeModel.IsForQuery = true;
            modelEditor.SelectUlkeModel = selectUlkeModel;
             
            ElInputModel modelMuhasebeKodu = new ElInputModel(InputDataType.Text);
            modelMuhasebeKodu.Id = "rehber_editor_input_muhasebe_kodu" + inputId ;
            modelEditor.InputMuhasebeKoduModel = modelMuhasebeKodu;

            ElInputModel aciklama = new ElInputModel(InputDataType.TextArea);
            aciklama.Id = "rehber_editor_input_aciklama" + inputId ;
            modelEditor.InputAciklamaModel = aciklama;
             
            DataRow[] rowsDovizKodu = GetTable("el_liste_deger").Select("liste_id = 5");
            ElSelectModel modelDovizKodu = new ElSelectModel(rowsDovizKodu, "deger", "kod");
            modelDovizKodu.Id = "rehber_editor_model_select_maas_doviz_kodu" + inputId ;
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
                modelMuhasebeKodu.Value = row["muhasebe_kodu"]; 
                aciklama.Value = row["aciklama"];
                modelDovizKodu.SetSelectedCode(Convert.ToInt32(row["doviz_kodu"]));
                selectSubeModel.SetSelectedCode(Convert.ToInt32(row["subesi"])); 
                selectUlkeModel.SetSelectedCode(Convert.ToInt32(row["ulke"])); 

                if (table.Rows.Count == 1)
                {
                    string tip = "1";
                    FillYuzdelikDefaultModelRehber(modelEditor.ModelKardanOrani, tip, -2);
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
                FillYuzdelikDefaultModelRehber(modelEditor.ModelKardanOrani, tip,-2);
            }

            FillEmptyYuzdelikModel(modelEditor.ModelKardanOrani, null);

            return modelEditor;
        }


        /// <summary>
        /// personel ve hanutcu yüzdeliklerinin belirlenmesi
        /// </summary>
        /// <param name="modelEditorModelKardanOrani"></param>
        /// <param name="tip"></param>
        public void FillYuzdelikDefaultModelRehber(List<KardanYuzdelikOranlariModel> modelEditorModelKardanOrani, string tip, int personelid)
        {
            string sql =
                "select *,muhasebe_kodu as muhasebe_kodu_oran ,ROW_NUMBER() OVER (ORDER BY urunTipi ) AS RowNum " +
                "from el_kardan_yuzdelik_oranlari_rehber " +
                "where yuzdelik_tip in (" + tip + ") and rehber_id = " + personelid;

            ElDbTable table = ExecuteTable(sql);

            foreach (DataRow dataRow in table.Rows)
            {
                if (!dataRow.IsNull("urunTipi"))
                    FillEmptyYuzdelikModel(modelEditorModelKardanOrani, dataRow);
            }
        }

        public void FillEmptyYuzdelikModelRehber(List<KardanYuzdelikOranlariModel> modelEditorModelKardanOrani, DataRow dataRow)
        {
            KardanYuzdelikOranlariModel model = new KardanYuzdelikOranlariModel();

            model.YuzdelikTipi = 0;
            DataRow[] urunTipileri = GetTable("el_liste_deger").Select("liste_id = 6");
            model.UrunTipi = new ElSelectModel(urunTipileri, "deger", "kod");

            //model.NarmalSatisParite1 = new ElInputModel(InputDataType.Double);
            //model.NarmalSatisParite2 = new ElInputModel(InputDataType.Double);
            model.NarmalSatisYuzdeligi = new ElInputModel(InputDataType.Double);

            //model.KapiSatisParite1 = new ElInputModel(InputDataType.Double);
            //model.KapiSatisParite2 = new ElInputModel(InputDataType.Double);
            //model.KapiSatisYuzdeligi = new ElInputModel(InputDataType.Double);

            //model.InternetSatisParite1 = new ElInputModel(InputDataType.Double);
            //model.InternetSatisParite2 = new ElInputModel(InputDataType.Double);
            //model.InternetSatisYuzdeligi = new ElInputModel(InputDataType.Double);

            //model.YurtDisiSatisParite1 = new ElInputModel(InputDataType.Double);
            //model.YurtDisiSatisParite2 = new ElInputModel(InputDataType.Double);
            //model.YurtDisiSatisYuzdeligi = new ElInputModel(InputDataType.Double);

            //model.GenelCiroSatisParite1 = new ElInputModel(InputDataType.Double);
            //model.GenelCiroSatisParite2 = new ElInputModel(InputDataType.Double);
            //model.GenelCiroSatisYuzdeligi = new ElInputModel(InputDataType.Double);
            //model.MuhasebeGelirKodu = new ElInputModel(InputDataType.Double);

            if (dataRow != null && !dataRow.IsNull("urunTipi"))
            {
                model.YuzdelikTipi = !dataRow.IsNull("yuzdelik_tip") ? Convert.ToInt32(dataRow["yuzdelik_tip"]) : 0;

                model.UrunTipi.SetSelectedCode(Convert.ToInt32(dataRow["urunTipi"]));
                //model.NarmalSatisParite1.Value = dataRow["normalSatisParite1"];
                //model.NarmalSatisParite2.Value = dataRow["normalSatisParite2"];
                model.NarmalSatisYuzdeligi.Value = dataRow["normalSatisYuzdesi"];

                //model.KapiSatisParite1.Value = dataRow["kapiSatisParite1"];
                //model.KapiSatisParite2.Value = dataRow["kapiSatisParite2"];
                //model.KapiSatisYuzdeligi.Value = dataRow["kapiSatisYuzdelik"];

                //model.InternetSatisParite1.Value = dataRow["internetSatisParite1"];
                //model.InternetSatisParite2.Value = dataRow["internetSatisParite2"];
                //model.InternetSatisYuzdeligi.Value = dataRow["internetSatisYuzdelik"];

                //model.YurtDisiSatisParite1.Value = dataRow["yurtDisiSatisParite1"];
                //model.YurtDisiSatisParite2.Value = dataRow["yurtDisiSatisParite2"];
                //model.YurtDisiSatisYuzdeligi.Value = dataRow["yurtDisiSatisYuzdelik"];

                //model.GenelCiroSatisParite1.Value = dataRow["genelSatisParite1"];
                //model.GenelCiroSatisParite2.Value = dataRow["genelSatisParite2"];
                //model.GenelCiroSatisYuzdeligi.Value = dataRow["genelSatisYuzdelik"];
            }

            modelEditorModelKardanOrani.Add(model);
        }
        private ElDbTable GetTableRow(int id)
        {
            string sql =
                "select p.*, [rehber_id],[urunTipi] ,[normalSatisParite1],yuzdelik_tip ,[normalSatisParite2],[normalSatisYuzdesi],[kapiSatisParite1],[kapiSatisParite2] " +
                ",[kapiSatisYuzdelik],[internetSatisParite1],[internetSatisParite2],[internetSatisYuzdelik],[yurtDisiSatisParite1],[yurtDisiSatisParite2] " +
                ",[yurtDisiSatisYuzdelik],[genelSatisParite1],[genelSatisParite2],[genelSatisYuzdelik] omuhasebe_kodu as muhasebe_kodu_oran " +
                " from  el_rehber p " +
                "left join el_kardan_yuzdelik_oranlari_rehber o on o.rehber_id= p.id " +
                "where p.id = " + id;

            ElDbTable table = ExecuteTable(sql);

            //DataTable table = GetTable("el_rehber");
            //DataRow row = table.Select("id=" + id).First();

            //return row;

            return table;
        }

    }
}