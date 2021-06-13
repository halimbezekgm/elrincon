using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace elrincon.web.Models.SharedModel
{
    public class KardanYuzdelikOraniSaveModel
    {
        public int? YuzdeTip { get; set; }
        public int? UrunTipi { get; set; }
        public string NarmalSatisParite1 { get; set; }
        public string NarmalSatisParite2 { get; set; }
        public string NarmalSatisYuzdeligi { get; set; }
        public string KapiSatisParite1 { get; set; }
        public string KapiSatisParite2 { get; set; }
        public string KapiSatisYuzdeligi { get; set; }
        public string InternetSatisParite1 { get; set; }
        public string InternetSatisParite2 { get; set; }
        public string InternetSatisYuzdeligi { get; set; }
        public string YurtDisiSatisParite1 { get; set; }
        public string YurtDisiSatisParite2 { get; set; }
        public string YurtDisiSatisYuzdeligi { get; set; }
        public string GenelCiroSatisParite1 { get; set; }
        public string GenelCiroSatisParite2 { get; set; }
        public string GenelCiroSatisYuzdeligi { get; set; }
        public int MuhasebeKodu{ get; set; }
    } 
}