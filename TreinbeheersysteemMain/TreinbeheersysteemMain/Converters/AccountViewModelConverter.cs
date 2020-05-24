using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;
using TreinbeheersysteemMain.Authentication;
using TreinbeheersysteemMain.Models.Authentication;

namespace TreinbeheersysteemMain.Converters
{
    public class AccountViewModelConverter
    {
        public Account ViewModelToAccount(AccountDetailViewModel vm)
        {
            Account a = new Account(vm.Id, vm.Gebruikersnaam, vm.Rol, new Persoon(vm.Voornaam, vm.Achternaam, vm.Email), vm.Actief);

            return a;
        }

        public AccountDetailViewModel AccountToViewModel(Account a)
        {
            AccountDetailViewModel vm = new AccountDetailViewModel()
            {
                Id = a.Id,
                Gebruikersnaam = a.Gebruikersnaam,
                Voornaam = a.Persoon.Voornaam,
                Achternaam = a.Persoon.Achternaam,
                Email = a.Persoon.Email,
                Actief = a.Actief,
                Rol = a.Rol,
                Rollen = GetRollen()
            };

            return vm;
        }

        public IEnumerable<AccountDetailViewModel> AccountListToViewModelList(IEnumerable<Account> Accounts)
        {
            AccountViewModel AccountViewModel = new AccountViewModel
            {
                Accounts = new List<AccountDetailViewModel>()
            };
            foreach (Account Account in Accounts)
            {
                AccountViewModel.Accounts.Add(AccountToViewModel(Account));
            }

            return AccountViewModel.Accounts;
        }

        public List<Rol> GetRollen()
        {
            List<Rol> rollen = new List<Rol>
                {
                    new Rol
                    {
                        Naam = "Beheerder"
                    },
                    new Rol
                    {
                        Naam = "Treinverkeersleider"
                    },
         };
            return rollen;
        }
    }
}
