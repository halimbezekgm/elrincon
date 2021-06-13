using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace elrincon.web.Models.Yonetim.Yetki
{
    public class YetkiEditorModel
    {
        public int KullaniciId { get; set; }
        public string KullaniciAdi { get; set; }
        public int KartId { get; set; }
        public string KartAdi { get; set; }
        public string GoruntuleDurum { get; set; }
        public string EkleDurum { get; set; }
        public string GuncelleDurum { get; set; }
        public string SilDurum { get; set; } 
    }
}