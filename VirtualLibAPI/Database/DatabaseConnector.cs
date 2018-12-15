using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;
using VirtualLibDatabase.Entities;
using VirtualLibrarity.Models.Entities;

namespace VirtualLibrarity.Database
{
    public static class DatabaseConnector
    {
        private static MySqlConnection _conn;

        public static void DatabaseConnectionInit()
        {
            string connString = "server=localhost;uid=root;pwd=skacania;database=vl";
            _conn = new MySqlConnection
            {
                ConnectionString = connString
            };
        }
        public static DbResponse GetData(MySqlCommand cmd)
        {
            try
            {
                _conn.Open();
                cmd.Connection = _conn;
                cmd.ExecuteScalar();
                var adapter = new MySqlDataAdapter(cmd);
                var res = new DataTable();

                adapter.Fill(res);
                if (res.Rows.Count > 0)
                {
                    return new DbResponse
                    {
                        Success = true,
                        Data = res
                    };
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception exc)
            {
                return new DbResponse
                {
                    Success = false,
                    Message = exc.Message
                };
            }
            finally
            {
                _conn.Close();
            }
        }
        public static bool UpdateData(MySqlCommand cmd)
        {
            try
            {
                _conn.Open();
                cmd.Connection = _conn;
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception exc)
            {
                Debug.Print(exc.Message);
                return false;
            }
            finally
            {
                _conn.Close();
            }
        }
    }
}
