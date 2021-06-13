using elrincon.web.Models.SharedModel;
using elrincon.web.Models.SharedModel.GridModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace elrincon.web.Models.KasaTakibi.Kur
{
    public class KurListeModel
    {
        public ElDataGridModel gridModel { get; set; }
        public ElInputModel sorguBasTarih { get; set; }
        public ElInputModel sorguBitTarih { get; set; }
    }
}