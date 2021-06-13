using System;
using System.Collections.Generic;
using System.Data;
using elrincon.web.Manager;
using elrincon.web.Models.PersonelBilgileri.Personel;
using elrincon.web.Models.SharedModel;
using elrincon.web.Models.Stok.HaliStok;

namespace elrincon.web.Controllers.Stok.HaliStok
{
    public class HaliStokBaseController : BaseController
    {

        // GET: HaliStokBase
        public HaliStokEditorModel FillModel(int? id, string inputId)
        {
            HaliStokEditorModel modelEditor = new HaliStokEditorModel();

            ElInputModel editorInputTarihModel = new ElInputModel(InputDataType.Date);
            editorInputTarihModel.Id = "hali_editor_input_tarih" + inputId;
            modelEditor.InputTarihModel = editorInputTarihModel;

            ElSelectModel selectSubeModel = GetDomainList(2);
            selectSubeModel.Id = "hali_editor_model_select_sube_id" + inputId;
            selectSubeModel.IsForQuery = true;
            modelEditor.SelectSubeModel = selectSubeModel;
            
            ElInputModel inputStokNoModel = new ElInputModel(InputDataType.Text);
            inputStokNoModel.Id = "hali_editor_input_stok" + inputId;
            inputStokNoModel.Disabled = true;
            modelEditor.InputStokNoModel = inputStokNoModel;

            ElSelectModel editorSelectAnaGrubu = GetDomainList(15);
            editorSelectAnaGrubu.Id = "hali_editor_model_select_ana_grubu" + inputId;
            editorSelectAnaGrubu.IsForQuery = true;
            modelEditor.SelectAnaGrubu = editorSelectAnaGrubu;

            ElSelectModel selectUrunCesidiModel = GetDomainList(7);
            selectUrunCesidiModel.Id = "hali_editor_model_select_urun_cesidi" + inputId;
            selectUrunCesidiModel.IsForQuery = true;
            modelEditor.SelectUrunCesidiModel = selectUrunCesidiModel;

            ElSelectModel selectMenseiModel = GetDomainList(8);
            selectMenseiModel.Id = "hali_editor_model_select_mensei" + inputId;
            selectMenseiModel.IsForQuery = true;
            modelEditor.SelectMenseiModel = selectMenseiModel;

            ElSelectModel uretimTipiModel = GetDomainList(9);
            uretimTipiModel.Id = "hali_editor_model_select_uretim_tip" + inputId;
            uretimTipiModel.IsForQuery = true;
            modelEditor.SelectUretimTipiModel = uretimTipiModel;

            ElSelectModel selectMaterialModel = GetDomainList(10);
            selectMaterialModel.Id = "hali_editor_model_select_materyal" + inputId;
            selectMaterialModel.IsForQuery = true;
            modelEditor.SelectMaterialModel= selectMaterialModel;

            ElSelectModel selectOlcuTipModel = GetDomainList(11);
            selectOlcuTipModel.Id = "hali_editor_model_select_olcu_tip" + inputId;
            selectOlcuTipModel.IsForQuery = true;
            modelEditor.SelectOlcuTipModel= selectOlcuTipModel;

            ElSelectModel selectDugumTipModel = GetDomainList(12);
            selectDugumTipModel.Id = "hali_editor_model_select_olcu_dugum" + inputId;
            selectDugumTipModel.IsForQuery = true;
            modelEditor.SelectDugumTipModel= selectDugumTipModel;

            ElInputModel inputOlcuBoyModel = new ElInputModel(InputDataType.Double);
            inputOlcuBoyModel.Id = "hali_editor_input_olcu_boy" + inputId;
            modelEditor.InputOlcuBoyModel = inputOlcuBoyModel;

            ElInputModel inputOlcuEnModel = new ElInputModel(InputDataType.Double);
            inputOlcuEnModel.Id = "hali_editor_input_olcu_en" + inputId;
            modelEditor.InputOlcuEnModel = inputOlcuEnModel;

            ElInputModel olcuMt2Model = new ElInputModel(InputDataType.Double);
            olcuMt2Model.Id = "hali_editor_input_olcu_mt2" + inputId;
            olcuMt2Model.Disabled = true;
            modelEditor.InputOlcuMt2Model = olcuMt2Model;

            ElInputModel modelEditorInputIncOlcuBoyModel = new ElInputModel(InputDataType.Double);
            modelEditorInputIncOlcuBoyModel.Id = "hali_editor_input_inc_olcu_boy" + inputId;
            modelEditorInputIncOlcuBoyModel.Disabled = true;
            modelEditor.InputIncOlcuBoyModel = modelEditorInputIncOlcuBoyModel;

            ElInputModel modelEditorInputIncOlcuEnModel = new ElInputModel(InputDataType.Double);
            modelEditorInputIncOlcuEnModel.Id = "hali_editor_input_olcu_inc_en" + inputId;
            modelEditorInputIncOlcuEnModel.Disabled = true;
            modelEditor.InputIncOlcuEnModel = modelEditorInputIncOlcuEnModel;

            ElInputModel modelEditorInputIncOlcuMt2Model = new ElInputModel(InputDataType.Double);
            modelEditorInputIncOlcuMt2Model.Id = "hali_editor_input_olcu_inc_mt2" + inputId;
            modelEditorInputIncOlcuMt2Model.Disabled = true;
            modelEditor.InputIncOlcuMt2Model = modelEditorInputIncOlcuMt2Model;

            //ElInputModel modelEditorInputDugumSayisiModel = new ElInputModel(InputDataType.Double);
            //modelEditorInputDugumSayisiModel.Id = "" + inputId;
            //modelEditor.InputDugumSayisiModel = modelEditorInputDugumSayisiModel;

            ElSelectModel editorInputDugumSayisiModel = GetDomainList(23);
            editorInputDugumSayisiModel.Id = "hali_editor_input_dugum_sayisi" + inputId;
            editorInputDugumSayisiModel.IsForQuery = true;
            modelEditor.InputDugumSayisiModel = editorInputDugumSayisiModel;

            //ElInputModel modelEditorInputRenkModel = new ElInputModel(InputDataType.Text);
            //modelEditorInputRenkModel.Id = "hali_editor_input_renk" + inputId;
            //modelEditor.InputRenkModel = modelEditorInputRenkModel;

            ElSelectModel editorInputDugumRenkModel = GetDomainList(24);
            editorInputDugumRenkModel.Id = "hali_editor_input_renk" + inputId;
            editorInputDugumRenkModel.IsForQuery = true;
            modelEditor.InputRenkModel = editorInputDugumRenkModel;

            ElSelectModel modelEditorSelectSaticiModel = GetDomainList(13);
            modelEditorSelectSaticiModel.Id = "hali_editor_model_select_satici" + inputId;
            modelEditorSelectSaticiModel.IsForQuery = true;
            modelEditor.SelectSaticiModel = modelEditorSelectSaticiModel;

            ElInputModel modelEditorInputFirmaKduModel = new ElInputModel(InputDataType.Text);
            modelEditorInputFirmaKduModel.Id = "hali_editor_input_firma_kodu" + inputId;
            modelEditor.InputFirmaKduModel = modelEditorInputFirmaKduModel;

            ElSelectModel modelEditorSelectKonsinyePesinModel = GetDomainList(14);
            modelEditorSelectKonsinyePesinModel.Id = "hali_editor_model_select_konsinye" + inputId;
            modelEditorSelectKonsinyePesinModel.IsForQuery = true;
            modelEditor.SelectKonsinyePesinModel = modelEditorSelectKonsinyePesinModel;

            ElInputModel editorInputToplamMaliyetModel = new ElInputModel(InputDataType.Double);
            editorInputToplamMaliyetModel.Id = "hali_editor_input_toplam_maliyet" + inputId;
            modelEditor.InputToplamMaliyetModel = editorInputToplamMaliyetModel;

            ElInputModel editorInputbirimFiyatModel = new ElInputModel(InputDataType.Double);
            editorInputbirimFiyatModel.Id = "hali_editor_input_birim_fiyat" + inputId; 
            modelEditor.InputbirimFiyatModel = editorInputbirimFiyatModel;

            ElInputModel editorInputEtiketCarpaniModel = new ElInputModel(InputDataType.Double);
            editorInputEtiketCarpaniModel.Id = "hali_editor_input_etiket_carpan" + inputId;
            modelEditor.InputEtiketCarpaniModel= editorInputEtiketCarpaniModel;

            ElInputModel editorInputAdetFiyatModel = new ElInputModel(InputDataType.Double);
            editorInputAdetFiyatModel.Id = "hali_editor_input_adet_fiyati" + inputId;
            modelEditor.InputAdetFiyatModel= editorInputAdetFiyatModel;

            ElInputModel inputEtiketFiyatModel = new ElInputModel(InputDataType.Double);
            inputEtiketFiyatModel.Id = "hali_editor_input_etiket_fiyati" + inputId;
            modelEditor.InputEtiketFiyatModel= inputEtiketFiyatModel;

            ElInputModel inputTamirYikamaModel = new ElInputModel(InputDataType.Double);
            inputTamirYikamaModel.Id = "hali_editor_input_tamir_yikama" + inputId;
            modelEditor.InputTamirYikamaModel= inputTamirYikamaModel;

            ElInputModel editorInputSatisCarpaniModel = new ElInputModel(1, InputDataType.Double);
            editorInputSatisCarpaniModel.Id = "hali_editor_input_satis_carpani" + inputId;
            modelEditor.InputSatisCarpaniModel= editorInputSatisCarpaniModel;
            editorInputSatisCarpaniModel.Disabled = true;

            ElInputModel editorInputSatisFiyatiModel = new ElInputModel(InputDataType.Double);
            editorInputSatisFiyatiModel.Id = "hali_editor_input_satis_fiyati" + inputId;
            modelEditor.InputSatisFiyatiModel= editorInputSatisFiyatiModel;

            ElInputModel editorInputAciklamaModel = new ElInputModel(InputDataType.TextArea);
            editorInputAciklamaModel.Id = "hali_editor_input_aciklama" + inputId;
            modelEditor.InputAciklamaModel= editorInputAciklamaModel;

            ElSelectModel modeleditorKonsinyeVerilen = GetPersonelList();
            modeleditorKonsinyeVerilen.Id = "hali_editor_model_select_konsinye_verilen" + inputId;
            modeleditorKonsinyeVerilen.IsForQuery = true;
            modelEditor.SelectKonsinyeVerilen = modeleditorKonsinyeVerilen;

            ElSelectModel editorSelectAlimDurumu = GetDomainList(16);
            editorSelectAlimDurumu.Id = "hali_editor_model_select_alimDurumu" + inputId;
            editorSelectAlimDurumu.IsForQuery = true;
            modelEditor.SelectAlimDurumu = editorSelectAlimDurumu;


            if (id.HasValue && id > 0)
            {
                ElDbTable table = GetTableRow(id.Value);

                DataRow row = table.Rows[0];

                editorInputTarihModel.Value = row["tarih"];
                inputStokNoModel.Value = row["stok_no"];
                editorSelectAnaGrubu.SetSelectedCode(Convert.ToInt32(1));/*row["ana_grup"])*/
                selectSubeModel.SetSelectedCode(Convert.ToInt32(row["subesi"]));
                selectUrunCesidiModel.SetSelectedCode(Convert.ToInt32(row["urun_cesidi"]));
                selectMenseiModel.SetSelectedCode(Convert.ToInt32(row["mensei"]));
                uretimTipiModel.SetSelectedCode(Convert.ToInt32(row["uretim_tipi"]));
                selectMaterialModel.SetSelectedCode(Convert.ToInt32(row["material"]));
                selectOlcuTipModel.SetSelectedCode(Convert.ToInt32(row["olcu_tip"]));
                selectDugumTipModel.SetSelectedCode(Convert.ToInt32(row["dugum_tip"]));
                inputOlcuBoyModel.Value = row["olcu_boy"];
                inputOlcuEnModel.Value = row["olcu_en"];
                olcuMt2Model.Value = row["olcu_mt2"];
                modelEditorInputIncOlcuBoyModel.Value = row["inc_olcu_boy"];
                modelEditorInputIncOlcuEnModel.Value = row["inc_olcu_en"];
                modelEditorInputIncOlcuMt2Model.Value = row["inc_olcu_mt2"];
                editorInputDugumSayisiModel.SetSelectedCode(Convert.ToInt32(row["dugum_sayisi"]));
                editorInputDugumRenkModel.SetSelectedCode (Convert.ToInt32(row["renk"]));
                editorSelectAlimDurumu.SetSelectedCode(Convert.ToInt32(row["satici"]));
                modelEditorInputFirmaKduModel.Value = row["firma"];
                editorInputToplamMaliyetModel.Value = row["toplam_maliyet"];
                editorInputbirimFiyatModel.Value = row["birim_fiyat"];
                editorInputAdetFiyatModel.Value = row["adet_fiyat"];

                if (Convert.ToInt32(row["birim_fiyat"]) > 0)
                    editorInputAdetFiyatModel.Disabled = true;

                if (Convert.ToInt32(row["adet_fiyat"]) > 0)
                    editorInputbirimFiyatModel.Disabled = true;

                editorInputEtiketCarpaniModel.Value = row["etiket_carpani"];
                inputEtiketFiyatModel.Value = row["etiket_fiyat"];
                inputTamirYikamaModel.Value = row["tamir_yikama"];
                editorInputSatisFiyatiModel.Value = row["satis_fiyati"];
                editorInputAciklamaModel.Value = row["aciklama"];
                modelEditorSelectKonsinyePesinModel.SetSelectedCode(Convert.ToInt32(row["konsinye_pesin"]));
            }
            else
            {
                string sql = "select top 1 * from el_hali_stok order by stok_no desc ";

                ElDbTable table = ExecuteTable(sql);
                int lastid = 100001;

                if (table.Rows.Count > 0)
                    lastid = Convert.ToInt32(table.Rows[0]["stok_no"])+1;
                inputStokNoModel.Value = lastid;
            }


            return modelEditor;
        }



