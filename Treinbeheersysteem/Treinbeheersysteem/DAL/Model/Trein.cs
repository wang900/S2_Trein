using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Treinbeheersysteem.DAL.Model
{
    public class Trein : Entity
    {
        public string Naam { get; set; }
        public int MaxSnelheid { get; set; }
        public Positie Positie { get; set; }
        public List<Wagon> Wagons { get; set; }
        public bool Actief { get; set; }

        public Trein()
        {

        }

        public Trein(long id, string naam, int maxSnelheid, Positie positie)
        {
            Id = id;
            Naam = naam;
            MaxSnelheid = maxSnelheid;
            Positie = positie;
            Wagons = new List<Wagon>();
        }

        public void WagonToevoegen(Wagon wagon)
        {
            Wagons.Add(wagon);
        }
    }
}
