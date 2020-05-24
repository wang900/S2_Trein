using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;

namespace Treinbeheersysteem.DAL.Contexts
{
    public interface IStationContext
    {
        Station GetStationbyId(long id);
        List<Station> GetAllStations();
        long CreateStation(Station s);
        bool UpdateStation(Station s);
        bool DeleteStation(long id);
    }
}
