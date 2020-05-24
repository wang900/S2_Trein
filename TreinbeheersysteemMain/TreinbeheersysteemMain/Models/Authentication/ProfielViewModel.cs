using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;

namespace TreinbeheersysteemMain.Models.Authentication
{
    public class ProfielViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "De gebruikersnaam is een verplicht veld.")]
        [StringLength(100, ErrorMessage = "De {0} moet tenminste {2} en maximaal {1} karakters lang zijn.", MinimumLength = 1)]
        public string Gebruikersnaam { get; set; }

        [Required(ErrorMessage = "De voornaam is een verplicht veld.")]
        [StringLength(100, ErrorMessage = "De {0} moet tenminste {2} en maximaal {1} karakters lang zijn.", MinimumLength = 1)]
        public string Voornaam { get; set; }

        [Required(ErrorMessage = "De achternaam is een verplicht veld")]
        [StringLength(100, ErrorMessage = "De {0} moet tenminste {2} en maximaal {1} karakters lang zijn.", MinimumLength = 1)]
        public string Achternaam { get; set; }
        
        public Rol Rol { get; set; }

        [Required(ErrorMessage = "De email is een verplichte veld.")]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Je moet je wachtwoord verifiëren.")]
        [DataType(DataType.Password)]
        public string Wachtwoord { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Nieuw wachtwoord")]
        public string NieuwWachtwoord { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Verifiëer nieuw wachtwoord")]
        [Compare("NieuwWachtwoord", ErrorMessage = "De wachtwoorden zijn niet hetzelfde.")]
        public string ConfirmNieuwWachtwoord { get; set; }

        public List<Rol> Rollen { get; set; }

    }
}
