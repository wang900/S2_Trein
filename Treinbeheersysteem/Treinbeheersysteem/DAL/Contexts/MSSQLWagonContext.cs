using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;
using Treinbeheersysteem.Parsers;

namespace Treinbeheersysteem.DAL.Contexts
{
    public class MSSQLWagonContext : BaseMSSQLContext, IWagonContext
    {
        public MSSQLWagonContext(string connString) : base(connString)
        {
        }

        public long CreateWagon(Wagon w)
        {
            string sql = "INSERT INTO Wagon (naam, stoelenEersteKlasse, stoelenTweedeKlasse, actief, treinId) VALUES(@naam, @stoelenEersteKlasse, @stoelenTweedeKlasse,@actief,@treinId); SELECT SCOPE_IDENTITY()";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("naam", w.Naam.ToString()),
                new KeyValuePair<string, string>("stoelenEersteKlasse", w.Stoelaantal_Klasse1.ToString()),
                new KeyValuePair<string, string>("naam", w.Stoelaantal_Klasse2.ToString()),
                new KeyValuePair<string, string>("actief", w.Actief ? "1" : "0"),
                new KeyValuePair<string, string>("treinId", w.TreinId.ToString())

            };

            DataSet result = ExecuteSql(sql, parameters);
            long newId = Convert.ToInt64(Math.Round(Convert.ToDecimal(result.Tables[0].Rows[0][0]), 0));
            return newId;
        }

        public bool DeleteWagon(long id)
        {
            string query = "update Wagon set [actief] = @actief where Id = @id";

            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("id", id.ToString()),
                    new KeyValuePair<string, string>("actief", "0")
                };

            ExecuteSql(query, parameters);
            return true;
        }

        public List<Wagon> GetAllWagons()
        {
            string sql = "Select * from Wagon";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            DataSet results = ExecuteSql(sql, parameters);

            List<Wagon> stations = new List<Wagon>();
            if (results != null)
            {

                for (int x = 0; x < results.Tables[0].Rows.Count; x++)
                {
                    Wagon w = DataSetParser.DataSetToWagon(results, x);
                    stations.Add(w);
                }
            }
            return stations;
        }

        public Wagon GetWagonbyId(long id)
        {
            string sql = "Select * from Wagon where id=@id";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("id", id.ToString())
            };

            DataSet results = ExecuteSql(sql, parameters);
            Wagon w = null;

            if (results != null && results.Tables[0].Rows.Count > 0)
            {
                w = DataSetParser.DataSetToWagon(results, 0);
            }
            return w;
        }

        public bool UpdateWagon(Wagon w)
        {
            string query = "update Wagon set [actief] = @actief, @fields where Id = @id";

            string fields = "";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("id", w.Id.ToString()),
                    new KeyValuePair<string, string>("actief", w.Actief ? "1" : "0")
                };

            if (w.Naam != null)
            {
                if (!string.IsNullOrWhiteSpace(fields))
                    fields += ",";
                fields += "[naam] = @naam";
                parameters.Add(new KeyValuePair<string, string>("naam", w.Naam));
            }
            if (w.TreinId != 0)
            {
                if (!string.IsNullOrWhiteSpace(fields))
                    fields += ",";
                fields += "[treinId] = @treinId";
                parameters.Add(new KeyValuePair<string, string>("treinId", w.TreinId.ToString()));
            }
            if (w.Stoelaantal_Klasse1 != 0)
            {
                if (!string.IsNullOrWhiteSpace(fields))
                    fields += ",";
                fields += "[stoelAantal_1] = @stoelAantal_1";
                parameters.Add(new KeyValuePair<string, string>("stoelAantal_1", w.Stoelaantal_Klasse1.ToString()));
            }
            if (w.Stoelaantal_Klasse2 != 0)
            {
                if (!string.IsNullOrWhiteSpace(fields))
                    fields += ",";
                fields += "[stoelAantal_2] = @stoelAantal_2";
                parameters.Add(new KeyValuePair<string, string>("stoelAantal_2", w.Stoelaantal_Klasse2.ToString()));
            }

            query = query.Replace("@fields", fields);

            ExecuteSql(query, parameters);
            return true;
        }
    }
}
