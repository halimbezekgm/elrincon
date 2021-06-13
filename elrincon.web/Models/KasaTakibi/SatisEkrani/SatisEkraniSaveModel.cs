using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using elrincon.web.Models.SharedModel;

// ReSharper disable All

namespace elrincon.web.Models.Yonetim.SatisEkrani
{
    public class SatisEkraniSaveModel
    {
        public int? Id { get; set; }

        public DateTime? InputTarihModel { get; set; }
        public string InputAdSoyadModel { get; set; }
        public int?  SubeModel { get; set; }
        public int?  KontratNoModel { get; set; }

        public string InputAciklamaModel { get; set; }
        public int?  OdemeSekli { get; set; }
        public double? OdemePesinMiktari { get; set; }
        public int? OdemePesinMiktariCinsi { get; set; }
        public string InputSatisNoModel { get; set; }
        public double? SatisTutariModel { get; set; }
        public int?  SatisParaCinsiModel { get; set; }
        public int?  RezervasyonBilgiModel { get; set; }
        public double? EtikekToplamiModel { get; set; }
        public int?  EtiketToplamiParaCinsiModel { get; set; }
        public string SatisParitesiModel { get; set; }
        public int?  UlkeModel { get; set; }
        public double? KargoUcretiModel { get; set; }
        public int?  KargoUcretParaCinsiModel { get; set; }
        public int?  SatisSekliModel { get; set; }

        public List<AlinanOdeme> AlinanOdemeler { get; set; }
        public List<TaksitListesi> TaksitList { get; set; }
        public List<StokListSave> StokListModel { get; set; }
        public List<SatisPersonelleriSave> SatisPersonelleriList { get; set; }
    }

    public class AlinanOdeme
    {
        public double? OdemeSekli { get; set; }
        public int? Odememiktari { get; set; }
        public int? OdemeDovizCinsi { get; set; }
    }

    public class TaksitListesi
    {
        public double? TaksitMiktari { get; set; }
        public int? TaksitOdemeCinsi { get; set; }
    }

    public class StokListSave
    {
        public int?  StokNoModel { get; set; }
        public string StokAlisTarihiModel { get; set; }
        public double? StokFiyatiModel { get; set; }
        public int? StokEnModel { get; set; }
        public int? StokBoyModel { get; set; }
    }

    public class SatisPersonelleriSave
    {
        public int?  PersoneltipBilgiModel { get; set; }
        public double? HaliYuzdelikModel { get; set; }
        public double? PirlantaYuzdelikModel { get; set; }
        public double? FanteziYuzdelikModel { get; set; }
        public double? GumusYuzdelikModel { get; set; }
        public double? AltınYuzdelikModel { get; set; }
        public double? SeramikYuzdelikModel { get; set; }
        public double? DeriYuzdelikModel { get; set; }
        public double? HediyelikYuzdelikModel { get; set; }
    }
}