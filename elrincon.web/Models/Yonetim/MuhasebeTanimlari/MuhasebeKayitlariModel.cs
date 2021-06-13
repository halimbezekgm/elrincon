using System.Collections.Generic;
using elrincon.web.Models.SharedModel;

namespace elrincon.web.Models.Yonetim.MuhasebeTanimlari
{
    public class MuhasebeKayitlariModel
    {
        public int Id { get; set; }
        public ElInputModel MuhasebeKasaKodu { get; set; }
        public List<MuhasebeTanimlariBilgiKodu> MuhasebeTanimlariBilgiKodlari { get; set; }
    }

    public class MuhasebeTanimlariBilgiKodu
    {
        public int DetayId { get; set; }
        public ElInputModel MuhasebeKasaBilgiKodu { get; set; }
        public List<MuhasebeTanimlariBilgiDetayKodu> MuhasebeTanimlariBilgiDetayKodlari { get; set; }
    }

    public class MuhasebeTanimlariBilgiDetayKodu
    {
        public ElInputModel MuhasebeKasaBilgiDetayKodu { get; set; }

    }
}