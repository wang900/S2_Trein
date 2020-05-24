using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Umbrella.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;

namespace TreinbeheersysteemMain.Models
{
    public class TicketDetailViewModel
    {
        public long Id { get; set; }
        public Klasse Klasse { get; set; }
        [Required(ErrorMessage = "Je hebt een vertrekplaats nodig")]
        public long BeginStationId { get; set; }
        [Required(ErrorMessage = "Je hebt een eindbestemming nodig")]
        [NotEqualTo("BeginStationId", ErrorMessage = "Het vertrekplaats en eindbestemming mogen niet hetzelfde zijn.")]
        public long EindStationId { get; set; }
        [StringLength(100, ErrorMessage = "De {0} moet tenminste {2} en maximaal {1} karakters lang zijn.", MinimumLength = 1)]
        public string Voornaam { get; set; }
        [StringLength(100, ErrorMessage = "De {0} moet tenminste {2} en maximaal {1} karakters lang zijn.", MinimumLength = 1)]
        public string Achternaam { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public DateTime Datum { get; set; }
        public double Prijs { get; set; }
        public StationViewModel StationViewModel { get; set; }
        public TrajectViewModel TrajectViewModel { get; set; }
    }
}
