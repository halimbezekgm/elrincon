using elrincon.web.Models.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace elrincon.web.Models.KasaTakibi.Kur
{
    public class KurEditorModel
    {
        public int id { get; set; }
        public DateTime KurEditorBaslik { get; set; }
        public DateTime KurEditorKayitBaslik { get; set; }
        public ElInputModel InputUSDModel { get; set; }
        public ElInputModel InputEURModel { get; set; }
        public ElInputModel InputGBPModel { get; set; }
        public ElInputModel InputCHFModel { get; set; }
        public ElInputModel InputAUDModel { get; set; }
        public ElInputModel InputCADModel { get; set; }
        public ElInputModel InputJPYModel { get; set; }
        public ElInputModel InputCNYModel { get; set; }
        public ElInputModel InputALTINModel { get; set; }
        public ElInputModel InputPESOMXNModel { get; set; }
        public ElInputModel InputPESOCHIModel { get; set; }
        public string AnlikUSDModel { get; set; }
        public string AnlikEURModel { get; set; }
        public string AnlikGBPModel { get; set; }
        public string AnlikCHFModel { get; set; }
        public string AnlikAUDModel { get; set; }
        public string AnlikCADModel { get; set; }
        public string AnlikJPYModel { get; set; }
        public string AnlikCNYModel { get; set; } 
        public string AnlikPESOMXNModel { get; set; } 
        public string AnlikPESOCHIModel { get; set; }
        public bool IsForDateQuery { get; set; }
        public ElInputModel QueryDate { get; set; }
    }
}