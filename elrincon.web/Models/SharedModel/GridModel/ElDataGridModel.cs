using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using elrincon.web.Manager;

namespace elrincon.web.Models.SharedModel.GridModel
{
    public class ElDataGridModel
    {
        public string DataUrl { get; set; }
        public string GridId { get; set; }
        public int PageSNumber { get; set; }
        public int CurrentPage { get; set; }
        public int PageTotal { get; set; }
        public int PageCount { get; set; }
        public bool IsWrite { get; set; } = true;

        public List<GridRow> GridRows { get; set; }
        public List<ActionButton> ActionButtons { get; set; }
        public string ActionButtonWith { get; set; }
        public DataGridTable GridTable { get; set; }
        public GridParameter GridParameter { get; set; }

        private int _pageSize;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }

            set
            {
                _pageSize = value;
                PageSNumber = 1;
                PageTotal = GridTable.GridTable.Rows.Count/_pageSize;
                CurrentPage = 1;
                this.GridTable.GridTable.OffsetFetch = Convert.ToInt32(_pageSize);
                this.GridTable.GridTable.Offset = (Convert.ToInt32(PageSNumber) - 1) * Convert.ToInt32(_pageSize);
            }
        }



        public ElDataGridModel(ElDbTable table, string gridId)
        { 
            GridId = gridId; 

            ActionButtons = new List<ActionButton>();
            GridRows = new List<GridRow>();
            GridParameter= new GridParameter();
            //PageCount = GridTable.GridTable.Rows.Count;
            //CurrentPage = 1;

        }
        public ElDataGridModel(string gridId)
        { 
            GridId = gridId; 

            ActionButtons = new List<ActionButton>();
            GridRows = new List<GridRow>();
            GridParameter= new GridParameter(); 
        }

        public ElDataGridModel()
        { 
        }

        public void SetParameter(GridParameter parameter)
        {
            SetFilter(parameter.FilterValues);

            //if (parameter.PageSize != null)
            //{
            //    PageSize = parameter.PageSize.Value;
            //    CurrentPage = parameter.PageNumber.Value;
            //}

        }
        private void SetFilter(Dictionary<string, string> filterValues)
        {
            //string filter = new GdSqlFilter(); 
            string filter = "";

            foreach (GridRow column in GridRows)
            {
                if (filterValues != null && filterValues.ContainsKey(column.DbName))
                {
                    column.FilterValue = filterValues[column.Caption];
                }

                foreach (DataRow field in this.GridTable.GridTable.Rows)
                {
                    //if (column.DbName == field.FieldName && column.FilterValue != null)
                    //{
                    //   if (column.IsDomain)
                    //    {
                    //        int val;
                    //        if (int.TryParse(column.FilterValue.ToString(), out val))
                    //        {
                    //            foreach (GdCodeValue cv in column.DomainSelectModel)
                    //            {
                    //                if (cv.Code == val && cv.Code != -1)
                    //                {
                    //                    string fN = column.DomainFieldName ?? column.Caption;
                    //                    sqlfilterConditions.Add(fN + "=@" + fN);
                    //                    filter.Add(fN, val);
                    //                    column.DomainSelectModel.SelectedIndex = column.DomainSelectModel.IndexOf(cv);

                    //                }
                    //                else if (cv.Code == val && cv.Code == -1)//statik bir tabloda -1 (tümü) durumu için -1 olmayan tüm değerler için eklendi..
                    //                {
                    //                    string fN = column.DomainFieldName ?? column.Caption;
                    //                    sqlfilterConditions.Add("( " + fN + " !=@" + fN + " or " + fN + " is null )");
                    //                    filter.Add(fN, val);
                    //                    column.DomainSelectModel.SelectedIndex = column.DomainSelectModel.IndexOf(cv);
                    //                }
                    //            }
                    //        }
                    //    }
                    //    else if (field.FieldType == GdDataType.Date)
                    //    {
                    //        DateTime date = DateTime.Parse(column.FilterValue.ToString());
                    //        sqlfilterConditions.Add(column.DbName + " = @" + column.DbName);
                    //        filter.Add(column.DbName, date);
                    //    }
                    //    else if (field.FieldType == GdDataType.String)
                    //    {
                    //        //if (column.DbName.Equals("birim_adi") && string.IsNullOrWhiteSpace(column.FilterValue))
                    //        //    continue;

                    //        if (string.IsNullOrWhiteSpace(column.FilterValue))
                    //            continue;

                    //        if (column.FindCorrectValue)
                    //            sqlfilterConditions.Add(column.DbName + " LIKE '" + column.FilterValue + "' ");
                    //        else
                    //            sqlfilterConditions.Add(column.DbName + " LIKE '%" + column.FilterValue + "%' ");

                    //        //sqlfilterConditions.Add(column.DbName + " LIKE '%'||@" + column.DbName + "||'%'");
                    //        //filter.Add(column.DbName, column.FilterValue);
                    //    }
                    //    else
                    //    {
                    //        if (column.FindCorrectValue)
                    //            sqlfilterConditions.Add("CAST(" + column.DbName + " as text) LIKE ''||@" + column.DbName + "||''");
                    //        else
                    //            sqlfilterConditions.Add("CAST(" + column.DbName + " as text) LIKE '%'||@" + column.DbName + "||'%'");

                    //        switch (field.FieldType)
                    //        {
                    //            case GdDataType.Integer:
                    //                filter.Add(column.DbName, int.Parse(column.FilterValue.ToString()));
                    //                break;
                    //            case GdDataType.Boolean:
                    //                filter.Add(column.DbName, bool.Parse(column.FilterValue.ToString()));
                    //                break;
                    //            case GdDataType.Real:
                    //                filter.Add(column.DbName,
                    //                    double.Parse(column.FilterValue.ToString(), CultureInfo.CurrentCulture));
                    //                break;
                    //        }
                    //    }
                    //}
                }
            }
            //filter.Text = string.Join(" AND ", sqlfilterConditions);
            //if (!string.IsNullOrEmpty(filter.Text))
            //{
            //    Table.SqlFilter = filter;
            //}
        }

    }

    public class DataGridTable
    {
        public ElDbTable GridTable { get; set; }
        public string KeyField { get; set; }

        public DataGridTable(ElDbTable gridTable, string keyField)
        {
            GridTable = gridTable;
            KeyField = keyField;
          
        }
    }

    public class GridRow
    {
        public string Caption { get; set; }
        public string DbName { get; set; }
        public string RowWith { get; set; }
        public bool IsSortuble { get; set; }
        public InputDataType DataType { get; set; }
        public string FilterValue { get; internal set; }
        public bool IsDomain { get; set; }

        public GridRow(string caption, string dbName, string with, InputDataType type)
        {
            Caption = caption;
            DbName = dbName;
            RowWith = with;
            DataType = type;
        }
    }
     
    public class ActionButton
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string FunctionName { get; set; }

        public ActionButton(string title, string url, string functionName)
        {
            Title = title;
            Url = url;
            FunctionName = functionName;
        }
    }
    
}