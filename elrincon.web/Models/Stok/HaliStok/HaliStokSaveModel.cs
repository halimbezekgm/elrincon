using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using elrincon.web.Models.SharedModel;

// ReSharper disable All

namespace elrincon.web.Models.Stok.HaliStok
{
    public class HaliStokSaveModel
    {
        public int? Id { get; set; }
        public string Tarih { get; set; }
        public int? Sube { get; set; }
        public string StokNo { get; set; }
        public int? AnaGrubu { get; set; }
        public int? UrunCesidi { get; set; }
        public int? Mensei { get; set; }
        public int? UretimTipi { get; set; }
        public int? KonsinyeVerilen { get; set; }
        public int? Material { get; set; }
        public int? OlcuTip { get; set; }
        public int? DugumTip { get; set; }
        public string OlcuBoy { get; set; }
        public string OlcuEn { get; set; }
        public string OlcuMt2 { get; set; }
        public string IncOlcuBoy { get; set; }
        public string IncOlcuEn { get; set; }
        public string IncOlcuMt2 { get; set; }
        public string DugumSayisi { get; set; }
        public string Renk { get; set; }
        public int? Satici { get; set; }
        public string FirmaKdu { get; set; }
        public int? KonsinyePesin { get; set; }
        public string ToplamMaliyet { get; set; }
        public string birimFiyat { get; set; }
        public string EtiketCarpani { get; set; }
        public string AdetFiyat { get; set; }
        public string EtiketFiyat { get; set; }
        public string TamirYikama { get; set; }
        public string SatisCarpani { get; set; }
        public string SatisFiyati { get; set; }
        public string Aciklama { get; set; }
    }
}