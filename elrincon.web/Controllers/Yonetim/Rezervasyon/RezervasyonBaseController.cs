using System;
using System.Collections.Generic;
using System.Data;
using elrincon.web.Manager;
using elrincon.web.Models.PersonelBilgileri.Personel;
using elrincon.web.Models.SharedModel;
using elrincon.web.Models.Yonetim.Rezervasyon;

namespace elrincon.web.Controllers.Yonetim.Rezervasyon
{
    public class RezervasyonBaseController : BaseController
    {

        public RezervasyonEditorModel FillModel(int? id, string inputId)
        {
            RezervasyonEditorModel modelEditor = new RezervasyonEditorModel(); 

            ElInputModel inputTarihModel = new ElInputModel(InputDataType.Date);
            inputTarihModel.Id = "rezervasyon_editor_input_tarih" + inputId;
            modelEditor.InputTarihModel = inputTarihModel;
            inputTarihModel.IsForQuery = true;
            inputTarihModel.Value = DateTime.Today;

            ElInputModel rezeryonNoModel = new ElInputModel(InputDataType.Text);
            rezeryonNoModel.Id = "rezervasyon_editor_input_InputRezeryonNoModel" + inputId;
            modelEditor.InputRezeryonNoModel = rezeryonNoModel;
            rezeryonNoModel.IsForQuery = true;
            rezeryonNoModel.Disabled = true;
            
            ElInputModel kisiSayisiModel = new ElInputModel(InputDataType.Integer);
            kisiSayisiModel.Id = "rezervasyon_editor_input_InputKisiSayisiModel" + inputId;
            modelEditor.InputKisiSayisiModel = kisiSayisiModel;
            kisiSayisiModel.IsForQuery = true;

            DataRow[] tableRows = GetTable("el_liste_deger").Select("liste_id = 21");
            ElSelectModel inputUlkesiModel = new ElSelectModel(tableRows, "deger", "kod");
            inputUlkesiModel.Id = "rezervasyon_editor_select_inputUlkesiModel" + inputId ;
            inputUlkesiModel.IsForQuery = true;
            modelEditor.InputUlkesiModel = inputUlkesiModel;

            DataRow[] rehberData = GetTablRehberList();//GetTable("el_liste_deger").Select("liste_id = 1");
            ElSelectModel rehber1Model = new ElSelectModel(rehberData, "adi", "id");
            rehber1Model.Id = "rezervasyon_editor_select_inputrehber1Model" + inputId ;
            rehber1Model.IsForQuery = true;
            modelEditor.Rehber1Model = rehber1Model;

            DataRow[] rehber2Data = GetTablRehberList();
            ElSelectModel rehber2Model = new ElSelectModel(rehber2Data, "adi", "id");
            rehber2Model.Id = "rezervasyon_editor_select_inputrehber2Model" + inputId ;
            rehber2Model.IsForQuery = true;
            modelEditor.Rehber2Model = rehber2Model;

            DataRow[] acentaData = GetTablAcentaList();
            ElSelectModel acentaModel = new ElSelectModel(acentaData, "adi", "id");
            acentaModel.Id = "rezervasyon_editor_input_InputAcentaModel" + inputId;
            modelEditor.InputAcentaModel = acentaModel;
            acentaModel.IsForQuery = true;

            DataRow[] tezgahtarData = GetTablePersonelTezgahtarList();
            ElSelectModel tezgahtarSelect  = new ElSelectModel(tezgahtarData, "adi", "id");
            tezgahtarSelect.Id = "rezervasyon_editor_input_servis" + inputId;
            modelEditor.InputServisPersonelModel = tezgahtarSelect;
            tezgahtarSelect.IsForQuery = true; 

            DataRow[] hanutcu1Data = GetTableHanutcuList();
            ElSelectModel hanutcu1Model = new ElSelectModel(hanutcu1Data, "adi", "id");
            hanutcu1Model.Id = "rezervasyon_editor_select_inputhanutcu1Model" + inputId ;
            hanutcu1Model.IsForQuery = true;
            modelEditor.Hanutcu1Model = hanutcu1Model;

            DataRow[] hanutcu2Data = GetTableHanutcuList();
            ElSelectModel hanutcu2Model = new ElSelectModel(hanutcu2Data, "adi", "id");
            hanutcu2Model.Id = "rezervasyon_editor_select_inputhanutcu2Model" + inputId ;
            hanutcu2Model.IsForQuery = true;
            modelEditor.Hanutcu2Model = hanutcu2Model;


            ElInputModel aciklamaModel = new ElInputModel(InputDataType.TextArea);
            aciklamaModel.Id = "rezervasyon_editor_input_aciklama" + inputId;
            modelEditor.InputAciklamaModel = aciklamaModel;
            aciklamaModel.IsForQuery = true;

            if (id.HasValue && id > 0)
            {
                ElDbTable table = GetTableRow(id.Value);

                DataRow row = table.Rows[0];

                kisiSayisiModel.Value = row["kisi_sayisi"];
                inputTarihModel.Value = row["tarih"];
                rezeryonNoModel.Value = row["rezervasyon_no"];
                acentaModel.SetSelectedCode(Convert.ToInt32(row["acenta"])) ;
                tezgahtarSelect.SetSelectedCode(Convert.ToInt32(row["servis"]));
                aciklamaModel.Value = row["aciklama"];
                inputUlkesiModel.SetSelectedCode(Convert.ToInt32(row["ulkesi"]));
                rehber1Model.SetSelectedCode(Convert.ToInt32(row["rehber_1"]));
                rehber2Model.SetSelectedCode(Convert.ToInt32(row["rehber_2"]));
                hanutcu1Model.SetSelectedCode(Convert.ToInt32(row["hanutcu_1"]));
                hanutcu2Model.SetSelectedCode(Convert.ToInt32(row["hanutcu_2"]));

            }
            else
            {
                string sql = "select top 1 * from el_rezervasyon order by rezervasyon_no desc ";

                ElDbTable table = ExecuteTable(sql);
                int lastid = 1;

                if (table.Rows.Count > 0)
                    lastid = Convert.ToInt32(table.Rows[0]["rezervasyon_no"]) + 1;
                rezeryonNoModel.Value = lastid;
            }


            return modelEditor;
        }


        private ElDbTable GetTableRow(int id)
        {
            string sql =
                "select * from el_rezervasyon where id =" + id;

            ElDbTable table = ExecuteTable(sql);

            //DataTable table = GetTable("el_personel");
            //DataRow row = table.Select("id=" + id).First();

            //return row;

            return table;
        }

        public DataRow[] GetTablAcentaList()
        {
            string sql = "select adi, id " +
                         "from el_acenta where durum != 0 or durum is null ";

            ElDbTable table = ExecuteTable(sql);

            return table.Select();
        }
        public DataRow[] GetTablePersonelTezgahtarList()
        {
            string sql = "select adi, id " +
                         "from el_personel where bolum=2 ";

            ElDbTable table = ExecuteTable(sql);

            return table.Select();
        }

        public DataRow[] GetTablRehberList()
        {
            string sql = "select concat(adi, ' ', soyadi) as adi, id " +
                         "from el_rehber where durum != 0 or durum is null ";

            ElDbTable table = ExecuteTable(sql);

            return table.Select();
        }
        
        public DataRow[] GetTableHanutcuList()
        {
            string sql = "select concat(adi, ' ', soyadi) as adi, id " +
                         "from el_personel " +
                         "where (durum != 0 or durum is null)  and personel_tipi = 2 ";

            ElDbTable table = ExecuteTable(sql);

            return table.Select();
        }


    }
}