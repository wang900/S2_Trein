using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;

namespace Treinbeheersysteem.DAL.Contexts
{
    public interface ITrajectContext
    {
        Traject GetTrajectbyId(long id);
        List<Traject> GetAllTrajecten();
        long CreateTraject(Traject t);
        bool UpdateTraject(Traject t);
        bool DeleteTraject(long id);
        List<Traject> GetTrajectenbyStations(long beginStationId, long eindStationId);
    }
}
