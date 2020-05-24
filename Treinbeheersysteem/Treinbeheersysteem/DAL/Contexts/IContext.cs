using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;

namespace Treinbeheersysteem.DAL.Contexts
{
    public interface IContext
    {

        Account GetAccountbyId(long id);
        List<Account> GetAccountsbyVoornaam(string voornaam);
        List<Account> GetAllBeheerders();
        List<Account> GetAllTreinverkeersleiders();
        long CreateAccount(Account a);
        void UpdateAccount(Account a);
        void ActiveAccount(long id, bool b);

        Verbinding GetVerbindingbyId(long id);
        List<Verbinding> GetAllVerbindingen();
        long CreateVerbinding(Verbinding v);
        void UpdateVerbinding(Verbinding v);
        void ActiveVerbinding(long id, bool b);

        Station GetStationbyId(long id);
        List<Station> GetAllStations();
        long CreateStation(Station s);
        void UpdateStation(Station s);
        void ActiveStation(long id, bool b);

        Perron GetPerronbyId(long id);
        List<Perron> GetAllPerrons();
        long CreatePerron(Perron p);
        void UpdatePerron(Perron p);
        void ActivePerron(long id, bool b);

        Ticket GetTicketbyId(long id);
        List<Ticket> GetAllTickets();
        long CreateTicket(Ticket t);
        void UpdateTicket(Ticket t);

        Traject GetTrajectbyId(long id);
        List<Traject> GetAllTrajecten();
        long CreateTraject(Traject t);
        void UpdateTraject(Traject t);
        void ActiveTraject(long id, bool b);

        Trein GetTreinbyId(long id);
        List<Trein> GetAllTreinen();
        long CreateTrein(Trein t);
        void UpdateTrein(Trein t);
        void ActiveTrein(long id, bool b);

        Wagon GetWagonbyId(long id);
        List<Wagon> GetAllWagons();
        long CreateWagon(Wagon t);
        void UpdateWagon(Wagon t);
        void ActiveWagon(long id, bool b);
    }
}
