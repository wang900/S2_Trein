using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL.Model;

namespace xUnitTestTreinbeheersysteem
{
    public abstract class RemoveData
    {
        public void EmptyLists()
        {
            #region Rol
            BaseMemoryContext.Rollen = new List<Rol>()
            {
            new Rol()
            {
                Id = 1,
                Naam = "Administrator"
            },
            new Rol()
            {
                Id = 2,
                Naam = "Beheerder"
            },

            new Rol()
            {
                Id = 3,
                Naam = "Treinverkeersleider"
            },
            };
            #endregion

            #region Accounts
            BaseMemoryContext.Accounts = new List<Account>()
            {
            new Account(1, "gebruikersnaam", BaseMemoryContext.Rollen.First(r => r.Id == 2), new Persoon("voornaam", "achternaam", "email"), true),
            new Account(2, "gebruikersnaa", BaseMemoryContext.Rollen.First(r => r.Id == 3), new Persoon("voornaam", "achternaam", "email"), true),
            new Account(3, "gebruikersna", BaseMemoryContext.Rollen.First(r => r.Id == 3), new Persoon("voornaam", "achternaam", "email"), true),
            new Account(4, "gebruikersn", BaseMemoryContext.Rollen.First(r => r.Id == 3), new Persoon("voornaam", "achternaam", "email"), true),
            new Account(5, "gebruikers", BaseMemoryContext.Rollen.First(r => r.Id == 2), new Persoon("voornaam", "achternaam", "email"), true)
            };
            #endregion

            #region Verbindingen
            BaseMemoryContext.Verbindingen = new List<Verbinding>()
        {
            new Verbinding(1, "naam", 50, new Perron(1, "naam", true), new Perron(1, "naam", true), true),
            new Verbinding(2, "naam", 50, new Perron(1, "naam", true), new Perron(1, "naam", true), true),
            new Verbinding(3, "naam", 40, new Perron(1, "naam", true), new Perron(1, "naam", true), true),

        };
            #endregion

            #region Stations
            BaseMemoryContext.Stations = new List<Station>()
        {
            new Station(1, "naam", new Positie(1, 23, 32), true),
            new Station(2, "naam", new Positie(2, 34, 32), true),
        };
            #endregion

            #region Perrons
            BaseMemoryContext.Perrons = new List<Perron>()
        {
            new Perron(1, "naam", true),
            new Perron(2, "naam", true),

        };
            #endregion

            #region Tickets
            BaseMemoryContext.Tickets = new List<Ticket>()
        {
            new Ticket(1, Klasse.TweedeKlasse, new Station(1, "naam", new Positie(1, 23, 42), true), new Station(2, "naam", new Positie(1, 53, 42), true), new Persoon("Henk", "van der Den", "email"), DateTime.Today),
            new Ticket(2, Klasse.EersteKlasse, new Station(1, "naam", new Positie(1, 23, 42), true), new Station(2, "naam", new Positie(1, 53, 42), true), new Persoon("Henk", "van der Den", "email"), DateTime.Today),
            new Ticket(3, Klasse.TweedeKlasse, new Station(1, "naam", new Positie(1, 23, 42), true), new Station(2, "naam", new Positie(1, 53, 42), true), new Persoon("Henk", "van der Den", "email"), DateTime.Today),
        };
            #endregion

            #region Treinen
            BaseMemoryContext.Treinen = new List<Trein>()
        {
            new Trein(1, "naam", 231, new Positie(1, 32, 42)),
            new Trein(2, "naam", 231, new Positie(1, 32, 42)),
            new Trein(3, "naam", 231, new Positie(1, 32, 42))
        };
            #endregion

            #region Trajecten
            BaseMemoryContext.Trajecten = new List<Traject>()
        {
            new Traject(1, "naam", DateTime.Today, new Trein(1, "naam", 231, new Positie(1, 32, 42))),
            new Traject(2, "naam", DateTime.Today, new Trein(1, "naam", 231, new Positie(1, 32, 42))),
            new Traject(3, "naam", DateTime.Today, new Trein(1, "naam", 231, new Positie(1, 32, 42)))
        };
            #endregion

            #region Wagons
            BaseMemoryContext.Wagons = new List<Wagon>()
        {
            new Wagon(1, "naam", 13, 25),
            new Wagon(2, "naam", 13, 25),
            new Wagon(3, "naam", 13, 25),
        };
            #endregion
        }
    }
}
