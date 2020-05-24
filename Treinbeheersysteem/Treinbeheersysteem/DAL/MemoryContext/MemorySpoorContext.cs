using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL.Model;

namespace Treinbeheersysteem.DAL.MemoryContext
{
    public class MemoryVerbindingContext : BaseMemoryContext, IVerbindingContext
    {
        #region Verbinding
        public bool DeleteVerbinding(long id)
        {
            Verbindingen.FirstOrDefault(v => v.Id == id).Actief = false;
            return true;
        }

        public long CreateVerbinding(Verbinding v)
        {
            Verbindingen.Add(v);
            return v.Id;
        }

        public List<Verbinding> GetAllVerbindingen()
        {
            List<Verbinding> Verbindingkopie = new List<Verbinding>(Verbindingen);
            return Verbindingkopie;
        }

        public Verbinding GetVerbindingbyId(long id)
        {
            Verbinding verbinding = Verbindingen.FirstOrDefault(v => v.Id == id);
            return verbinding;
        }

        public bool UpdateVerbinding(Verbinding v)
        {
            int index = Verbindingen.FindIndex(x => x.Id == v.Id);
            Verbindingen[index] = v;
            return true;
        }
        #endregion
    }
}
