using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using elrincon.web.Models.SharedModel;

namespace elrincon.web.Models.PersonelBilgileri.Personel
{
    public class PersonelEditorModel
    { 
        public ElInputModel InputAdiModel { get; set; }
        public ElInputModel InputSoyadiModel { get; set; }
        public ElInputModel InputTcModel { get; set; }
        public ElInputModel InputTelNo1Model { get; set; }
        public ElInputModel InputTelNo2Model { get; set; }
        public ElInputModel InputKullaniciAdiModel { get; set; }
        public ElInputModel InputSifresiModel { get; set; }
        public ElInputModel InputMailAdresiModel { get; set; }
        public ElInputModel InputAdresiModel { get; set; }
        public ElSelectModel SelectBolumModel { get; set; }
        public ElSelectModel SelectSubeModel { get; set; }
        public ElSelectModel SelectMuhasebeMaasGiderHesabiModel { get; set; }
        public ElSelectModel SelectMuhasebeKoduModel { get; set; }
        public ElSelectModel SelectMaasTipiModel { get; set; }
        public ElInputModel InputMaasMiktariModel { get; set; }
        public ElInputModel InputIseGirisTarihiModel { get; set; }
        public ElSelectModel SelectDovizKoduModel { get; set; }

        public ElInputModel InputAciklamaModel { get; set; }

        public int? Id { get; set; }

        public List<KardanYuzdelikOranlariModel> ModelKardanOrani { get; set; }

    }
}