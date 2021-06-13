using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace elrincon.web.Manager
{
    public class ElDbTable : DataTable, IElDbTable
    {
        public new string TableName;
        public string DefaultName { get; set; }
        private string _sql;
        private int _offset;
        private int _offsetFetch;
        private string _orderby;

        public SqlConnection _sqlConnection;
        public SqlTransaction _sqlTransaction;
        public SqlCommand _command;
        public string KeyField { get; set; }

        
        public string Orderby
        {
            get => _orderby; 
            set
            {
                _orderby = value;
                //ElDbTable elDbTable = this;
                //elDbTable.DefaultView.Sort = value;
                //DataTableReader rd = new DataTableReader(elDbTable.DefaultView.ToTable());
                //this.Clear();
                //this.Load(rd);
            }
        }
         

        public string Sql
        {
            get => _sql;
            set => _sql = value;
        }

        public int Offset
        {
            get => _offset;
            set
            {
                string order = " order by id ";
                
                _offset = value;
                this.Clear();

                if (!string.IsNullOrWhiteSpace(_orderby))
                    order = _orderby;


                _sql = _sql + order + " OFFSET "+ _offset + " ROWS FETCH NEXT "+ _offsetFetch +" ROWS ONLY ";

                ExecuteTable(_sql);
            }
        }

        public int OffsetFetch
        {
            get => _offsetFetch;
            set => _offsetFetch = value;
        }


        public ElDbTable()
        {
            _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["elrincon"].ConnectionString);
            _command = new SqlCommand();
        }

        /// <summary>
        /// get table
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public ElDbTable GetTable(string tableName)
        {
            TableName = tableName;
            using (_sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["elrincon"].ConnectionString))
            {
                try
                {
                    _sqlConnection.Open();

                    _command = new SqlCommand();

                    _command = _sqlConnection.CreateCommand();
                    _command.Connection = _sqlConnection;

                    _command = new SqlCommand("Select * from (select * from " + tableName + " ) as " + tableName,
                        _sqlConnection);

                    _sql = _command.CommandText;

                    SqlDataReader rdr = _command.ExecuteReader();

                    this.Load(rdr);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                    throw new Exception(e.Message);
                }
            }

            return this;
        }

        public ElDbTable ExecuteTable(string query)
        {
            _sqlConnection.Close();
            _sqlConnection.Dispose();
            _command.Dispose();

            using (_sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["elrincon"].ConnectionString))
            {
                try
                {
                    _sqlConnection.Open();
                    _command = new SqlCommand();

                    _command = _sqlConnection.CreateCommand();
                    _command.Connection = _sqlConnection;



                    _command.CommandText = "select * from (" + query + ") as table_query ";
                    _sql = _command.CommandText;

                    this.Load(_command.ExecuteReader());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //_sqlConnection.Close();
                    //_sqlConnection.Dispose();
                    throw new Exception(ex.Message);

                }
            }

            return this;
        }

        /// <summary>
        /// insert table
        /// </summary>
        /// <param name="updateData"></param>
        /// <returns></returns>
        public int InsertTable(RowBufferModel updateData)
        {
            int insertId = 0;
            using (_sqlConnection = GetSqlConnection())
            {
                try
                { 
                    //_sqlConnection.Open();
                    _command.Parameters.Clear();

                    string text = "INSERT INTO " + TableName + "(";

                    foreach (KeyValuePair<string, object> kvp in updateData)
                        text += kvp.Key + ",";

                    text = text.Remove(text.Length - 1) + ") VALUES( ";

                    foreach (KeyValuePair<string, object> kvp in updateData)
                        text += "@" + kvp.Key + ",";

                    text = text.Remove(text.Length - 1) + ") ";

                    _command.CommandText = text + ";SELECT CAST(scope_identity() AS int)";

                    foreach (KeyValuePair<string, object> kv in updateData)
                        _command.Parameters.AddWithValue("@" + kv.Key, kv.Value);

                    _sql = _command.CommandText;

                    //insertId = Convert.ToInt32(_command.ExecuteNonQuery());
                    insertId = Convert.ToInt32(_command.ExecuteScalar());
                    //insertId = (int)_command.ExecuteScalar();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw new Exception(e.Message);
                }
            }

            return insertId;
        }
        
        /// <summary>
        /// update table
        /// </summary>
        /// <param name="updateData"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateTable(RowBufferModel updateData, int id,string keyfield = "id")
        {
            using (_sqlConnection = GetSqlConnection())
            {
                try
                {
                    string text = "UPDATE " + this.TableName + " SET ";

                    foreach (KeyValuePair<string, object> kvp in updateData)
                    {
                        //var tt = kvp.Value.GetType();
                        //if(tt.Name.Equals("DateTime"))
                        //    text += kvp.Key + "= CAST('" + kvp.Value + "' AS DATETIME),";
                        //else
                        
                            text += kvp.Key + "=@" + kvp.Key+ ",";
                         
                    }
                    text = text.Remove(text.Length - 1);

                    foreach (KeyValuePair<string, object> kv in updateData)
                        _command.Parameters.AddWithValue("@" + kv.Key, kv.Value);

                    _command.CommandText = text + " WHERE "+keyfield+"=" + id;
                    _sql = _command.CommandText;

                    _command.ExecuteNonQuery();

                    return true;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                    throw new Exception(e.Message);
                }
            }
        }
        
        /// <summary>
        /// delete row
        /// </summary>
        /// <param name="keyField"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public bool Delete(object keyField, object keyValue)
        {
            using (_sqlConnection = GetSqlConnection())
            {
                try
                {
                    _command.CommandText = "delete from " + this.TableName + " where " + keyField + "=" + keyValue + ";";
                    _sql = _command.CommandText;

                    _command.ExecuteNonQuery();

                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                    throw new Exception(e.Message);
                }
            }
        }
        
        /// <summary>
        /// delete row
        /// </summary>
        /// <param name="tableName"></param> 
        /// <returns></returns>
        public bool Delete(object tableName)
        {
            using (_sqlConnection = GetSqlConnection())
            {
                try
                {
                    _command.CommandText = "delete from " + tableName + ";";
                    _sql = _command.CommandText;

                    _command.ExecuteNonQuery();

                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                    throw new Exception(e.Message);
                }
            }
        }

        /// <summary>
        /// get sql connection
        /// </summary>
        /// <returns></returns>
        private SqlConnection GetSqlConnection()
        {
             _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["elrincon"].ConnectionString);
            _command = new SqlCommand();
            _sqlConnection.Open();

            _command = _sqlConnection.CreateCommand();
            _command.Connection = _sqlConnection;
             
            return _sqlConnection;
        }

        /// <summary>
        /// begin transaction
        /// </summary>
        public void BeginTransaction()
        {
            _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["elrincon"].ConnectionString);
            _command = new SqlCommand();

            if (_sqlConnection.State == ConnectionState.Closed)
            {
                _sqlConnection.Open();

                _sqlTransaction = _sqlConnection.BeginTransaction(IsolationLevel.RepeatableRead, "SampleTransaction");

                _command = _sqlConnection.CreateCommand();
                _command.Connection = _sqlConnection;
                _command.Transaction = _sqlTransaction;
            }
        }

        /// <summary>
        /// commit transaction
        /// </summary>
        public void CommitTransaction()
        {
            if((_sqlConnection.State & ConnectionState.Open) != 0)
            {
                _sqlTransaction.Commit();
                _sqlConnection.Close();
                
            }
        }

        /// <summary>
        /// roolback trans
        /// </summary>
        public void RoolbackTransaction()
        {
            if ((_sqlConnection.State & ConnectionState.Open) != 0)
            {
                _sqlTransaction.Rollback();

                _sqlConnection.Close();

            }
        }
        public static bool IsNumericType(object o)
        {
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
    }
}