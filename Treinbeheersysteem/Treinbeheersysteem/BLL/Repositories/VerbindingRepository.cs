using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL;
using Treinbeheersysteem.DAL.Model;

namespace Treinbeheersysteem.BLL.Repositories
{
    public class VerbindingRepository
    {
        IVerbindingContext Context;
        public VerbindingRepository(IVerbindingContext context)
        {
            Context = context;
        }

        public Verbinding GetVerbindingbyId(long id)
        {
            return Context.GetVerbindingbyId(id);
        }

        public List<Verbinding> GetAllVerbindingen()
        {
            return Context.GetAllVerbindingen();
        }

        public long CreateVerbinding(Verbinding v)
        {
            return Context.CreateVerbinding(v);
        }

        public bool UpdateVerbinding(Verbinding v)
        {
            return Context.UpdateVerbinding(v);
        }

        public bool DeleteVerbinding (long id)
        {
            return Context.DeleteVerbinding(id);
        }
    }
}