        public ElSelectModel GetPersonelList()
        {
            string sql = "select concat (adi, ' ', soyadi) as ad, id from el_personel  ";//where durum != 0

            DataTable tableUrunCesidi = ExecuteTable(sql);
            ElSelectModel model = new ElSelectModel(tableUrunCesidi, "ad", "id");

            return model;
        }

        private ElDbTable GetTableRow(int id)
        {
            string sql =
                "select [id],subesi,[tarih],[stok_no],ana_grup,[urun_cesidi],[mensei],[uretim_tipi],[material],[olcu_tip],[dugum_tip]," +
                "[olcu_boy],[olcu_en],[olcu_mt2],[inc_olcu_boy],[inc_olcu_en],[inc_olcu_mt2],[dugum_sayisi],[renk]," +
                "[satici],[firma],[konsinye_pesin],[toplam_maliyet],[birim_fiyat],[etiket_carpani],[adet_fiyat],[etiket_fiyat],konsinye_verilen_id," +
                "[tamir_yikama],[satis_carpani],[satis_fiyati],[aciklama]  FROM  el_hali_stok p " +
                "where p.id = " + id;

            ElDbTable table = ExecuteTable(sql);

            //DataTable table = GetTable("el_personel");
            //DataRow row = table.Select("id=" + id).First();

            //return row;

            return table;
        }
    }
}