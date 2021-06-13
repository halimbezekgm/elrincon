using System.Collections.Generic;

namespace elrincon.web.Models.Yonetim.HesapPlani
{ 
    public class HesapPlaniSaveParentModel
    {
        public List<HesapPlaniSaveModel> SaveModels { get; set; }
    }
    public class HesapPlaniSaveModel
    {
        public int Id { get; set; }
        public string MuhasebeKasaKodu { get; set; }

        public List<HesapPlanilariSaveBilgiKodu> HesapPlanilariBilgiKodlari { get; set; }
    }

    public class HesapPlanilariSaveBilgiKodu
    {
        public int DetayId { get; set; }
        public string MuhasebeKasaBilgiKodu { get; set; }
        public List<HesapPlanilariSaveBilgiDetayKodu> HesapPlanilariBilgiDetayKodlari { get; set; }
    }

    public class HesapPlanilariSaveBilgiDetayKodu
    {
        public string MuhasebeKasaBilgiDetayKodu { get; set; }

    }
}