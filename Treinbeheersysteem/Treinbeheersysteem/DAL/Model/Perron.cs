using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Treinbeheersysteem.DAL.Model
{
    public class Perron : Entity
    {
        public string Naam { get; set; }
        public long StationId { get; set; }
        public bool Actief { get; set; }

        public Perron()
        {

        }

        public Perron(long id, string naam, bool actief)
        {
            Id = id;
            Naam = naam;
            Actief = actief;
        }


    }
}
