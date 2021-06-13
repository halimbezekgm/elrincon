using System;
using System.Data;
using elrincon.web.Manager;
using elrincon.web.Models.FirmaDisiKayitlar.MusteriBilgileri;
using elrincon.web.Models.SharedModel;  

namespace elrincon.web.Controllers.FirmaDisiKayitlar.MusteriBilgileri
{
    public class MusteriBilgileriBaseController : BaseController
    {

        public MusteriBilgileriEditorModel FillModel(int? id, string inputId)
        {
            MusteriBilgileriEditorModel modelEditor = new MusteriBilgileriEditorModel(); 

            ElInputModel inputEyaletModel = new ElInputModel(InputDataType.Text);
            inputEyaletModel.Id = "MusteriBilgileri_editor_input_inputEyaletModel" + inputId;
            modelEditor.InputEyaletModel = inputEyaletModel;
            inputEyaletModel.IsForQuery = true;

            ElInputModel inputSoyadModel = new ElInputModel(InputDataType.Text);
            inputSoyadModel.Id = "MusteriBilgileri_editor_input_inputSoyadModel" + inputId;
            modelEditor.InputSoyadModel = inputSoyadModel;
            inputSoyadModel.IsForQuery = true;

            ElInputModel inputAdModel = new ElInputModel(InputDataType.Text);
            inputAdModel.Id = "MusteriBilgileri_editor_input_inputAdModel" + inputId;
            modelEditor.InputAdModel = inputAdModel;
            inputAdModel.IsForQuery = true;

            ElInputModel inputFaxModel = new ElInputModel(InputDataType.Text);
            inputFaxModel.Id = "MusteriBilgileri_editor_input_inputFaxModel" + inputId;
            modelEditor.InputFaxModel = inputFaxModel;
            inputFaxModel.IsForQuery = true;
            
            ElInputModel inputPostaKoduModel = new ElInputModel(InputDataType.Integer);
            inputPostaKoduModel.Id = "MusteriBilgileri_editor_input_inputPostaKoduModel" + inputId;
            modelEditor.InputPostaKoduModel = inputPostaKoduModel;
            inputPostaKoduModel.IsForQuery = true;

            ElInputModel inputSehirModel = new ElInputModel(InputDataType.Text);
            inputSehirModel.Id = "MusteriBilgileri_editor_input_inputSehirModel" + inputId;
            modelEditor.InputSehirModel = inputSehirModel;
            inputSehirModel.IsForQuery = true;
            
            ElInputModel inputEmailModel = new ElInputModel(InputDataType.Text);
            inputEmailModel.Id = "MusteriBilgileri_editor_input_inputEmailModel" + inputId;
            modelEditor.InputEmailModel = inputEmailModel;
            inputEmailModel.IsForQuery = true;
            
            ElInputModel inputAdresModel = new ElInputModel(InputDataType.Text);
            inputAdresModel.Id = "MusteriBilgileri_editor_input_inputAdresModel" + inputId;
            modelEditor.InputAdresModel = inputAdresModel;
            inputAdresModel.IsForQuery = true;

            ElInputModel inputTaxOfficeModel = new ElInputModel(InputDataType.Text);
            inputTaxOfficeModel.Id = "MusteriBilgileri_editor_input_inputTaxOfficeModel" + inputId;
            modelEditor.InputTaxOfficeModel = inputTaxOfficeModel;
            inputTaxOfficeModel.IsForQuery = true;
            
            ElInputModel inputTelefonCepModel = new ElInputModel(InputDataType.Text);
            inputTelefonCepModel.Id = "MusteriBilgileri_editor_input_inputTelefonCepModel" + inputId;
            modelEditor.InputTelefonCepModel = inputTelefonCepModel;
            inputTelefonCepModel.IsForQuery = true;
            
            ElInputModel inputTelefonModel = new ElInputModel(InputDataType.Text);
            inputTelefonModel.Id = "MusteriBilgileri_editor_input_inputTelefonModel" + inputId;
            modelEditor.InputTelefonModel = inputTelefonModel;
            inputTelefonModel.IsForQuery = true;
            
            ElInputModel inputVergiNoModel = new ElInputModel(InputDataType.Text);
            inputVergiNoModel.Id = "MusteriBilgileri_editor_input_inputVergiNoModel" + inputId;
            modelEditor.InputVergiNoModel = inputVergiNoModel;
            inputVergiNoModel.IsForQuery = true; 

            DataRow[] rehber2Data = GetTable("el_liste_deger").Select("liste_id = 21");
            ElSelectModel selectUlkesiModel = new ElSelectModel(rehber2Data, "deger", "kod");
            selectUlkesiModel.Id = "MusteriBilgileri_editor_select_selectUlkesiModel" + inputId ;
            selectUlkesiModel.IsForQuery = true;
            modelEditor.SelectUlkesiModel = selectUlkesiModel; 
            
            if (id.HasValue && id > 0)
            {
                ElDbTable table = GetTableRow(id.Value);

                DataRow row = table.Rows[0];

                inputAdModel.Value = row["adi"];
                inputSoyadModel.Value = row["soyadi"];
                inputSehirModel.Value = row["sehir"];
                inputEyaletModel.Value = row["eyalet"];
                inputTaxOfficeModel.Value = row["tax_office"];
                inputTelefonModel.Value = row["telefon"];
                inputTelefonCepModel.Value = row["mobil_tel"];
                inputEmailModel.Value = row["email"];
                inputVergiNoModel.Value = row["vergi_no"];
                //inputAdModel.Value = row["tarih"];
                inputFaxModel.Value = row["fax"];
                inputPostaKoduModel.Value = row["posta_kodu"];
                inputAdresModel.Value = row["adres"]; 
                selectUlkesiModel.SetSelectedCode(Convert.ToInt32(row["ulkesi"]));
            } 

            return modelEditor;
        }


        private ElDbTable GetTableRow(int id)
        {
            string sql =
                "select * from el_musteri_bilgileri where id =" + id;

            ElDbTable table = ExecuteTable(sql);

            //DataTable table = GetTable("el_personel");
            //DataRow row = table.Select("id=" + id).First();

            //return row;

            return table;
        }

    }
}