using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Treinbeheersysteem.DAL.Model
{
    public class Verbinding : Entity
    {
        public string Naam { get; set; }
        public int Lengte { get; set; }
        public Perron BeginPerron { get; set; }
        public Perron EindPerron { get; set; }
        public bool Actief { get; set; }

        public Verbinding()
        {

        }

        public Verbinding(long id, string naam, int lengte, Perron beginPerron, Perron eindPerron, bool actief)
        {
            Id = id;
            Naam = naam;
            Lengte = lengte;
            BeginPerron = beginPerron;
            EindPerron = eindPerron;
            Actief = actief;
        }
    }
}
