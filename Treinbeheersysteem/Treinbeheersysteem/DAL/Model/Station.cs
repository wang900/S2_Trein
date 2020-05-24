using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Treinbeheersysteem.DAL.Model
{
    public class Station : Entity
    {
        public string Naam { get; set; }
        public bool Actief { get; set; }
        public List<Perron> Perrons { get; set; }
        public Positie Positie { get; set; }
        
        public Station()
        {

        }

        public Station(long id, string naam, Positie positie, bool actief)
        {
            Id = id;
            Naam = naam;
            Actief = actief;
            Positie = positie;
            Perrons = new List<Perron>();
        }

        public void PerronToevoegen(Perron perron)
        {
            Perrons.Add(perron);
        }
    }
}
