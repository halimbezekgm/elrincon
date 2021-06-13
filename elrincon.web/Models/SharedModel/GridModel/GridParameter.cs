using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace elrincon.web.Models.SharedModel.GridModel
{
    public class GridParameter
    {
        public string SortubleQuery { get; set; }
        public string PageSize { get; set; }
        public string PageNumber { get; set; }
        public string OrderBy { get; set; }
        public FilterValues FilterValues { get; set; }
        public CustomProperties CustomProperties { get; set; }
    }
}