using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;

namespace TreinbeheersysteemMain.Models
{
    public class WagonDetailViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "De naam is een verplicht veld.")]
        public string Naam { get; set; }
        [Display(Name = "Stoelen 1e klasse")]
        [Required(ErrorMessage = "De aantal stoelen voor de 1e klasse is een verplicht veld.")]
        public int Stoelaantal_Klasse1 { get; set; }
        [Display(Name = "Stoelen 2e klasse")]
        [Required(ErrorMessage = "De aantal stoelen voor de 2e klasse is een verplicht veld.")]
        public int Stoelaantal_Klasse2 { get; set; }
        [Display(Name = "Trein")]
        [Required(ErrorMessage = "De trein is een verplicht veld.")]
        public long TreinId { get; set; }
        public bool Actief { get; set; }
    }
}
