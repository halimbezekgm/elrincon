using elrincon.web.Models.SharedModel.GridModel;

namespace elrincon.web.Models.PersonelBilgileri.Personel
{
    public class PersonelListeModel
    {
        public ElDataGridModel GridModel { get; set; }
        public PersonelEditorModel PersonelEditorModel { get; set; }
    }
}