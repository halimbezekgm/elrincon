using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using elrincon.web.Models.SharedModel;

// ReSharper disable All

namespace elrincon.web.Models.Yonetim.Rezervasyon
{
    public class RezervasyonSaveModel
    {
        public int? Id { get; set; }
        public DateTime? InputTarihModel { get; set; }
        public string InputRezeryonNoModel { get; set; }
        public int? InputAcentaModel { get; set; }
        public int? InputServisPersoneliModel { get; set; }
        public string InputKisiSayisiModel { get; set; }
        public int? InputUlkesiModel { get; set; }
        public int? Rehber1Model { get; set; }
        public int? Rehber2Model { get; set; }
        public int? Hanutcu1Model { get; set; }
        public int? Hanutcu2Model { get; set; }

        public string InputAciklamaModel { get; set; }
    }
}