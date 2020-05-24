using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;

namespace Treinbeheersysteem.DAL.Contexts
{
    public class MSSQLContext : IContext
    {
        readonly string connectionString = "Server=mssql.fhict.local;Database=dbi410315;User Id=dbi410315;Password=YourChoosenPassword;";

        public void ActiveAccount(long id, bool b)
        {
            
        }

        private DataSet ExecuteSql(string sql, List<KeyValuePair<string, string>> parameters)
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
            catch
            {
                return null;
            }

            return ds;
        }

        public void ActiveVerbinding(long id, bool b)
        {
            throw new NotImplementedException();
        }

        public void ActiveStation(long id, bool b)
        {
            throw new NotImplementedException();
        }

        public void ActivePerron(long id, bool b)
        {
            throw new NotImplementedException();
        }

        public void ActiveTicket(long id, bool b)
        {
            throw new NotImplementedException();
        }

        public void ActiveTrein(long id, bool b)
        {
            throw new NotImplementedException();
        }

        public void ActiveTraject(long id, bool b)
        {
            throw new NotImplementedException();
        }

        public void ActiveWagon(long id, bool b)
        {
            throw new NotImplementedException();
        }

        public long CreateAccount(Account a)
        {
            throw new NotImplementedException();
        }

        public long CreateVerbinding(Verbinding v)
        {
            throw new NotImplementedException();
        }

        public long CreateStation(Station s)
        {
            throw new NotImplementedException();
        }

        public long CreatePerron(Perron p)
        {
            throw new NotImplementedException();
        }

        public long CreateTicket(Ticket t)
        {
            throw new NotImplementedException();
        }

        public long CreateTrein(Trein t)
        {
            throw new NotImplementedException();
        }

        public long CreateTraject(Traject t)
        {
            throw new NotImplementedException();
        }

        public long CreateWagon(Wagon t)
        {
            throw new NotImplementedException();
        }

        public Account GetAccountbyId(long id)
        {
            throw new NotImplementedException();
        }

        public List<Account> GetAllBeheerders()
        {
            throw new NotImplementedException();
        }

        public List<Verbinding> GetAllVerbindingen()
        {
            throw new NotImplementedException();
        }

        public List<Station> GetAllStations()
        {
            throw new NotImplementedException();
        }

        public List<Perron> GetAllPerrons()
        {
            throw new NotImplementedException();
        }

        public List<Ticket> GetAllTickets()
        {
            throw new NotImplementedException();
        }

        public List<Trein> GetAllTreinen()
        {
            throw new NotImplementedException();
        }

        public List<Traject> GetAllTrajecten()
        {
            throw new NotImplementedException();
        }

        public List<Account> GetAllTreinverkeersleiders()
        {
            throw new NotImplementedException();
        }

        public List<Wagon> GetAllWagons()
        {
            throw new NotImplementedException();
        }

        public Verbinding GetVerbindingbyId(long id)
        {
            throw new NotImplementedException();
        }

        public Station GetStationbyId(long id)
        {
            throw new NotImplementedException();
        }

        public Perron GetPerronbyId(long id)
        {
            throw new NotImplementedException();
        }

        public Ticket GetTicketbyId(long id)
        {
            throw new NotImplementedException();
        }

        public Trein GetTreinbyId(long id)
        {
            throw new NotImplementedException();
        }

        public Traject GetTrajectbyId(long id)
        {
            throw new NotImplementedException();
        }

        public Wagon GetWagonbyId(long id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAccount(Account a)
        {
            throw new NotImplementedException();
        }

        public void UpdateVerbinding(Verbinding v)
        {
            throw new NotImplementedException();
        }

        public void UpdateStation(Station s)
        {
            throw new NotImplementedException();
        }

        public void UpdatePerron(Perron p)
        {
            throw new NotImplementedException();
        }

        public void UpdateTicket(Ticket t)
        {
            throw new NotImplementedException();
        }

        public void UpdateTrein(Trein t)
        {
            throw new NotImplementedException();
        }

        public void UpdateTraject(Traject t)
        {
            throw new NotImplementedException();
        }

        public void UpdateWagon(Wagon t)
        {
            throw new NotImplementedException();
        }

        public List<Account> GetAccountsbyVoornaam(string voornaam)
        {
            throw new NotImplementedException();
        }
    }
}
