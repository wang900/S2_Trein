using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL;
using Treinbeheersysteem.DAL.Model;

namespace Treinbeheersysteem.BLL.Repositories
{
    public class TrajectRepository
    {
        ITrajectContext Context;
        public TrajectRepository(ITrajectContext context)
        {
            Context = context;
        }
        public Traject GetTrajectbyId(long id)
        {
            return Context.GetTrajectbyId(id);
        }

        public List<Traject> GetAllTrajecten()
        {
            return Context.GetAllTrajecten();
        }

        public long CreateTraject(Traject t)
        {
            return Context.CreateTraject(t);
        }

        public bool UpdateTraject(Traject t)
        {
            return Context.UpdateTraject(t);
        }

        public bool DeleteTraject(long id)
        {
            return Context.DeleteTraject(id);
        }

        public List<Traject> GetTrajectenbyStations(long beginStationId, long eindStationId)
        {
            return Context.GetTrajectenbyStations(beginStationId, eindStationId);
        }
    }
}
