using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TreinbeheersysteemMain.Models.Authentication
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "De gebruikersnaam kan niet leeg zijn.")]
        public string Gebruikersnaam { get; set; }

        [Required(ErrorMessage = "De wachtwoord kan niet leeg zijn.")]
        [DataType(DataType.Password)]
        public string Wachtwoord { get; set; }

        [Display(Name = "Onthoud mijn login")]
        public bool RememberMe { get; set; }

    }
}
