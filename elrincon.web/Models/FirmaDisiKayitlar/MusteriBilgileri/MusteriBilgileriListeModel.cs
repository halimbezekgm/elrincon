using elrincon.web.Models.SharedModel.GridModel;
using elrincon.web.Models.Yonetim.Rezervasyon;

namespace elrincon.web.Models.FirmaDisiKayitlar.MusteriBilgileri
{
    public class MusteriBilgileriListeModel
    {
        public ElDataGridModel GridModel { get; set; }
        public MusteriBilgileriEditorModel MusteriBilgileriEditorModel { get; set; }
    }
}