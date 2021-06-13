using System.Collections.Generic;
using elrincon.web.Models.SharedModel;

namespace elrincon.web.Models.Yonetim.HesapPlani
{
    public class HesapPlaniModel
    {
        public int Id { get; set; }
        public ElInputModel MuhasebeKasaKodu { get; set; }
        public List<HesapPlaniBilgiKodu> HesapPlaniBilgiKodlari { get; set; }
    }

    public class HesapPlaniBilgiKodu
    {
        public int DetayId { get; set; }
        public ElInputModel MuhasebeKasaBilgiKodu { get; set; }
        public List<HesapPlaniBilgiDetayKodu> HesapPlaniBilgiDetayKodlari { get; set; }
    }

    public class HesapPlaniBilgiDetayKodu
    {
        public ElInputModel MuhasebeKasaBilgiDetayKodu { get; set; }

    }
}