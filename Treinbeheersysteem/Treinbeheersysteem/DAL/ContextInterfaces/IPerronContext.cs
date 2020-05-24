using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;

namespace Treinbeheersysteem.DAL.Contexts
{
    public interface IPerronContext
    {
        Perron GetPerronbyId(long id);
        List<Perron> GetAllPerrons();
        List<Perron> GetAllPerronsbyStationId(long stationId);
        long CreatePerron(Perron p);
        bool UpdatePerron(Perron p);
        bool DeletePerron(long id);
    }
}
