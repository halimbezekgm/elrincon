using System;
using System.Collections.Generic;
using System.Data;

namespace elrincon.web.Models.SharedModel
{
    public class ElSelectModel : List<CodeValueDic>
    {
        private int _selectedIndex = -1;
        private string _id;
        private string _defaultBlankOption = string.Empty;
        private string _defaultBlankOptionValue = string.Empty;
        private bool _disabled;
        private string _class;
        private object _tag;
        private bool _searchable;
        private bool _cancelButton = false;
        public bool IsForQuery { get; set; } = false;

        public ElSelectModel()
        {
        }

        public ElSelectModel(DataTable table, string displayField, string codeField)
        {
            FillFromTable(table, displayField, codeField);
        }

        public void FillFromTable(DataTable table, string displayField, string codeField)
        {
            foreach (DataRow row in table.Rows)
            {
                int code = Convert.ToInt32(row[codeField]);
                string value = row[displayField].ToString();
                CodeValueDic pair = new CodeValueDic(code, value);
                Add(pair);
            }
        }
        public ElSelectModel(DataRow[] tableRows, string displayField, string codeField)
        {
            FillFromTableRows(tableRows, displayField, codeField);
        }

        public void FillFromTableRows(DataRow[] tableRows, string displayField, string codeField)
        {
            foreach (DataRow row in tableRows)
            {
                int code = Convert.ToInt32(row[codeField]);
                string value = row[displayField].ToString();
                CodeValueDic pair = new CodeValueDic(code, value);
                Add(pair);
            }
        }

        public int MaxContainerHeight { get; set; } = 200;
        public int ContainerLineHeight { get; set; }

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; }
        }

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string DefaultBlankOption
        {
            get { return _defaultBlankOption; }
            set { _defaultBlankOption = value; }
        }

        public string GetValue(int kod)
        {
            foreach (CodeValueDic codeVal in this)
            {
                if (codeVal.Code == kod)
                    return codeVal.Value;
            }
            return string.Empty;
        }

        public bool Disabled
        {
            get { return _disabled; }
            set { _disabled = value; }
        }

        public void SetSelectedValue(string value)
        {
            _selectedIndex = -1;
            foreach (CodeValueDic cv in this)
            {
                if (cv.Value == value)
                {
                    SelectedIndex = IndexOf(cv);
                    break;
                }
            }
        }

        public void SetSelectedCode(int code)
        {
            _selectedIndex = -1;
            foreach (CodeValueDic cv in this)
            {
                if (cv.Code == code)
                {
                    SelectedIndex = IndexOf(cv);
                    break;
                }
            }
        }

        public bool Searchable
        {
            get { return _searchable; }
            set { _searchable = value; }
        }

        public string Class
        {
            get { return _class; }
            set { _class = value; }
        }

        public object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        public bool CancelButton
        {
            get { return _cancelButton; }
            set { _cancelButton = value; }
        }

        //public string DefaultBlankOptionValue { get; set; }

        public string DefaultBlankOptionValue
        {
            get { return _defaultBlankOptionValue; }
            set { _defaultBlankOptionValue = value; }
        }

        public ElSelectModel Clone()
        {
            return (ElSelectModel)this.MemberwiseClone();
        }
    }
}