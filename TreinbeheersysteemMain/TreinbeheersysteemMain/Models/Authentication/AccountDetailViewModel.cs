using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;

namespace TreinbeheersysteemMain.Models.Authentication
{
    public class AccountDetailViewModel
    {
        public long Id { get; set; }
        public string Gebruikersnaam { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Email { get; set; }
        public bool Actief { get; set; }
        public Rol Rol { get; set; }
        public List<Rol> Rollen { get; set; }
    }
}
