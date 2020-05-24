using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;

namespace Treinbeheersysteem.DAL.Contexts
{
    public interface IVerbindingContext
    {
        Verbinding GetVerbindingbyId(long id);
        List<Verbinding> GetAllVerbindingen();
        long CreateVerbinding(Verbinding v);
        bool UpdateVerbinding(Verbinding v);
        bool DeleteVerbinding(long id);
    }
}
