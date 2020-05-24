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
    public class WagonController : Controller
    {
        readonly IWagonContext context;
        readonly WagonRepository repo;
        readonly WagonViewModelConverter converter;
        public WagonController(IConfiguration configuration)
        {
            context = new MSSQLWagonContext(configuration.GetConnectionString("DefaultConnection"));
            repo = new WagonRepository(context);
            converter = new WagonViewModelConverter();
        }

        public IActionResult Index()
        {
            List<Wagon> wagons = repo.GetAllWagons();
            WagonViewModel vm = new WagonViewModel
            {
                Wagons = new List<WagonDetailViewModel>()
            };
            foreach (Wagon wagon in wagons)
            {
                vm.Wagons.Add(converter.WagonToViewModel(wagon));
            }
            
            return View(vm.Wagons);
        }
        
        public IActionResult Details(int id)
        {
            if (id < 1)
                return BadRequest("Id cannot be 0");

            Wagon wagon = repo.GetWagonbyId(id);
            if (wagon == null)
                return BadRequest("Wagon could not be found");

            WagonDetailViewModel vm = converter.WagonToViewModel(wagon);
            return View(vm);
        }

        // GET: Wagon/Create
        [Authorize(Roles = "Beheerder")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Wagon/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public IActionResult Create(WagonDetailViewModel vm)
        {
            if (ModelState.IsValid)
            {
                WagonViewModelConverter wagonViewModelConverter = new WagonViewModelConverter();
                Wagon w = wagonViewModelConverter.ViewModelToWagon(vm);
                decimal id = repo.CreateWagon(w);
                return RedirectToAction("Details", new { id });
            }
            return View();
        }

        // GET: Wagon/Edit/5
        [Authorize(Roles = "Beheerder")]
        public IActionResult Edit(int id)
        {
            try
            {
                WagonDetailViewModel vm = converter.WagonToViewModel(repo.GetWagonbyId(id));
                return View(vm);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Wagon/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public IActionResult Edit(WagonDetailViewModel vm)
        {
            try
            {
                Wagon wagon = converter.ViewModelToWagon(vm);
                repo.UpdateWagon(wagon);
                return RedirectToAction("Details", new { wagon.Id });
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Beheerder")]
        public IActionResult Delete(int id)
        {
            repo.DeleteWagon(id);
            return RedirectToAction(nameof(Index));
        }
    }
}