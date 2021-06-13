using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace elrincon.web.Models.SharedModel
{
    public class CodeValueDic:Dictionary<int,string>
    {
        public int Code { get; set; }
        public string Value { get; set; }
        
        public CodeValueDic(int code, string value)
        {
            Code = code;
            Value = value;
        }

    }
}