using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Treinbeheersysteem.DAL.Contexts
{
    public abstract class BaseMSSQLContext
    {
        private readonly string connectionString;

        public BaseMSSQLContext(string connString)
        {
            connectionString = connString;
        }

        public DataSet ExecuteSql(string sql, List<KeyValuePair<string, string>> parameters)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = conn.CreateCommand();

                foreach (KeyValuePair<string, string> kvp in parameters)
                {
                    SqlParameter param = new SqlParameter
                    {
                        ParameterName = "@" + kvp.Key,
                        Value = kvp.Value
                    };
                    cmd.Parameters.Add(param);
                }

                cmd.CommandText = sql;
                da.SelectCommand = cmd;

                conn.Open();
                da.Fill(ds);
                conn.Close();
            }
            catch (Exception)
            {
                return null;
            }

            return ds;
        }
    }
}