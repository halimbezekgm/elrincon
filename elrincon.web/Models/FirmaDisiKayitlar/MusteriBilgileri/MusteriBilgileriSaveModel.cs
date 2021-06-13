using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using elrincon.web.Models.SharedModel;

// ReSharper disable All

namespace elrincon.web.Models.FirmaDisiKayitlar.MusteriBilgileri
{
    public class MusteriBilgileriSaveModel
    {
        public int? Id { get; set; }
        
        public string InputSehirModel { get; set; }
        public string InputAdModel { get; set; }
        public string InputSoyadModel { get; set; }
        public string InputEyaletModel { get; set; }
        public string InputPostaKoduModel { get; set; }
        public string InputVergiNoModel { get; set; }
        public string InputTaxOfficeModel { get; set; }
        public string InputTelefonModel { get; set; }
        public string InputTelefonCepModel { get; set; }
        public string InputEmailModel { get; set; }
        public string InputFaxModel { get; set; }
        public int SelectUlkesiModel { get; set; }
        public string InputAdresModel { get; set; }
    }
}