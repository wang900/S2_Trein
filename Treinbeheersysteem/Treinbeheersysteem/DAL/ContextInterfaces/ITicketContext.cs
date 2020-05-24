using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;

namespace Treinbeheersysteem.DAL.Contexts
{
    public interface ITicketContext
    {
        Ticket GetTicketbyId(long id);
        List<Ticket> GetAllTickets();
        long CreateTicket(Ticket t);
        bool UpdateTicket(Ticket t);
    }
}
