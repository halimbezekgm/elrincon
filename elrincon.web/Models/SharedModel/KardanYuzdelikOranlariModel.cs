using System.Web.Mvc;

namespace elrincon.web.Models.SharedModel
{
    public class KardanYuzdelikOranlariModel
    {
        public int YuzdelikTipi { get; set; }
        public ElSelectModel UrunTipi { get; set; }
        public ElInputModel NarmalSatisParite1 { get; set; }
        public ElInputModel NarmalSatisParite2 { get; set; }
        public ElInputModel NarmalSatisYuzdeligi { get; set; }
        public ElInputModel KapiSatisParite1 { get; set; }
        public ElInputModel KapiSatisParite2 { get; set; }
        public ElInputModel KapiSatisYuzdeligi { get; set; }
        public ElInputModel InternetSatisParite1 { get; set; }
        public ElInputModel InternetSatisParite2 { get; set; }
        public ElInputModel InternetSatisYuzdeligi { get; set; }
        public ElInputModel YurtDisiSatisParite1 { get; set; }
        public ElInputModel YurtDisiSatisParite2 { get; set; }
        public ElInputModel YurtDisiSatisYuzdeligi { get; set; }
        public ElInputModel GenelCiroSatisParite1 { get; set; }
        public ElInputModel GenelCiroSatisParite2 { get; set; }
        public ElInputModel GenelCiroSatisYuzdeligi { get; set; }
        public ElSelectModel MuhasebeGelirKodu { get; set; }
    }
}