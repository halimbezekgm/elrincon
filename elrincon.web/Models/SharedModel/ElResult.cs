using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace elrincon.web.Models.SharedModel
{
    public class ElResult
    {
        public string result { get; set; }
        public string message { get; set; }


        public void AddMessage(string msg)
        {
            message += "<br/>" +msg ;
        }

        public void SetError()
        {
            result = "err";
        }
        public void SetOk()
        {
            result = "ok";
            message = "Başarılı";
        }

        public bool HasError()
        {
            if(!string.IsNullOrWhiteSpace(this.message))
                return true;

            return false;
        }

        public void AddErrorMessage(string eMessage)
        {
            message = eMessage;
        }
         
    }
}