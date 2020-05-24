using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Treinbeheersysteem.DAL.Model
{
    public class Wagon : Entity
    {
        public string Naam { get; set; }
        public int Stoelaantal_Klasse1 { get; set; }
        public int Stoelaantal_Klasse2 { get; set; }
        public long TreinId { get; set; }
        public bool Actief { get; set; }

        public Wagon()
        {

        }

        public Wagon(long id, string naam, int stoelaantal_Klasse1, int stoelaantal_Klasse2)
        {
            Id = id;
            Naam = naam;
            Stoelaantal_Klasse1 = stoelaantal_Klasse1;
            Stoelaantal_Klasse2 = stoelaantal_Klasse2;
        }
    }
}
