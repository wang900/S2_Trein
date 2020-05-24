using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;
using Treinbeheersysteem.Parsers;

namespace Treinbeheersysteem.DAL.Contexts
{
    public class MSSQLTicketContext : BaseMSSQLContext, ITicketContext
    {
        public MSSQLTicketContext(string connString) : base(connString)
        {
        }

        public long CreateTicket(Ticket t)
        {
            string sql = "INSERT INTO Ticket (klasse, prijs, beginStationId, eindStationId, persoonId) VALUES(@klasse,@prijs,@beginStationId,@eindStationId,@persoonId); SELECT SCOPE_IDENTITY()";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("klasse", ((int)t.Klasse).ToString()),
                new KeyValuePair<string, string>("prijs", t.prijs.ToString()),
                new KeyValuePair<string, string>("beginStationId", t.BeginStation.Id.ToString()),
                new KeyValuePair<string, string>("eindStationId", t.EindStation.Id.ToString()),
                new KeyValuePair<string, string>("persoonId", t.Persoon.Id.ToString()),
            };

            DataSet result = ExecuteSql(sql, parameters);
            long newId = Convert.ToInt64(Math.Round(Convert.ToDecimal(result.Tables[0].Rows[0][0]), 0));
            return newId;
        }

        public List<Ticket> GetAllTickets()
        {
            string sql = "Select * from Ticket";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            DataSet results = ExecuteSql(sql, parameters);

            List<Ticket> tickets = new List<Ticket>();
            if (results != null)
            {

                for (int x = 0; x < results.Tables[0].Rows.Count; x++)
                {
                    Ticket p = DataSetParser.DataSetToTicket(results, x);
                    tickets.Add(p);
                }
            }
            return tickets;
        }

        public Ticket GetTicketbyId(long id)
        {
            string sql = "Select * from Ticket where id=@id";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("id", id.ToString())
            };

            DataSet results = ExecuteSql(sql, parameters);
            Ticket p = null;

            if (results != null && results.Tables[0].Rows.Count > 0)
            {
                p = DataSetParser.DataSetToTicket(results, 0);
            }
            return p;
        }

        public bool UpdateTicket(Ticket t)
        {
            string query = "update Ticket set @fields where Id = @id";

            string fields = "";
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("id", t.Id.ToString())
                };

            if (t.Klasse != default(Klasse))
            {
                if (!string.IsNullOrWhiteSpace(fields))
                    fields += ",";
                fields += "[klasse] = @klasse";
                parameters.Add(new KeyValuePair<string, string>("klasse", ((int)t.Klasse).ToString()));
            }
            if (t.Datum == DateTime.Today)
            {
                if (!string.IsNullOrWhiteSpace(fields))
                    fields += ",";
                fields += "[actief] = @actief";
                parameters.Add(new KeyValuePair<string, string>("actief", t.Datum.ToString("yyyy-mm-dd")));
            }

            query = query.Replace("@fields", fields);

            ExecuteSql(query, parameters);
            return true;
        }
    }
}
