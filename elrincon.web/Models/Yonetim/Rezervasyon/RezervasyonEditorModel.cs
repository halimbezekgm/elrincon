using System.Collections.Generic;
using elrincon.web.Models.SharedModel;

namespace elrincon.web.Models.Yonetim.Rezervasyon
{
    public class RezervasyonEditorModel
    {
        public int? Id { get; set; }

        public ElInputModel InputTarihModel { get; set; } 
        public ElInputModel InputRezeryonNoModel { get; set; } 
        public ElSelectModel InputAcentaModel { get; set; } 
        public ElSelectModel InputServisPersonelModel { get; set; } 
        public ElInputModel InputKisiSayisiModel { get; set; } 
        public ElSelectModel InputUlkesiModel { get; set; }  
        public ElSelectModel Rehber1Model { get; set; } 
        public ElSelectModel Rehber2Model { get; set; } 
        public ElSelectModel Hanutcu1Model { get; set; } 
        public ElSelectModel Hanutcu2Model { get; set; } 

        public ElInputModel InputAciklamaModel { get; set; }


    }
}