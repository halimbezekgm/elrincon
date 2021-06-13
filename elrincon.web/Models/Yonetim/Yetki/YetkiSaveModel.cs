using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace elrincon.web.Models.Yonetim.Yetki
{
    public class YetkiSaveModel
    {
        public int KartId { get; set; }
        public int KullaniciId { get; set; }
        public bool GoruntuleDurum { get; set; }
        public bool EkleDurum { get; set; }
        public bool GuncelleDurum { get; set; }
        public bool SilDurum { get; set; }
    }
}