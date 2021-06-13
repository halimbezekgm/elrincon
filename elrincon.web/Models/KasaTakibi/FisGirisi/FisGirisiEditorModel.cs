using System.Collections.Generic;
using elrincon.web.Models.SharedModel;

namespace elrincon.web.Models.KasaTakibi.FisGirisi
{
    public class FisGirisiEditorModel
    { 
        public ElInputModel InputAdiModel { get; set; }
        public ElInputModel InputSoyadiModel { get; set; } 
        public ElInputModel InputTelNo1Model { get; set; }
        public ElInputModel InputTelNo2Model { get; set; } 
        public ElInputModel InputMailAdresiModel { get; set; }
        public ElInputModel InputAdresiModel { get; set; }
        public ElSelectModel SelectBolumModel { get; set; }
        public ElSelectModel SelectSubeModel { get; set; } 
        public ElSelectModel SelectMuhasebeMaasGiderHesabiModel { get; set; }  
        public ElSelectModel SelectDovizKoduModel { get; set; }

        public ElInputModel InputAciklamaModel { get; set; }

        public int? Id { get; set; }

        public List<KardanYuzdelikOranlariModel> ModelKardanOrani { get; set; }
        public ElInputModel InputFaxModel { get; set; }
        public ElSelectModel SelectUlkeModel { get; set; }
        public ElInputModel InputPostaKoduModel { get; set; }
    }
}