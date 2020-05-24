using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL;
using Treinbeheersysteem.DAL.Model;

namespace Treinbeheersysteem.BLL.Repositories
{
    public class TreinRepository
    {
        ITreinContext Context;
        public TreinRepository(ITreinContext context)
        {
            Context = context;
        }

        public Trein GetTreinbyId(long id)
        {
            return Context.GetTreinbyId(id);
        }

        public List<Trein> GetAllTreinen()
        {
            return Context.GetAllTreinen();
        }

        public long CreateTrein(Trein t)
        {
            return Context.CreateTrein(t);
        }

        public bool UpdateTrein(Trein t)
        {
            return Context.UpdateTrein(t);
        }

        public bool DeleteTrein(long id)
        {
            return Context.DeleteTrein(id);
        }
    }
}
