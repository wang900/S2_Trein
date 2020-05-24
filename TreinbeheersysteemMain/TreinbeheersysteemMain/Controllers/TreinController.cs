using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    public class TreinController : Controller
    {
        readonly ITreinContext context;
        readonly TreinRepository repo;
        TreinViewModelConverter converter;

        public TreinController(IConfiguration configuration)
        {
            context = new MSSQLTreinContext(configuration.GetConnectionString("DefaultConnection"));
            repo = new TreinRepository(context);
            converter = new TreinViewModelConverter();
        }

        public IActionResult Index()
        {
            List<Trein> treinen = repo.GetAllTreinen();
            TreinViewModel vm = new TreinViewModel
            {
                Treinen = new List<TreinDetailViewModel>()
            };
            foreach (Trein trein in treinen)
            {
                vm.Treinen.Add(converter.TreinToViewModel(trein));
            }

            return View(vm.Treinen);
        }

        public IActionResult Details(int id)
        {
            if (id < 1)
                return BadRequest("Id cannot be 0");

            Trein trein = repo.GetTreinbyId(id);
            if (trein == null)
                return BadRequest("Trein could not be found");

            TreinDetailViewModel vm = converter.TreinToViewModel(trein);
            return View(vm);
        }

        [Authorize(Roles = "Beheerder")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public IActionResult Create(TreinDetailViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Trein trein = converter.ViewModelToTrein(vm);
                decimal id = repo.CreateTrein(trein);
                return RedirectToAction("Details", new { id });
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            try
            {
                TreinDetailViewModel vm = converter.TreinToViewModel(repo.GetTreinbyId(id));
                return View(vm);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public IActionResult Edit(TreinDetailViewModel vm)
        {
            try
            {
                Trein trein = converter.ViewModelToTrein(vm);
                bool succes = repo.UpdateTrein(trein);
                if (succes)
                {
                    return RedirectToAction("Details", new { trein.Id });
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
            repo.DeleteTrein(id);
            return View();
        }
    }
}