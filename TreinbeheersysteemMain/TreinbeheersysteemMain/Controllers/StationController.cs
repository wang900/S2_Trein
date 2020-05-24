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
    [Authorize(Roles = "Beheerder, Treinverkeersleider")]
    public class StationController : Controller
    {
        readonly IStationContext context;
        readonly StationRepository repo;
        readonly StationViewModelConverter converter;
        public StationController(IConfiguration configuration)
        {
            context = new MSSQLStationContext(configuration.GetConnectionString("DefaultConnection"));
            repo = new StationRepository(context);
            converter = new StationViewModelConverter();
        }

        public IActionResult Index()
        {
            List<Station> stations = repo.GetAllStations();
            StationViewModel vm = new StationViewModel
            {
                Stations = new List<StationDetailViewModel>()
            };
            foreach (Station station in stations)
            {
                vm.Stations.Add(converter.StationToViewModel(station));
            }
            
            return View(vm.Stations);
        }
        
        public IActionResult Details(int id)
        {
            if (id < 1)
                return BadRequest("Id cannot be 0");

            Station station = repo.GetStationbyId(id);
            if (station == null)
                return BadRequest("Station could not be found");

            StationDetailViewModel vm = converter.StationToViewModel(station);
            return View(vm);
        }

        // GET: Station/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Station/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public IActionResult Create(StationDetailViewModel vm)
        {
            if (ModelState.IsValid)
            {
                StationViewModelConverter stationViewModelConverter = new StationViewModelConverter();
                Station s = stationViewModelConverter.ViewModelToStation(vm);
                decimal id = repo.CreateStation(s);
                return RedirectToAction("Details", new { id });
            }
            return View();
        }

        // GET: Station/Edit/5
        [Authorize(Roles = "Beheerder")]
        public IActionResult Edit(int id)
        {
            try
            {
                StationDetailViewModel vm = converter.StationToViewModel(repo.GetStationbyId(id));
                return View(vm);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
            
        }

        // POST: Station/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public IActionResult Edit(StationDetailViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Station station = converter.ViewModelToStation(vm);
                    repo.UpdateStation(station);
                    return RedirectToAction("Details", new { station.Id });
                }
                catch
                {

                }
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Beheerder")]
        public IActionResult Delete(int id)
        {
            repo.DeleteStation(id);
            return RedirectToAction(nameof(Index));
        }
    }
}