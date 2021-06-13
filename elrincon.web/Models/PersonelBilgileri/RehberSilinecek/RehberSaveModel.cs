using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using elrincon.web.Models.SharedModel;

// ReSharper disable All

namespace elrincon.web.Models.RehberBilgileri.Rehber
{
    public class RehberSaveModel
    {
        public int? Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; } 
        public string TelNo1 { get; set; }
        public string TelNo2 { get; set; }
        public string MailAdresi { get; set; }
        public int? Subesi { get; set; } 
        public string Adres{ get; set; } 
        public string MuhasebeKodu { get; set; }  
        public int? DovizKodu { get; set; }
        public string Aciklama { get; set; }
        public List<KardanYuzdelikOraniSaveModel> SaveYuzdelikOraniModels { get; set; }
        public string InputFaxModel { get; set; }
        public int? SelectUlkeModel { get; set; }
        public string InputPostaKoduModel { get; set; }
    }
}