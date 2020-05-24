using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Treinbeheersysteem.DAL.Model
{
    public class Positie : Entity
    {
        public int X_Coördinaat { get; set; }
        public int Y_Coördinaat { get; set; }

        public Positie()
        {

        }

        public Positie(int id, int x_Coördinaat, int y_Coördinaat)
        {
            Id = id;
            X_Coördinaat = x_Coördinaat;
            Y_Coördinaat = y_Coördinaat;
        }
    }
}
