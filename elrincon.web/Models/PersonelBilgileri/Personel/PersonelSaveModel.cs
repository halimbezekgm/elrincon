using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using elrincon.web.Models.SharedModel;

// ReSharper disable All

namespace elrincon.web.Models.PersonelBilgileri.Personel
{
    public class PersonelSaveModel
    {
        public int? Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string TcNo { get; set; }
        public string KullaniciAdi{ get; set; }
        public string Sifre{ get; set; }
        public string TelNo1 { get; set; }
        public string TelNo2 { get; set; }
        public DateTime? IseGirisTarihi { get; set; }
        public string MailAdresi { get; set; }
        public int? Subesi { get; set; }
        public int? Bolumu { get; set; }
        public string Adres{ get; set; }
        public int? MuhasebeMaasGiderHesabi{ get; set; }
        public int? MuhasebeKodu { get; set; }
        public int? MaasTipi { get; set; }
        public string MaasMiktari{ get; set; }
        public int? DovizKodu { get; set; }
        public string Aciklama { get; set; }
        public List<KardanYuzdelikOraniSaveModel> SaveYuzdelikOraniModels { get; set; }
    }
}