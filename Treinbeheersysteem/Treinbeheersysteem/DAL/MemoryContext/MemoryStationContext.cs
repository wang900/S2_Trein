using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL.Model;

namespace Treinbeheersysteem.DAL.MemoryContext
{
    public class MemoryStationContext : BaseMemoryContext, IStationContext
    {
        #region Station
        public bool DeleteStation(long id)
        {
            Stations.FirstOrDefault(s => s.Id == id).Actief = false;
            return true;
        }

        public long CreateStation(Station s)
        {
            Stations.Add(s);
            return s.Id;
        }

        public List<Station> GetAllStations()
        {
            List<Station> StationKopie = new List<Station>(Stations);
            return StationKopie;
        }

        public Station GetStationbyId(long id)
        {
            Station station = Stations.FirstOrDefault(s => s.Id == id);
            return station;
        }

        public bool UpdateStation(Station s)
        {
            int index = Stations.FindIndex(st => st.Id == s.Id);
            Stations[index] = s;
            return true;
        }
        #endregion
    }
}
