using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace elrincon.web.Models.SharedModel
{
    public class ElInputModel
    {
        object _value;
        readonly InputDataType _format;
        int _length = int.MaxValue;
        bool _sign;
        string _id;
        string _placeHolder;
        private bool _disabled;
        private bool _readOnly;
        private string _parentDivStyle;
        private string _prepend;
        public bool IsForQuery { get; set; } = false;


        public ElInputModel(object value, InputDataType format)
        {
            _value = value;
            _format = format;
        }

        public ElInputModel(InputDataType format)
        {
            _format = format;
        }

        public object Value
        {
            set { _value = value; }
            get { return _value; }
        }

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public InputDataType Format
        {
            get { return _format; }
        }

        public int Length
        {
            get { return _length; }
            set { _length = value; }
        }

        public bool Sign
        {
            get { return _sign; }
            set { _sign = value; }
        }

        public string PlaceHolder
        {
            get { return _placeHolder; }
            set { _placeHolder = value; }
        }

        public bool Disabled //todo: enis -> ali enabled olacak, enable = true default
        {
            get { return _disabled; }
            set { _disabled = value; }
        }

        public bool ReadOnly
        {
            get { return _readOnly; }
            set { _readOnly = value; }
        }

        public static string ToString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            return value.Trim();
        }

        public static string ToUpper(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            return value.Trim().ToUpper();
        }

        public static string ToTitleCase(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            return ToTitleCase2(value.Trim());
        }

        public static string ToTitleCase2(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        public static bool IsAlphabetRuleValid(string value)
        {
            char[] validChars = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZQWXabcçdefgğhıijklmnoöprsştuüvyzqwx ".ToCharArray();
            char[] charArray = value.ToCharArray();
            foreach (char val in charArray)
            {
                bool find = false;
                foreach (char validChar in validChars)
                {
                    if (val != validChar)
                        continue;

                    find = true;
                    break;
                }
                if (!find)
                    return false;
            }
            return true;
        }

        public static bool IsNumeric(string value)
        {
            double number;
            if (Double.TryParse(value, out number))
                return true;

            return false;
        }

        public static bool IsPhoneValid(string value)
        {
            string val = value.Trim();
            val = new string(val.Where(c => char.IsDigit(c)).ToArray());

            if ((val.Length != 10) || (val.StartsWith("0")))
                return false;

            return true;
        }

        public static bool IsMailValid(string value)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(value);
                return addr.Address == value;
            }
            catch
            {
                return false;
            }
        }

        public static long? ToPhone(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            string result = value.Trim();
            result = new string(result.Where(c => char.IsDigit(c)).ToArray());

            if (result.Length != 10)
                throw new Exception("Telefon numarası 10 karakter olmalı...");

            if (result.StartsWith("0"))
                throw new Exception("Telefon numarası 0 ile başlamamalı...");

            return Convert.ToInt64(result);
        }

        public static string ToFormatedPhone(string value)
        {
            if (value.Length != 10)
                throw new Exception("Telefon numarası 10 haneli olmalı");

            return string.Format("{0:(###) ###-##-##}", long.Parse(value));
        }

        public static int? ToInteger(int? value)
        {
            if (value == null)
                return null;

            return value;
        }

        public static double? ToDouble(double? value)
        {
            if (value == null)
                return null;

            return value;
        }

        public static DateTime? ToDateTime(DateTime? value)
        {
            if (value == null)
                return null;

            return value;
        }

        public static string ToDate(string value)
        {
            if (value.Contains(" "))
            {
                string[] vals = value.Split(' ');
                value = vals[0];
            }
            return value;
        }

        public static string ToDateTime(string value)
        {
            return value;
        }

        public string ParentDivStyle
        {
            set { _parentDivStyle = value; }
            get { return _parentDivStyle; }
        }

        public string Prepend
        {
            get { return _prepend; }
            set { _prepend = value; }
        }
    }
}