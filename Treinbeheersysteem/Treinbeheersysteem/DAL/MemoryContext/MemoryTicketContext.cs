using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL.Model;

namespace Treinbeheersysteem.DAL.MemoryContext
{
    public class MemoryTicketContext : BaseMemoryContext, ITicketContext
    {
        #region Ticket

        public long CreateTicket(Ticket t)
        {
            Tickets.Add(t);
            return t.Id;
        }

        public List<Ticket> GetAllTickets()
        {
            return new List<Ticket>(Tickets);
        }
        public Ticket GetTicketbyId(long id)
        {
            Ticket ticket = Tickets.FirstOrDefault(t => t.Id == id);
            return ticket;
        }
        public bool UpdateTicket(Ticket ticket)
        {
            int index = Tickets.FindIndex(t => t.Id == ticket.Id);
            Tickets[index] = ticket;
            return true;
        }
        #endregion
    }
}
