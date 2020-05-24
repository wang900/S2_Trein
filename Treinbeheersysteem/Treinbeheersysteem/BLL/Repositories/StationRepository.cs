using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL;
using Treinbeheersysteem.DAL.Model;

namespace Treinbeheersysteem.BLL.Repositories
{
    public class StationRepository
    {
        IStationContext Context;
        public StationRepository(IStationContext context)
        {
            Context = context;
        }

        public Station GetStationbyId(long id)
        {
            return Context.GetStationbyId(id);
        }

        public List<Station> GetAllStations()
        {
            return Context.GetAllStations();
        }

        public long CreateStation(Station s)
        {
            return Context.CreateStation(s);
        }

        public bool UpdateStation(Station s)
        {
            return Context.UpdateStation(s);
        }

        public bool DeleteStation(long id)
        {
            return Context.DeleteStation(id);
        }
    }
}
