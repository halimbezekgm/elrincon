using System;

namespace elrincon.web.Models.EkIslemler.UlkeKargoHesabi
{
    public class KargoHesabiSaveModel
    {
        public int id { get; set; }  
        public string adi { get; set; }
        public int dhl { get; set; }
        public int ups { get; set; }
        public int tnt { get; set; }

    }
}