using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Treinbeheersysteem.DAL.Model
{
    public enum Klasse
    {
        EersteKlasse,
        TweedeKlasse
    }
    public class Ticket : Entity
        
    {
        public readonly double prijs;
        public Klasse Klasse { get; set; }
        public Station BeginStation { get; set; }
        public Station EindStation { get; set; }
        public Persoon Persoon { get; set; }
        public DateTime Datum { get; set; }

        public Ticket(long id, Klasse klasse, Station beginStation, Station eindStation, Persoon persoon, DateTime datum)
        {
            Id = id;
            Klasse = klasse;
            BeginStation = beginStation;
            EindStation = eindStation;
            Persoon = persoon;
            Datum = datum;
        }

        public Ticket()
        {
        }

        public Ticket(long id, Klasse klasse, Station beginStation, Station eindStation, DateTime datum)
        {
            Id = id;
            Klasse = klasse;
            BeginStation = beginStation;
            EindStation = eindStation;
            Datum = datum;
            prijs = PrijsBerekenen(BeginStation, EindStation);
        }

        public void Verbindingentoevoegen()
        {

        }

        public double PrijsBerekenen(Station beginStation, Station eindStation)
        {
            //Bereken prijs met de verbindinglengte tussen de twee stations.
            double prijs = 0;
            
            return prijs;
        }
    }
}
