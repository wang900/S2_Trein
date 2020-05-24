using System;
using Treinbeheersysteem.BLL.Repositories;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL.MemoryContext;
using Treinbeheersysteem.DAL.Model;
using Xunit;

namespace xUnitTestTreinbeheersysteem
{
    public class TestTicketRepository : RemoveData
    {
        readonly ITicketContext context = new MemoryTicketContext();
        TicketRepository ticketRepository;
        

        [Fact]
        public void CreateTicket()
        {
            EmptyLists();

            ticketRepository = new TicketRepository(context);
            Ticket ticket = new Ticket(4 ,Klasse.TweedeKlasse, new Station(1, "naam", new Positie(1, 23, 42), true), new Station(2, "naam", new Positie(1, 53, 42), true), new Persoon("Henk", "dsa", "email"), DateTime.Today);
            
            Assert.Equal(4, ticketRepository.CreateTicket(ticket));
        }

        [Fact]
        public void GetAllTickets()
        {
            EmptyLists();

            ticketRepository = new TicketRepository(context);

            Assert.Equal(3, ticketRepository.GetAllTickets().Count);
        }

        [Fact]
        public void GetTicketbyId()
        {
            EmptyLists();

            ticketRepository = new TicketRepository(context);
            Ticket ticket = new Ticket(1, Klasse.TweedeKlasse, new Station(1, "naam", new Positie(1, 23, 42), true), new Station(2, "naam", new Positie(1, 53, 42), true), new Persoon("Henk", "van der Den", "email"), DateTime.Today);

            Assert.Equal(ticket.Id, ticketRepository.GetTicketbyId(1).Id);
            Assert.Equal(ticket.Persoon.Voornaam, ticketRepository.GetTicketbyId(1).Persoon.Voornaam);
            Assert.Equal(ticket.Datum, ticketRepository.GetTicketbyId(1).Datum);
        }

        [Fact]
        public void UpdateTicket()
        {
            EmptyLists();

            ticketRepository = new TicketRepository(context);
            Ticket ticket = new Ticket(1, Klasse.TweedeKlasse, new Station(1, "dsa", new Positie(1, 23, 42), true), new Station(2, "gb", new Positie(1, 53, 42), true), new Persoon("Henk", "van der Den", "email12"), DateTime.Today);
            
            Assert.True(ticketRepository.UpdateTicket(ticket));
        }
    }
}
