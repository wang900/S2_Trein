using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL.Model;

namespace Treinbeheersysteem.DAL.MemoryContext
{
    public class MemoryTrajectContext : BaseMemoryContext, ITrajectContext
    {
        #region Traject
        public bool DeleteTraject(long id)
        {
            Trajecten.FirstOrDefault(t => t.Id == id).Actief = false;
            return true;
        }

        public long CreateTraject(Traject treinreis)
        {
            Trajecten.Add(treinreis);
            return treinreis.Id;
        }
        public List<Traject> GetAllTrajecten()
        {
            return new List<Traject>(Trajecten);
        }
        public Traject GetTrajectbyId(long id)
        {
            Traject treinreis = Trajecten.FirstOrDefault(t => t.Id == id);
            return treinreis;
        }
        public bool UpdateTraject(Traject treinreis)
        {
            int index = Trajecten.FindIndex(t => t.Id == treinreis.Id);
            Trajecten[index] = treinreis;
            return true;
        }

        public List<Traject> GetTrajectenbyStations(long beginStationId, long eindStationId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
