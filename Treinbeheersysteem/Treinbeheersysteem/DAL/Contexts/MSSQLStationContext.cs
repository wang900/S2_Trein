using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;
using Treinbeheersysteem.Parsers;

namespace Treinbeheersysteem.DAL.Contexts
{
    public class MSSQLStationContext : BaseMSSQLContext, IStationContext
    {
        public MSSQLStationContext(string connString) : base(connString)
        {
        }

        public long CreateStation(Station s)
        {
            string sql = "INSERT INTO Station (naam, actief) VALUES(@naam,@actief); SELECT SCOPE_IDENTITY()";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("naam", s.Naam.ToString()),
                new KeyValuePair<string, string>("actief", s.Actief ? "1" : "0")
            };

            DataSet result = ExecuteSql(sql, parameters);
            long newId = Convert.ToInt64(Math.Round(Convert.ToDecimal(result.Tables[0].Rows[0][0]), 0));
            return newId;
        }

        public bool DeleteStation(long id)
        {
            string query = "update Station set Actief = @actief where Id = @id";

            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("id", id.ToString()),
                    new KeyValuePair<string, string>("actief", "0")
                };

            ExecuteSql(query, parameters);
            return true;
        }

        public List<Station> GetAllStations()
        {
            string sql = "Select * from Station";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            DataSet results = ExecuteSql(sql, parameters);

            List<Station> stations = new List<Station>();
            if (results != null)
            {

                for (int x = 0; x < results.Tables[0].Rows.Count; x++)
                {
                    Station s = DataSetParser.DataSetToStation(results, x);
                    stations.Add(s);
                }
            }
            return stations;
        }

        public Station GetStationbyId(long id)
        {
            string sql = "Select * from Station where id=@id";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("id", id.ToString())
            };

            DataSet results = ExecuteSql(sql, parameters);
            Station s = null;

            if (results != null && results.Tables[0].Rows.Count > 0)
            {
                s = DataSetParser.DataSetToStation(results, 0);
            }
            return s;
        }

        public bool UpdateStation(Station s)
        {
            string query = "update Station set [actief] = @actief, @fields where Id = @id";

            string fields = "";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("id", s.Id.ToString()),
                    new KeyValuePair<string, string>("actief", s.Actief ? "1" : "0")
                };

            if (s.Naam != null)
            {
                if (!string.IsNullOrWhiteSpace(fields))
                    fields += ",";
                fields += "[naam] = @naam";
                parameters.Add(new KeyValuePair<string, string>("naam", s.Naam));
            }

            query = query.Replace("@fields", fields);

            ExecuteSql(query, parameters);
            return true;
        }
    }
}
