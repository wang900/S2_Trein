using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;
using Treinbeheersysteem.Parsers;

namespace Treinbeheersysteem.DAL.Contexts
{
    public class MSSQLVerbindingContext : BaseMSSQLContext, IVerbindingContext
    {
        public MSSQLVerbindingContext(string connString) : base(connString)
        {
        }

        public long CreateVerbinding(Verbinding v)
        {
            string sql = "INSERT INTO Verbinding (naam, lengte, beginPerronId, eindPerronId, actief) VALUES(@naam,@lengte,@beginPerronId,@eindPerronId@actief); SELECT SCOPE_IDENTITY()";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("naam", v.Naam.ToString()),
                new KeyValuePair<string, string>("lengte", v.Lengte.ToString()),
                new KeyValuePair<string, string>("beginPerronId", v.BeginPerron.Id.ToString()),
                new KeyValuePair<string, string>("eindPerronId", v.EindPerron.Id.ToString()),
                new KeyValuePair<string, string>("actief", v.Actief ? "1" : "0")
            };

            DataSet result = ExecuteSql(sql, parameters);
            long newId = Convert.ToInt64(Math.Round(Convert.ToDecimal(result.Tables[0].Rows[0][0]), 0));
            return newId;
        }

        public bool DeleteVerbinding(long id)
        {
            string query = "update Verbinding set [actief] = @actief where Id = @id";

            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("id", id.ToString()),
                    new KeyValuePair<string, string>("actief", "0")
                };

            ExecuteSql(query, parameters);
            return true;
        }

        public List<Verbinding> GetAllVerbindingen()
        {
            string sql = "Select * from dbo.verbinding";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            DataSet results = ExecuteSql(sql, parameters);

            List<Verbinding> verbindingen = new List<Verbinding>();
            if (results != null)
            {
                for (int x = 0; x < results.Tables[0].Rows.Count; x++)
                {
                    Verbinding v = DataSetParser.DataSetToVerbinding(results, x);
                    verbindingen.Add(v);
                }
            }
            return verbindingen;

        }

        public Verbinding GetVerbindingbyId(long id)
        {
            string sql = "Select * from Verbinding where id=@id";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("id", id.ToString())
            };

            DataSet results = ExecuteSql(sql, parameters);
            Verbinding v = null;

            if (results != null && results.Tables[0].Rows.Count > 0)
            {
                v = DataSetParser.DataSetToVerbinding(results, 0);
            }
            return v;
        }

        public bool UpdateVerbinding(Verbinding v)
        {
            string query = "update Verbinding set [actief] = @actief, @fields where Id = @id";

            string fields = "";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("id", v.Id.ToString()),
                    new KeyValuePair<string, string>("actief", v.Actief ? "1" : "0")
                };

            if (v.Naam != null)
            {
                if (!string.IsNullOrWhiteSpace(fields))
                    fields += ",";
                fields += "[naam] = @naam";
                parameters.Add(new KeyValuePair<string, string>("naam", v.Naam));
            }

            query = query.Replace("@fields", fields);

            ExecuteSql(query, parameters);
            return true;
        }
    }
}
