using elrincon.web.Models.PersonelBilgileri.Personel;
using elrincon.web.Models.SharedModel.GridModel;

namespace elrincon.web.Models.Yonetim
{
    public class DomainListeModel
    {
        public ElDataGridModel GridModel { get; set; }
        public PersonelEditorModel PersonelEditorModel { get; set; }
    }
}