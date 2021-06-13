using elrincon.web.Models.KasaTakibi.SatisEkrani;
using elrincon.web.Models.SharedModel.GridModel;

namespace elrincon.web.Models.Yonetim.SatisEkrani
{
    public class SatisEkraniListeModel
    {
        public ElDataGridModel GridModel { get; set; }
        public SatisEkraniEditorModel SatisEkraniEditorModel { get; set; }
    }
}