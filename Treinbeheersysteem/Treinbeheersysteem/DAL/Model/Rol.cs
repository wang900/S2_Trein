using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Treinbeheersysteem.DAL.Model
{
    public class Rol : Entity
    {
        public string Naam { get; set; }
        public DateTime CreatieDatum { get; set; }

    }
}
