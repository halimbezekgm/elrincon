using System.Collections.Generic;
using System.Web.Mvc;
using elrincon.web.Models.SharedModel;

namespace elrincon.web.Models.KasaTakibi.SatisEkrani
{
    public class SatisEkraniEditorModel
    {
        public int? Id { get; set; }

        public ElInputModel InputTarihModel { get; set; } 
        public ElInputModel InputAdSoyadModel { get; set; }  
        public ElSelectModel SubeModel { get; set; } 
        public ElSelectModel KontratNoModel { get; set; } 

        public ElInputModel InputAciklamaModel { get; set; }
        public ElSelectModel OdemeSekli { get; set; }
        public ElInputModel InputSatisNoModel { get; set; }
        public ElInputModel SatisTutariModel { get; set; }
        public ElSelectModel SatisParaCinsiModel { get; set; }
        public ElSelectModel RezervasyonBilgiModel { get; set; }
        public ElInputModel EtikekToplamiModel { get; set; }
        public ElSelectModel EtiketToplamiParaCinsiModel { get; set; }
        public ElInputModel SatisParitesiModel { get; set; }
        public ElSelectModel UlkeModel { get; set; }
        public ElInputModel KargoUcretiModel { get; set; }
        public ElSelectModel KargoUcretParaCinsiModel { get; set; }
        public ElSelectModel SatisSekliModel { get; set; }

        public SatisOdemeModel SatisOdemeEditorModel { get; set; }
        public List<StokList> StokListModel { get; set; }
        public List<SatisPersonelleri> SatisPersonelleriList { get; set; }
        public ElSelectModel CiroDahilmiModel { get; set; }
    }
     
    public class StokList
    {
        public ElSelectModel StokNoModel { get; set; }
        public ElInputModel StokUrunAdiModel { get; set; }
        public ElInputModel StokFiyatiModel { get; set; }
        public ElInputModel StokEnModel { get; set; }
        public ElInputModel StokBoyModel { get; set; }
    }

    public class SatisPersonelleri
    { 
        public ElSelectModel PersoneltipBilgiModel { get; set; }
        public ElInputModel HaliYuzdelikModel { get; set; }
        public ElInputModel PirlantaYuzdelikModel { get; set; }
        public ElInputModel FanteziYuzdelikModel { get; set; }
        public ElInputModel GumusYuzdelikModel { get; set; }
        public ElInputModel AltınYuzdelikModel { get; set; }
        public ElInputModel SeramikYuzdelikModel { get; set; }
        public ElInputModel DeriYuzdelikModel { get; set; }
        public ElInputModel HediyelikYuzdelikModel { get; set; }
    }
}