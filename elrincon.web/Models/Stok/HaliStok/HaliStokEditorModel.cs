using System.Collections.Generic;
using System.Web.Mvc;
using elrincon.web.Models.SharedModel;

namespace elrincon.web.Models.Stok.HaliStok
{
    public class HaliStokEditorModel
    { 
        public int? Id { get; set; }
        public ElInputModel InputTarihModel { get; set; }
        public ElSelectModel SelectSubeModel { get; set; }
        public ElInputModel InputStokNoModel { get; set; }
        public ElSelectModel SelectUrunCesidiModel { get; set; }
        public ElSelectModel SelectMenseiModel { get; set; }
        public ElSelectModel SelectUretimTipiModel { get; set; }
        public ElSelectModel SelectMaterialModel { get; set; }
        public ElSelectModel SelectOlcuTipModel { get; set; }
        public ElSelectModel SelectDugumTipModel { get; set; }
        public ElInputModel InputOlcuBoyModel { get; set; }
        public ElInputModel InputOlcuEnModel { get; set; }
        public ElInputModel InputOlcuMt2Model { get; set; }
        public ElInputModel InputIncOlcuBoyModel { get; set; }
        public ElInputModel InputIncOlcuEnModel { get; set; }
        public ElInputModel InputIncOlcuMt2Model { get; set; }
        public ElSelectModel InputDugumSayisiModel { get; set; }
        public ElSelectModel InputRenkModel { get; set; }
        public ElSelectModel SelectSaticiModel { get; set; }
        public ElInputModel InputFirmaKduModel { get; set; }
        public ElSelectModel SelectKonsinyePesinModel { get; set; }
        public ElInputModel InputToplamMaliyetModel { get; set; }
        public ElInputModel InputbirimFiyatModel { get; set; }
        public ElInputModel InputEtiketCarpaniModel { get; set; }
        public ElInputModel InputAdetFiyatModel { get; set; }
        public ElInputModel InputEtiketFiyatModel { get; set; }
        public ElInputModel InputTamirYikamaModel { get; set; }
        public ElInputModel InputSatisCarpaniModel { get; set; }
        public ElInputModel InputSatisFiyatiModel { get; set; }
        public ElInputModel InputAciklamaModel { get; set; }
        public ElSelectModel SelectAnaGrubu { get; set; }
        public ElSelectModel SelectKonsinyeVerilen { get; set; }
        public ElSelectModel SelectAlimDurumu { get; set; }
    }
}