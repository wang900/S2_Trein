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
    public class VerbindingController : Controller
    {
        readonly IVerbindingContext context;
        readonly VerbindingRepository repo;
        readonly VerbindingViewModelConverter converter;
        public VerbindingController(IConfiguration configuration)
        {
            context = new MSSQLVerbindingContext(configuration.GetConnectionString("DefaultConnection"));
            repo = new VerbindingRepository(context);
            converter = new VerbindingViewModelConverter();
        }

        public IActionResult Index()
        {
            List<Verbinding> verbindingen = repo.GetAllVerbindingen();
            if (verbindingen.Count > 0)
                return View(converter.VerbindingListToViewModelList(verbindingen));
            else
            {
                IEnumerable<VerbindingDetailViewModel> vm = new List<VerbindingDetailViewModel>();
                return View(vm);
            }
        }
        
        public IActionResult Details(int id)
        {
            if (id < 1)
                return BadRequest("Id cannot be 0");

            Verbinding verbinding = repo.GetVerbindingbyId(id);
            if (verbinding == null)
                return BadRequest("Verbinding could not be found");

            VerbindingDetailViewModel vm = converter.VerbindingToViewModel(verbinding);
            return View(vm);
        }

        // GET: Verbinding/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Verbinding/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VerbindingDetailViewModel vm)
        {
            if (ModelState.IsValid)
            {
                VerbindingViewModelConverter verbindingViewModelConverter = new VerbindingViewModelConverter();
                Verbinding p = verbindingViewModelConverter.ViewModelToVerbinding(vm);
                decimal id = repo.CreateVerbinding(p);
                return RedirectToAction("Details", new { id });
            }
            return View();
        }

        // GET: Verbinding/Edit/5
        public IActionResult Edit(int id)
        {
            try
            {
                VerbindingDetailViewModel vm = converter.VerbindingToViewModel(repo.GetVerbindingbyId(id));
                return View(vm);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Verbinding/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(VerbindingDetailViewModel vm)
        {
            try
            {
                Verbinding verbinding = converter.ViewModelToVerbinding(vm);
                repo.UpdateVerbinding(verbinding);
                return RedirectToAction("Details", new { verbinding.Id });
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Delete(int id)
        {
            repo.DeleteVerbinding(id);
            return RedirectToAction(nameof(Index));
        }
    }
}