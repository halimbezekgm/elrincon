using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using elrincon.web.Models.SharedModel;
using elrincon.web.Models.SharedModel.GridModel;

namespace elrincon.web.Models.KasaTakibi.KasaBankaGoruntule
{
    public class KasaBankaGoruntuleModel
    {
        public ElInputModel SorguTarihiBas { get; set; }
        public ElInputModel SorguTarihiBitis { get; set; }
        public ElSelectModel HesapNo { get; set; }
        public ElSelectModel HesapDetayNo { get; set; }
        public ElInputModel SorguDevirTarihi { get; set; }
        public ElInputModel DevirMiktariveParaCinsi { get; set; }
        public ElDataGridModel KasaBankaListGridModel{ get; set; }
    }
}