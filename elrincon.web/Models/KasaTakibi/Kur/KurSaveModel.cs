using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace elrincon.web.Models.KasaTakibi.Kur
{
    public class KurSaveModel
    {
        public int id { get; set; } 
        public double USD{ get; set; }
        public double EUR { get; set; }
        public double GBP { get; set; }
        public double CHF { get; set; }
        public double AUD { get; set; }
        public double CAD { get; set; }
        public double JPY { get; set; }
        public double CNY { get; set; }
        public double ALTIN { get; set; }
        public double PESOMXN { get; set; }
        public double PESOCHI { get; set; }
        public DateTime Tarih { get; set; }
    }
}