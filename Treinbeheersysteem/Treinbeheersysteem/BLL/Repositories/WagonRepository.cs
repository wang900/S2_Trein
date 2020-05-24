using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL;
using Treinbeheersysteem.DAL.Model;

namespace Treinbeheersysteem.BLL.Repositories
{
    public class WagonRepository
    {
        IWagonContext Context;
        public WagonRepository(IWagonContext context)
        {
            Context = context;
        }

        public Wagon GetWagonbyId(long id)
        {
            return Context.GetWagonbyId(id);
        }

        public List<Wagon> GetAllWagons()
        {
            return Context.GetAllWagons();
        }

        public long CreateWagon(Wagon w)
        {
            return Context.CreateWagon(w);
        }

        public bool UpdateWagon(Wagon w)
        {
            return Context.UpdateWagon(w);
        }

        public bool DeleteWagon(long id)
        {
            return Context.DeleteWagon(id);
        }

    }
}
