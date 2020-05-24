using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;
using Treinbeheersysteem.Parsers;

namespace Treinbeheersysteem.DAL.Contexts
{
    public class MSSQLPerronContext : BaseMSSQLContext, IPerronContext
    {
        public MSSQLPerronContext(string connString) : base(connString)
        {
        }

        public long CreatePerron(Perron p)
        {
            string sql = "INSERT INTO Perron (naam, stationId, actief) VALUES(@naam,@stationId,@actief); SELECT SCOPE_IDENTITY()";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("naam", p.Naam.ToString()),
                new KeyValuePair<string, string>("stationId", p.StationId.ToString()),
                new KeyValuePair<string, string>("actief", "1")
            };

            DataSet result = ExecuteSql(sql, parameters);
            long newId = Convert.ToInt64(Math.Round(Convert.ToDecimal(result.Tables[0].Rows[0][0]), 0));
            return newId;
        }

        public bool DeletePerron(long id)
        {
            try
            {
                string query = "update Perron set [actief] = @actief where Id = @id";

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

        public List<Perron> GetAllPerrons()
        {
            string sql = "Select * from Perron ";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            DataSet results = ExecuteSql(sql, parameters);

            List<Perron> perrons = new List<Perron>();
            if (results != null)
            {

                for (int x = 0; x < results.Tables[0].Rows.Count; x++)
                {
                    Perron p = DataSetParser.DataSetToPerron(results, x);
                    perrons.Add(p);
                }
            }
            return perrons;
        }

        public List<Perron> GetAllPerronsbyStationId(long stationId)
        {
            string sql = "Select * from Perron where stationId = @stationId";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("stationId", stationId.ToString())
            };
            DataSet results = ExecuteSql(sql, parameters);

            List<Perron> perrons = new List<Perron>();
            if (results != null)
            {
                for (int x = 0; x < results.Tables[0].Rows.Count; x++)
                {
                    Perron p = DataSetParser.DataSetToPerron(results, x);
                    perrons.Add(p);
                }
            }
            return perrons;
        }

        public Perron GetPerronbyId(long id)
        {
            string sql = "Select * from Perron where id=@id";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("id", id.ToString())
            };

            DataSet results = ExecuteSql(sql, parameters);
            Perron p = null;

            if (results != null && results.Tables[0].Rows.Count > 0)
            {
                p = DataSetParser.DataSetToPerron(results, 0);
            }
            return p;

        }

        public bool UpdatePerron(Perron p)
        {
            string query = "update Perron set [actief] = @actief, @fields where Id = @id";

            string fields = "";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("id", p.Id.ToString()),
                    new KeyValuePair<string, string>("actief", p.Actief? "1" : "0")
                };

            if (p.Naam != null)
            {
                if (!string.IsNullOrWhiteSpace(fields))
                    fields += ",";
                fields += "[naam] = @naam";
                parameters.Add(new KeyValuePair<string, string>("naam", p.Naam));
            }

            query = query.Replace("@fields", fields);

            ExecuteSql(query, parameters);
            return true;
        }
    }
}
