using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL;
using Treinbeheersysteem.DAL.Model;

namespace Treinbeheersysteem.BLL.Repositories
{
    public class PerronRepository
    {
        IPerronContext Context;
        public PerronRepository(IPerronContext context)
        {
            Context = context;
        }

        public Perron GetPerronbyId(long id)
        {
            return Context.GetPerronbyId(id);
        }
        public List<Perron> GetAllPerrons()
        {
            return Context.GetAllPerrons();
        }
        public long CreatePerron(Perron p)
        {
            return Context.CreatePerron(p);
        }
        public bool UpdatePerron(Perron p)
        {
            return Context.UpdatePerron(p);
        }
        public bool DeletePerron(long id)
        {
            return Context.DeletePerron(id);
        }
    }
}
