using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Umbrella.DataAnnotations;

namespace TreinbeheersysteemMain.Models
{
    public class PerronDetailViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "De naam is een verplicht veld")]
        [StringLength(100, ErrorMessage = "De {0} moet tenminste {2} en maximaal {1} karakters lang zijn.", MinimumLength = 1)]
        public string Naam { get; set; }
        [Display(Name = "Station")]
        [Required(ErrorMessage = "Het station is een verplicht veld")]
        public long StationId { get; set; }
        [Required]
        public bool Actief { get; set; }

        public StationViewModel StationViewModel { get; set; }
    }
}
