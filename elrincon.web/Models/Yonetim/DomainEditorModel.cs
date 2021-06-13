 
using elrincon.web.Models.SharedModel;
using elrincon.web.Models.SharedModel.GridModel;

namespace elrincon.web.Models.Yonetim
{
    public class DomainEditorModel
    {
        public ElDataGridModel ListeGridModel { get; set; }
        public ElInputModel NewDomainName { get; set; }
        public int ListeId { get; set; }
        public int? ChildId{ get; set; }
        public string ListeAdi { get; set; }
        public ElInputModel ListeAdiInputModel { get; set; }
    }
}