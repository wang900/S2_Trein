using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Treinbeheersysteem.BLL.Repositories;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL.Model;
using TreinbeheersysteemMain.Converters;
using TreinbeheersysteemMain.Models;

namespace TreinbeheersysteemMain.Controllers
{
    public class HomeController : Controller
    {
        readonly ITicketContext ticketContext;
        readonly TicketRepository ticketRepo;
        readonly TicketViewModelConverter ticketConverter;
        readonly IStationContext stationContext;
        readonly StationRepository stationRepo;
        readonly StationViewModelConverter stationConverter;
        readonly ITrajectContext trajectContext;
        readonly TrajectRepository trajectRepo;
        readonly TrajectViewModelConverter trajectConverter;
        public HomeController(IConfiguration configuration)
        {
            ticketContext = new MSSQLTicketContext(configuration.GetConnectionString("DefaultConnection"));
            ticketRepo = new TicketRepository(ticketContext);
            ticketConverter = new TicketViewModelConverter();
            stationContext = new MSSQLStationContext(configuration.GetConnectionString("DefaultConnection"));
            stationRepo = new StationRepository(stationContext);
            stationConverter = new StationViewModelConverter();
            trajectContext = new MSSQLTrajectContext(configuration.GetConnectionString("DefaultConnection"));
            trajectRepo = new TrajectRepository(trajectContext);
            trajectConverter = new TrajectViewModelConverter();

        }

        public IActionResult Index()
        {
            TicketDetailViewModel vm = new TicketDetailViewModel
            {
                StationViewModel = new StationViewModel()
            };
            vm.StationViewModel.Stations = stationConverter.StationListToViewModelList(stationRepo.GetAllStations()).ToList();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(TicketDetailViewModel vm)
        {
            TicketDetailViewModel tvm = new TicketDetailViewModel();
            if (ModelState.IsValid)
            {
                Ticket ticket = ticketConverter.ViewModelToTicket(vm);
                
                if (trajectRepo.GetTrajectenbyStations(ticket.BeginStation.Id, ticket.EindStation.Id) != null)
                {
                    tvm = new TicketDetailViewModel
                    {
                        TrajectViewModel = new TrajectViewModel()
                    };
                    tvm.TrajectViewModel.Trajecten = trajectConverter.TrajectListToViewModelList(trajectRepo.GetTrajectenbyStations(ticket.BeginStation.Id, ticket.EindStation.Id)).ToList();
                    return RedirectToAction("Reis", tvm);
                }
            }
            else
            {
                tvm = new TicketDetailViewModel
                {
                    StationViewModel = new StationViewModel()
                };
                tvm.StationViewModel.Stations = stationConverter.StationListToViewModelList(stationRepo.GetAllStations()).ToList();
            }
            return View(tvm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Reis(TicketDetailViewModel vm)
        {
            if (ModelState.IsValid)
            {
                return View(vm);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
