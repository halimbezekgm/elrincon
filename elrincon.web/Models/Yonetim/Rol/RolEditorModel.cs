using elrincon.web.Models.SharedModel;

namespace elrincon.web.Models.Yonetim.Rol
{
    public class RolEditorModel
    {
        public ElInputModel InputRolAdiModel { get; set; }
        public ElInputModel InputRolAciklamaModel { get; set; }
        public ElSelectModel SelectRolKarti { get; set; }
        public int RolId { get; set; }
    }
}