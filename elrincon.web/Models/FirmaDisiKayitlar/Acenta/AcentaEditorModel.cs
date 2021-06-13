using System.Collections.Generic;
using System.Web.Mvc;
using elrincon.web.Models.SharedModel;

namespace elrincon.web.Models.FirmaDisiKayitlar.Acenta
{
    public class AcentaEditorModel
    {
        public ElInputModel InputAdiModel { get; set; }
        public ElInputModel InputSehirModel { get; set; }
        public ElInputModel InputTelNo1Model { get; set; }
        public ElInputModel InputTelNo2Model { get; set; }
        public ElInputModel InputMailAdresiModel { get; set; }
        public ElInputModel InputAdresiModel { get; set; }
        public ElSelectModel SelectBolumModel { get; set; }
        public ElSelectModel SelectSubeModel { get; set; }
        public ElInputModel InputMuhasebeKoduModel { get; set; }
        public ElSelectModel SelectDovizKoduModel { get; set; }

        public ElInputModel InputAciklamaModel { get; set; }

        public int? Id { get; set; }

        public List<KardanYuzdelikOranlariModel> ModelKardanOrani { get; set; }
        public ElInputModel InputFaxModel { get; set; }
        public ElSelectModel SelectUlkeModel { get; set; }
        public ElInputModel InputPostaKoduModel { get; set; }
        public ElInputModel InputIlgiliKisi1Model { get; set; }
        public ElInputModel InputIlgiliKisi2Model { get; set; }
    }
}