using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace elrincon.web.Manager
{
    //public class DbManager:IDbManager
    //{ 
    //    static SqlConnection GetSqlConnection()
    //    {
    //        string connectionString = ConfigurationManager.ConnectionStrings["elrincon"].ConnectionString;

    //        return new SqlConnection(connectionString);
    //    }
         
    //    /// <summary>
    //    /// get table
    //    /// </summary>
    //    /// <param name="tableName"></param>
    //    /// <returns></returns>
    //    public DataTable GetTable(string tableName)
    //    { 
    //        using (SqlConnection connection = GetSqlConnection())
    //        {
    //            try
    //            {
    //                connection.Open();
    //                SqlCommand command = new SqlCommand("Select * from " + tableName, connection);

    //                DataTable table = new DataTable();
    //                SqlDataReader rdr = command.ExecuteReader();

    //                table.Load(rdr);
                     
    //                return table;

    //            }
    //            catch (Exception e)
    //            {
    //                Console.WriteLine(e);
    //            }
    //            finally
    //            {
    //                connection.Close();
    //            }
    //        }

    //        return null;
    //    }

    //    public DataTable ExecuteTable(string query)
    //    {
    //        DataTable table = new DataTable();
    //        using (SqlConnection connection = GetSqlConnection())
    //        {
    //            connection.Open();
    //            SqlCommand command = connection.CreateCommand();
    //            try
    //            {
    //                command.CommandText = query;
    //                table.Load(command.ExecuteReader());
    //            }
    //            catch (Exception ex)
    //            {
    //                Console.WriteLine(ex.Message);
    //            }
    //        }
    //        return table;
    //    }

    //    /// <summary>
    //    /// get data reader
    //    /// </summary>
    //    /// <param name="sql"></param>
    //    /// <returns></returns>
    //    public SqlDataReader GetTableReader(string sql)
    //    {
    //        using (SqlConnection connection = GetSqlConnection())
    //        {
    //            try
    //            {
    //                connection.Open();
    //                SqlCommand command = new SqlCommand(sql, connection);

    //                SqlDataReader rdr = command.ExecuteReader();

    //                return rdr;

    //            }
    //            catch (Exception e)
    //            {
    //                Console.WriteLine(e);
    //            }
    //            finally
    //            {
    //                connection.Close();
    //            }
    //        }

    //        return null;
    //    }

    //    /// <summary>
    //    /// insert table
    //    /// </summary>
    //    /// <param name="tableName"></param>
    //    /// <param name="updateData"></param>
    //    /// <returns></returns>
    //    public int InsertTable(string tableName, RowBufferModel updateData)
    //    {
    //        int insertId = 0;
    //        using (SqlConnection connection = GetSqlConnection())
    //        {
    //            try
    //            {
    //                connection.Open();
    //                SqlCommand command = new SqlCommand();
    //                command.Connection = connection;

    //                string text = "INSERT INTO " + tableName + "(";

    //                foreach (KeyValuePair<string, object> kvp in updateData)
    //                    text += kvp.Key + ",";

    //                text = text.Remove(text.Length - 1) + ") VALUES( ";

    //                foreach (KeyValuePair<string, object> kvp in updateData)
    //                    text += "@" + kvp.Key + ",";

    //                text = text.Remove(text.Length - 1) + ") ";

    //                command.CommandText = text;
    //                command.Prepare();

    //                foreach (KeyValuePair<string, object> kv in updateData)
    //                    command.Parameters.AddWithValue("@" + kv.Key, kv.Value.ToString());

    //                insertId = Convert.ToInt32(command.ExecuteScalar());

    //                return insertId;
    //            }
    //            catch (Exception e)
    //            {
    //                Console.WriteLine(e);
    //                throw new Exception(e.Message);
    //            }
    //            finally
    //            {
    //                connection.Close();
    //            }
    //        }

    //        return insertId;
    //    }

    //    /// <summary>
    //    /// update table
    //    /// </summary>
    //    /// <param name="tableName"></param>
    //    /// <param name="updateData"></param>
    //    /// <param name="id"></param>
    //    /// <returns></returns>
    //    public bool UpdateTable( string tableName, RowBufferModel updateData, int id)
    //    {
    //        using (SqlConnection connection = GetSqlConnection())
    //        {
    //            try
    //            {
    //                connection.Open();
    //                SqlTransaction transaction = connection.BeginTransaction();
    //                SqlCommand command = new SqlCommand();
    //                command.Connection = connection;
    //                command.Transaction = transaction;

    //                string text = "UPDATE " + tableName + " SET ";

    //                foreach (KeyValuePair<string, object> kvp in updateData)
    //                    text += kvp.Key + "='" + kvp.Value + "',";

    //                text = text.Remove(text.Length - 1);
    //                command.CommandText = text + " WHERE id=" + id;

    //                command.ExecuteNonQuery();

    //                transaction.Commit();

    //                return true;

    //            }
    //            catch (Exception e)
    //            {
    //                Console.WriteLine(e);
    //            }
    //            finally
    //            {
    //                connection.Close();
    //            }
    //        }

    //        return false;
    //    }

    //    /// <summary>
    //    /// delete row
    //    /// </summary>
    //    /// <param name="keyField"></param>
    //    /// <param name="keyValue"></param>
    //    /// <param name="tableName"></param>
    //    /// <returns></returns>
    //    public bool Delete(object keyField, object keyValue, string tableName)
    //    {
    //        using (var connection = GetSqlConnection())
    //        {
    //            try
    //            {
    //                connection.Open();
    //                SqlCommand command = new SqlCommand();
    //                command.Connection = connection;
    //                command.CommandText = "delete from " + tableName + " where "+ keyField + "=" + keyValue + ";";
    //                command.ExecuteNonQuery();

    //                return true;
    //            }
    //            catch (Exception e)
    //            {
    //                Console.WriteLine(e);
    //            }
    //            finally
    //            {
    //                connection.Close();
    //            }
    //        }

    //        return false;
    //    }
         
    //}
}