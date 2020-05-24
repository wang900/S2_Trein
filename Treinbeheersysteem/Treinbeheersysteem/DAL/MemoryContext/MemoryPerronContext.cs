using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL.Model;

namespace Treinbeheersysteem.DAL.MemoryContext
{
    public class MemoryPerronContext : BaseMemoryContext, IPerronContext
    {
        #region Perron
        public bool DeletePerron(long id)
        {
            Perrons.FirstOrDefault(p => p.Id == id).Actief = false;
            return true;
        }

        public long CreatePerron(Perron p)
        {
            Perrons.Add(p);
            return p.Id;
        }

        public List<Perron> GetAllPerrons()
        {
            return new List<Perron>(Perrons);
        }

        public Perron GetPerronbyId(long id)
        {
            Perron perron = Perrons.FirstOrDefault(s => s.Id == id);
            return perron;
        }

        public bool UpdatePerron(Perron p)
        {
            int index = Perrons.FindIndex(x => x.Id == p.Id);
            Perrons[index] = p;
            return true;
        }

        public List<Perron> GetAllPerronsbyStationId(long stationId)
        {
            return new List<Perron>(Perrons.FindAll(s => s.StationId == stationId));
        }
        #endregion
    }
}
