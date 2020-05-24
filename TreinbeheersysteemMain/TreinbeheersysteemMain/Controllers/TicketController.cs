using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Treinbeheersysteem.BLL.Repositories;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL.Model;
using TreinbeheersysteemMain.Converters;
using TreinbeheersysteemMain.Models;

namespace TreinbeheersysteemMain.Controllers
{
    [Authorize(Roles = "Beheerder")]
    public class TicketController : Controller
    {
        readonly ITicketContext context;
        readonly TicketRepository repo;
        readonly TicketViewModelConverter converter;
        readonly IStationContext stationContext;
        readonly StationRepository stationRepo;
        readonly StationViewModelConverter stationConverter;
        public TicketController(IConfiguration configuration)
        {
            context = new MSSQLTicketContext(configuration.GetConnectionString("DefaultConnection"));
            repo = new TicketRepository(context);
            converter = new TicketViewModelConverter();
            stationContext = new MSSQLStationContext(configuration.GetConnectionString("DefaultConnection"));
            stationRepo = new StationRepository(stationContext);
            stationConverter = new StationViewModelConverter();
        }

        public IActionResult Index()
        {
            List<Ticket> tickets = repo.GetAllTickets();
            TicketViewModel vm = new TicketViewModel
            {
                Tickets = new List<TicketDetailViewModel>()
            };
            foreach (Ticket ticket in tickets)
            {
                vm.Tickets.Add(converter.TicketToViewModel(ticket));
            }
            
            return View(vm.Tickets);
        }
        
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            if (id < 1)
                return BadRequest("Id cannot be 0");

            Ticket ticket = repo.GetTicketbyId(id);
            if (ticket == null)
                return BadRequest("Ticket could not be found");

            TicketDetailViewModel vm = converter.TicketToViewModel(ticket);
            return View(vm);
        }

        // GET: Ticket/Create
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ticket/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult Create(TicketDetailViewModel vm)
        {
            if (ModelState.IsValid)
            {
                TicketViewModelConverter ticketViewModelConverter = new TicketViewModelConverter();
                Ticket t = ticketViewModelConverter.ViewModelToTicket(vm);
                decimal id = repo.CreateTicket(t);
                return RedirectToAction("Details", new { id });
            }
            return View();
        }

        // GET: Ticket/Edit/5
        public IActionResult Edit(int id)
        {
            try
            {
                TicketDetailViewModel vm = converter.TicketToViewModel(repo.GetTicketbyId(id));
                return View(vm);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Ticket/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TicketDetailViewModel vm)
        {
            try
            {
                Ticket ticket = converter.ViewModelToTicket(vm);
                repo.UpdateTicket(ticket);
                return RedirectToAction("Details", new { ticket.Id });
            }
            catch
            {
                return View();
            }
        }
    }
}