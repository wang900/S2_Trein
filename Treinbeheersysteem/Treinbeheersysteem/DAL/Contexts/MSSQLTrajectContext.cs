using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;
using Treinbeheersysteem.Parsers;

namespace Treinbeheersysteem.DAL.Contexts
{
    public class MSSQLTrajectContext : BaseMSSQLContext, ITrajectContext
    {
        public MSSQLTrajectContext(string connString) : base(connString)
        {
        }

        public long CreateTraject(Traject t)
        {
            string sql = "INSERT INTO Traject (naam, actief) VALUES(@naam,@actief); SELECT SCOPE_IDENTITY()";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("naam", t.Naam.ToString()),
                new KeyValuePair<string, string>("actief", "1")
            };

            DataSet result = ExecuteSql(sql, parameters);
            long newId = Convert.ToInt64(Math.Round(Convert.ToDecimal(result.Tables[0].Rows[0][0]), 0));
            return newId;
        }

        public bool DeleteTraject(long id)
        {
            try
            {
                string query = "update Traject set [actief] = @actief where Id = @id";

                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("id", id.ToString()),
                    new KeyValuePair<string, string>("actief", "0")
                };

                ExecuteSql(query, parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Traject> GetAllTrajecten()
        {
            string sql = "Select * from Traject ";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            DataSet results = ExecuteSql(sql, parameters);

            List<Traject> trajecten = new List<Traject>();
            if (results != null)
            {
                for (int x = 0; x < results.Tables[0].Rows.Count; x++)
                {
                    Traject t = DataSetParser.DataSetToTraject(results, x);
                    trajecten.Add(t);
                }
            }
            return trajecten;
        }

        public Traject GetTrajectbyId(long id)
        {
            string sql = "Select * from Traject where id=@id";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("id", id.ToString())
            };

            DataSet results = ExecuteSql(sql, parameters);
            Traject t = null;

            if (results != null && results.Tables[0].Rows.Count > 0)
            {
                t = DataSetParser.DataSetToTraject(results, 0);
            }
            return t;
        }

        public bool UpdateTraject(Traject t)
        {
            string query = "update Traject set [actief] = @actief, @fields where Id = @id";

            string fields = "";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("id", t.Id.ToString()),
                    new KeyValuePair<string, string>("actief", t.Actief ? "1" : "0")
                };

            if (t.Naam != null)
            {
                if (!string.IsNullOrWhiteSpace(fields))
                    fields += ",";
                fields += "[naam] = @naam";
                parameters.Add(new KeyValuePair<string, string>("naam", t.Naam));
            }

            query = query.Replace("@fields", fields);

            ExecuteSql(query, parameters);
            return true;
        }

        public List<Traject> GetTrajectenbyStations(long beginStationId, long eindStationId)
        {
            try
            {
                string query = "SELECT id, naam, actief FROM GetTrajects" +
                    " WHERE beginPerronId IN ( SELECT Id FROM Perron WHERE stationId = @beginstationid)" +
                    " AND eindPerronId IN ( SELECT Id FROM Perron WHERE = @eindstationid";
                
                List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("beginstationid", beginStationId.ToString()),
                    new KeyValuePair<string, string>("eindstationid", eindStationId.ToString())
                };
                DataSet results = ExecuteSql(query, parameters);
                List<Traject> trajecten = new List<Traject>();
                if (results != null)
                {
                    for (int x = 0; x < results.Tables[0].Rows.Count; x++)
                    {
                        Traject t = DataSetParser.DataSetToTraject(results, x);
                        trajecten.Add(t);
                    }
                }
                return trajecten;
            }
            catch
            {
                return null;
            }
        }
    }
}
