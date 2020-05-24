using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;

namespace Treinbeheersysteem.DAL.Contexts
{
    public interface IWagonContext
    {
        Wagon GetWagonbyId(long id);
        List<Wagon> GetAllWagons();
        long CreateWagon(Wagon w);
        bool UpdateWagon(Wagon w);
        bool DeleteWagon(long id);
    }
}
