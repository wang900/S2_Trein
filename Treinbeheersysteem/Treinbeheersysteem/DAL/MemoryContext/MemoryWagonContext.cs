using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL.Model;

namespace Treinbeheersysteem.DAL.MemoryContext
{
    public class MemoryWagonContext : BaseMemoryContext, IWagonContext
    {
        #region Wagon
        public bool DeleteWagon(long id)
        {
            Wagons.FirstOrDefault(w => w.Id == id).Actief = false;
            return true;
        }
        
        public long CreateWagon(Wagon wagon)
        {
            Wagons.Add(wagon);
            return wagon.Id;
        }
        
        public List<Wagon> GetAllWagons()
        {
            return new List<Wagon>(Wagons);
        }
        
        public Wagon GetWagonbyId(long id)
        {
            Wagon wagon = Wagons.FirstOrDefault(w => w.Id == id);
            return wagon;
        }
        
        public bool UpdateWagon(Wagon wagon)
        {
            int index = Wagons.FindIndex(w => w.Id == wagon.Id);
            Wagons[index] = wagon;
            return true;
        }
        #endregion
    }
}
