using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;
using TreinbeheersysteemMain.Models.Authentication;

namespace TreinbeheersysteemMain.Converters
{
    public class ProfielViewModelConverter
    {
        public Account ViewModelToAccount(ProfielViewModel vm)
        {
            Account a = new Account(vm.Id, vm.Gebruikersnaam, vm.Rol, new Persoon(vm.Voornaam, vm.Achternaam, vm.Email), true);
            if (vm.NieuwWachtwoord != null)
            {
                a.SetPassword(vm.NieuwWachtwoord);
            }
            return a;
        }

        public ProfielViewModel AccountToViewModel(Account a)
        {
            ProfielViewModel vm = new ProfielViewModel()
            {
                Id = a.Id,
                Gebruikersnaam = a.Gebruikersnaam,
                Voornaam = a.Persoon.Voornaam,
                Achternaam = a.Persoon.Achternaam,
                Email = a.Persoon.Email,
                Rol = a.Rol,
                Wachtwoord = a.Wachtwoord,
            };

            return vm;
        }
    }
}
