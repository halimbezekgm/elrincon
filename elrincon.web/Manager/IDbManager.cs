using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace elrincon.web.Manager
{
    interface IDbManager
    {
        DataTable GetTable(string tableName);
        int InsertTable(string tableName, RowBufferModel updateData);
        bool  UpdateTable(string tableName, RowBufferModel updateData, int id);
        bool Delete(object keyField, object keyValue, string tableName);
        SqlDataReader GetTableReader(string sql);
    }
}
