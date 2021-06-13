using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using elrincon.web.Models.SharedModel;

namespace elrincon.web.Models.EmptyModel
{
    public class EmtyEditorModel
    {
        public ElInputModel InputModel { get; set; }
        public ElInputModel InputSifreModel { get; set; }
        public ElSelectModel SelectModel { get; set; }
        public int? Id { get; set; }
    }
}