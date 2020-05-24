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
    public class PerronController : Controller
    {
        readonly IPerronContext context;
        readonly IStationContext stationContext;
        readonly PerronRepository repo;
        readonly StationRepository stationRepo;
        readonly PerronViewModelConverter converter;
        readonly StationViewModelConverter stationConverter;
        public PerronController(IConfiguration configuration)
        {
            context = new MSSQLPerronContext(configuration.GetConnectionString("DefaultConnection"));
            stationContext = new MSSQLStationContext(configuration.GetConnectionString("DefaultConnection"));
            repo = new PerronRepository(context);
            stationRepo = new StationRepository(stationContext);
            converter = new PerronViewModelConverter();
            stationConverter = new StationViewModelConverter();
        }

        public IActionResult Index()
        {
            List<Perron> perrons = repo.GetAllPerrons();
            PerronViewModel vm = new PerronViewModel
            {
                Perrons = new List<PerronDetailViewModel>()
            };
            foreach (Perron perron in perrons)
            {
                vm.Perrons.Add(converter.PerronToViewModel(perron));
            }

            return View(vm.Perrons);
        }

        public IActionResult Details(int id)
        {
            if (id < 1)
                return BadRequest("Id cannot be 0");

            Perron perron = repo.GetPerronbyId(id);
            if (perron == null)
                return BadRequest("Perron could not be found");

            PerronDetailViewModel vm = converter.PerronToViewModel(perron);
            return View(vm);
        }

        // GET: Perron/Create
        [Authorize(Roles = "Beheerder")]
        public IActionResult Create()
        {
            PerronDetailViewModel vm = new PerronDetailViewModel
            {
                StationViewModel = new StationViewModel(),
            };
            vm.StationViewModel.Stations = stationConverter.StationListToViewModelList(stationRepo.GetAllStations()).ToList();
            return View(vm);
        }

        // POST: Perron/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public IActionResult Create(PerronDetailViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Perron p = converter.ViewModelToPerron(vm);
                long id = repo.CreatePerron(p);
                return RedirectToAction("Details", new { id });
            }
            return View();
        }

        // GET: Perron/Edit/5
        [Authorize(Roles = "Beheerder")]
        public IActionResult Edit(int id)
        {
            try
            {
                PerronDetailViewModel vm = converter.PerronToViewModel(repo.GetPerronbyId(id));
                return View(vm);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Perron/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public IActionResult Edit(PerronDetailViewModel vm)
        {
            try
            {
                Perron perron = converter.ViewModelToPerron(vm);
                bool succes = repo.UpdatePerron(perron);
                if (succes)
                {
                    return RedirectToAction("Details", new { perron.Id });
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Beheerder")]
        public IActionResult Delete(int id)
        {
            repo.DeletePerron(id);
            return RedirectToAction(nameof(Index));
        }
    }
}