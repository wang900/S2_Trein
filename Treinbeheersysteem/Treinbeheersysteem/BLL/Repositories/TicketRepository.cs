using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL;
using Treinbeheersysteem.DAL.Model;

namespace Treinbeheersysteem.BLL.Repositories
{
    public class TicketRepository
    {
        ITicketContext Context;
        public TicketRepository(ITicketContext context)
        {
            Context = context;
        }

        public Ticket GetTicketbyId(long id)
        {
            return Context.GetTicketbyId(id);
        }

        public List<Ticket> GetAllTickets()
        {
            return Context.GetAllTickets();
        }

        public long CreateTicket(Ticket t)
        {
            return Context.CreateTicket(t);
        }

        public bool UpdateTicket(Ticket t)
        {
            return Context.UpdateTicket(t);
        }
        
    }
}
