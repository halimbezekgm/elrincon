using System.Collections.Generic;

namespace elrincon.web.Models.Yonetim.MuhasebeTanimlari
{ 
    public class MuhasebeTanimSaveParentModel
    {
        public List<MuhasebeTanimSaveModel> SaveModels { get; set; }
    }
    public class MuhasebeTanimSaveModel
    {
        public int? Id { get; set; }
        public string MuhasebeKasaKodu { get; set; }

        public List<MuhasebeTanimlariSaveBilgiKodu> MuhasebeTanimlariBilgiKodlari { get; set; }
    }

    public class MuhasebeTanimlariSaveBilgiKodu
    {
        public int? DetayId { get; set; }
        public string MuhasebeKasaBilgiKodu { get; set; }
        public List<MuhasebeTanimlariSaveBilgiDetayKodu> MuhasebeTanimlariBilgiDetayKodlari { get; set; }
    }

    public class MuhasebeTanimlariSaveBilgiDetayKodu
    {
        public string MuhasebeKasaBilgiDetayKodu { get; set; }

    }
}