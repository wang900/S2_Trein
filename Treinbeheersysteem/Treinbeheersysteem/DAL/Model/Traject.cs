using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Treinbeheersysteem.DAL.Model
{
    public class Traject : Entity
    {
        public string Naam { get; set; }
        public DateTime BeginTijd { get; set; }
        public List<DateTime> Aankomsttijden { get; set; }
        public List<DateTime> Vertrektijden { get; set; }
        public List<Verbinding> Verbindingen { get; set; }
        public Trein Trein { get; set; }
        public bool Actief { get; set; }

        public Traject()
        {

        }

        public Traject(long id, string naam, DateTime beginTijd, Trein trein)
        {
            Id = id;
            Naam = naam;
            BeginTijd = beginTijd;
            Trein = trein;
            Verbindingen = new List<Verbinding>();
            Aankomsttijden = new List<DateTime>();
            Vertrektijden = new List<DateTime>();
        }

        public void VerbindingenToevoegen(Verbinding verbinding)
        {
            Verbindingen.Add(verbinding);
        }

        public void AankomsttijdToevoegen(DateTime aankomsttijd)
        {
            Aankomsttijden.Add(aankomsttijd);
        }

        public void VertrektijdToevoegen(DateTime vertrektijd)
        {
            Vertrektijden.Add(vertrektijd);
        }
    }
}
