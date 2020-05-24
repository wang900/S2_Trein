using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Treinbeheersysteem.BLL.Repositories;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL.Model;
using TreinbeheersysteemMain.Authentication;
using TreinbeheersysteemMain.Converters;
using TreinbeheersysteemMain.Models.Authentication;

namespace TreinbeheersysteemMain.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        readonly AccountViewModelConverter converter;
        readonly ProfielViewModelConverter Pconverter;

        public AccountController(
            UserManager<Account> userManager,
            SignInManager<Account> signInManager
          )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            converter = new AccountViewModelConverter();
            Pconverter = new ProfielViewModelConverter();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        //[ValidateAntiForgeryToken]
        public IActionResult Index()
        {
            IEnumerable<AccountDetailViewModel> accounts = converter.AccountListToViewModelList(_userManager.GetUsersInRoleAsync("").Result);
            return View(accounts);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(string returnUrl = null)
        {
            RegisterViewModel vm = new RegisterViewModel
            {
                Rollen = converter.GetRollen(),
            };
            return View(vm);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {

                var result = await _signInManager.PasswordSignInAsync(model.Gebruikersnaam, model.Wachtwoord, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                        if (returnUrl == null)
                            returnUrl = "Home";
                        return RedirectToAction("Index", returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty,"Invalid login attempt.");
                    return View(model);
                }
            }

            return View(model);
        }
        

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                Account account = new Account(-1, model.Gebruikersnaam, model.Rol, new Persoon(model.Voornaam, model.Achternaam, model.Email), model.Actief);
                var result = await _userManager.CreateAsync(account, "Test123!");
                if (result.Succeeded)
                {
                    returnUrl = "Index";
                    return RedirectToAction(returnUrl);
                }
                ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
            }
            else
            {
                model.Rollen = converter.GetRollen();
            }
            return View(model);
        }

        [Authorize]
        public IActionResult Profiel(long id)
        {
            Account account = _userManager.FindByIdAsync(id.ToString()).Result;
            ProfielViewModel vm = Pconverter.AccountToViewModel(account);
            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Profiel(ProfielViewModel vm)
        {
            Account account = Pconverter.ViewModelToAccount(vm);
            var result = await _userManager.UpdateAsync(account);
            if (ModelState.IsValid)
            {
                
                
                return View();
            }
            else
            {
                return View(vm);
            }
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            if (HttpContext.User?.Identity.IsAuthenticated == true)
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        // GET: Account/Edit/5
        public IActionResult Edit(int id)
        {
            try
            {
                AccountDetailViewModel vm = converter.AccountToViewModel(_userManager.FindByIdAsync(id.ToString()).Result);
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
        public async Task<IActionResult> Edit(AccountDetailViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Account account = converter.ViewModelToAccount(vm);
                    var result = await _userManager.UpdateAsync(account);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Details", new { account.Id });
                    }
                }
                catch
                {
                    throw;
                }
            }
            vm.Rollen = converter.GetRollen();
            return View(vm);
        }


        public IActionResult Details(int id)
        {
            if (id < 1)
                return BadRequest("Id cannot be 0");

            Account account = _userManager.FindByIdAsync(id.ToString()).Result;
            if (account == null)
                return BadRequest("Account could not be found");

            AccountDetailViewModel vm = converter.AccountToViewModel(account);
            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id >= 1)
            {
                Account account = default(Account);
                account.Id = id;
                await _userManager.DeleteAsync(account);
            }
                return RedirectToAction(nameof(Index));
        }

        //AccessDenied
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}