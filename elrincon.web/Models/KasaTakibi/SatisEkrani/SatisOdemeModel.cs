using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using elrincon.web.Models.SharedModel;

namespace elrincon.web.Models.KasaTakibi.SatisEkrani
{
    public class SatisOdemeModel
    {
        public List<SatisOdemeTipiMiktariModel> SatisTipiMiktariModel { get; set; }
        public List<SatisOdemeTaksitModel> SatisTaksitModel { get; set; }
        public int OdemeTipi { get; set; }
    }

    public class SatisOdemeTaksitModel
    {
        public ElInputModel OdenecekMiktari { get; set; }
        public ElSelectModel OdenecekParaCinsi { get; set; }
    }
    public class SatisOdemeTipiMiktariModel
    {
        public ElInputModel OdemeMiktari { get; set; }
        public ElSelectModel OdemeParaCinsi { get; set; }
        public ElSelectModel OdemeTipiCinsi { get; set; }
    }
}