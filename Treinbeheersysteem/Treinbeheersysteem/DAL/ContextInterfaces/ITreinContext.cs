using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;

namespace Treinbeheersysteem.DAL.Contexts
{
    public interface ITreinContext
    {
        Trein GetTreinbyId(long id);
        List<Trein> GetAllTreinen();
        long CreateTrein(Trein t);
        bool UpdateTrein(Trein t);
        bool DeleteTrein(long id);
    }
}
