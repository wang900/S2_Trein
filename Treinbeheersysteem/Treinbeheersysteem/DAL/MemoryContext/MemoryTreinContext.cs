using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL.Model;

namespace Treinbeheersysteem.DAL.MemoryContext
{
    public class MemoryTreinContext : BaseMemoryContext, ITreinContext
    {
        #region Trein
        public bool DeleteTrein(long id)
        {
            Treinen.FirstOrDefault(t => t.Id == id).Actief = false;
            return true;
        }
        public long CreateTrein(Trein t)
        {
            Treinen.Add(t);
            return t.Id;
        }
        public Trein GetTreinbyId(long id)
        {
            Trein trein = Treinen.FirstOrDefault(t => t.Id == id);
            return trein;
        }
        public List<Trein> GetAllTreinen()
        {
            return new List<Trein>(Treinen);
        }
        public bool UpdateTrein(Trein trein)
        {
            int index = Treinen.FindIndex(t => t.Id == trein.Id);
            Treinen[index] = trein;
            return true;
        }
        #endregion
    }
}
