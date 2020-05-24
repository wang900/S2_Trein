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
    public class TrajectController : Controller
    {
        readonly ITrajectContext context;
        readonly TrajectRepository repo;
        readonly TrajectViewModelConverter converter;
        public TrajectController(IConfiguration configuration)
        {
            context = new MSSQLTrajectContext(configuration.GetConnectionString("DefaultConnection"));
            repo = new TrajectRepository(context);
            converter = new TrajectViewModelConverter();
        }

        public IActionResult Index()
        {
            List<Traject> trajecten = repo.GetAllTrajecten();
            TrajectViewModel vm = new TrajectViewModel
            {
                Trajecten = new List<TrajectDetailViewModel>()
            };
            foreach (Traject traject in trajecten)
            {
                vm.Trajecten.Add(converter.TrajectToViewModel(traject));
            }
            
            return View(vm.Trajecten);
        }
        
        public IActionResult Details(int id)
        {
            if (id < 1)
                return BadRequest("Id cannot be 0");

            Traject traject = repo.GetTrajectbyId(id);
            if (traject == null)
                return BadRequest("Traject could not be found");

            TrajectDetailViewModel vm = converter.TrajectToViewModel(traject);
            return View(vm);
        }

        // GET: Traject/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Traject/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TrajectDetailViewModel vm)
        {
            if (ModelState.IsValid)
            {
                TrajectViewModelConverter trajectViewModelConverter = new TrajectViewModelConverter();
                Traject t = trajectViewModelConverter.ViewModelToTraject(vm);
                decimal id = repo.CreateTraject(t);
                return RedirectToAction("Details", new { id });
            }
            return View();
        }

        // GET: Traject/Edit/5
        public IActionResult Edit(int id)
        {
            try
            {
                TrajectDetailViewModel vm = converter.TrajectToViewModel(repo.GetTrajectbyId(id));
                return View(vm);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Traject/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TrajectDetailViewModel vm)
        {
            try
            {
                Traject traject = converter.ViewModelToTraject(vm);
                repo.UpdateTraject(traject);
                return RedirectToAction("Details", new { traject.Id });
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Delete(int id)
        {
            repo.DeleteTraject(id);
            return RedirectToAction(nameof(Index));
        }
    }
}