using System.Web.Mvc;
using elrincon.web.Models.SharedModel;

namespace elrincon.web.Models.FirmaDisiKayitlar.MusteriBilgileri
{
    public class MusteriBilgileriEditorModel
    {
        public int? Id { get; set; }

        public ElInputModel InputSehirModel { get; set; }  
        public ElInputModel InputAdModel { get; set; }  
        public ElInputModel InputSoyadModel { get; set; }  
        public ElInputModel InputEyaletModel { get; set; }  
        public ElInputModel InputPostaKoduModel { get; set; }  
        public ElInputModel InputVergiNoModel { get; set; }  
        public ElInputModel InputTaxOfficeModel { get; set; }  
        public ElInputModel InputTelefonModel { get; set; }  
        public ElInputModel InputTelefonCepModel { get; set; }  
        public ElInputModel InputEmailModel { get; set; }  
        public ElInputModel InputFaxModel { get; set; }    
        public ElSelectModel SelectUlkesiModel { get; set; }
        public ElInputModel InputAdresModel { get; set; }
    }
}