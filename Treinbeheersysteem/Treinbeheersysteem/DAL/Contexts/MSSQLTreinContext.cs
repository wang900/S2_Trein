using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;
using Treinbeheersysteem.Parsers;

namespace Treinbeheersysteem.DAL.Contexts
{
    public class MSSQLTreinContext : BaseMSSQLContext, ITreinContext
    {
        public MSSQLTreinContext(string connString) : base(connString)
        {
        }

        public long CreateTrein(Trein t)
        {
            string sql = "INSERT INTO Trein (naam, maxSnelheid, actief) VALUES(@naam,@maxSnelheid,@actief); SELECT SCOPE_IDENTITY()";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("naam", t.Naam.ToString()),
                new KeyValuePair<string, string>("maxSnelheid", t.MaxSnelheid.ToString()),
                new KeyValuePair<string, string>("actief", "1")
            };

            DataSet result = ExecuteSql(sql, parameters);
            long newId = Convert.ToInt64(Math.Round(Convert.ToDecimal(result.Tables[0].Rows[0][0]), 0));
            return newId;
        }

        public bool DeleteTrein(long id)
        {
            string query = "update Trein set [actief] = @actief where Id = @id";

            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("id", id.ToString()),
                    new KeyValuePair<string, string>("actief", "0")
                };

            ExecuteSql(query, parameters);
            return true;
        }

        public List<Trein> GetAllTreinen()
        {
            string sql = "Select * from Trein ";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            DataSet results = ExecuteSql(sql, parameters);

            List<Trein> treinen = new List<Trein>();
            if (results != null)
            {

                for (int x = 0; x < results.Tables[0].Rows.Count; x++)
                {
                    Trein p = DataSetParser.DataSetToTrein(results, x);
                    treinen.Add(p);
                }
            }
            return treinen;
        }

        public Trein GetTreinbyId(long id)
        {
            string sql = "Select * from Trein where id=@id";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("id", id.ToString())
            };

            DataSet results = ExecuteSql(sql, parameters);
            Trein p = null;

            if (results != null && results.Tables[0].Rows.Count > 0)
            {
                p = DataSetParser.DataSetToTrein(results, 0);
            }
            return p;
        }

        public bool UpdateTrein(Trein t)
        {
            string query = "update Trein set [actief] = @actief, @fields where Id = @id";

            string fields = "";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("id", t.Id.ToString()),
                    new KeyValuePair<string, string>("actief", t.Actief? "1" : "0")
                };

            if (t.Naam != null)
            {
                if (!string.IsNullOrWhiteSpace(fields))
                    fields += ",";
                fields += "[naam] = @naam";
                parameters.Add(new KeyValuePair<string, string>("naam", t.Naam));
            }
            if (t.MaxSnelheid != 0)
            {
                if (!string.IsNullOrWhiteSpace(fields))
                    fields += ",";
                fields += "[maxSnelheid] = @maxSnelheid";
                parameters.Add(new KeyValuePair<string, string>("maxSnelheid", t.MaxSnelheid.ToString()));
            }

            query = query.Replace("@fields", fields);

            ExecuteSql(query, parameters);
            return true;
        }
    }
}
