using System;
using elrincon.web.Models.SharedModel;

namespace elrincon.web.Models.EkIslemler.UlkeKargoHesabi
{
    public class KargoHesabiModel
    {
        public int id { get; set; }

        public ElSelectModel Ulkeler { get; set; }
        
        public ElSelectModel KargoFirmalari{ get; set; }

        public ElInputModel Miktar { get; set; }
        public ElInputModel Sonuc { get; set; }
    }
}