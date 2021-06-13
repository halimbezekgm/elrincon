using System;
using elrincon.web.Models.SharedModel;

namespace elrincon.web.Models.EkIslemler.UlkeKargoHesabi
{
    public class KargoKodlariModel
    {
        public int id { get; set; } 
        public ElInputModel InputUlkeAdiModel { get; set; }
        public ElInputModel InputDHLModel { get; set; }
        public ElInputModel InputUPSModel { get; set; }
        public ElInputModel InputTNTModel { get; set; }
    }
}