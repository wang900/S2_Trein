using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TreinbeheersysteemMain.Models
{
    public class TrajectDetailViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "De naam is een verplicht veld.")]
        [StringLength(100, ErrorMessage = "De {0} moet tenminste {2} en maximaal {1} karakters lang zijn.", MinimumLength = 1)]
        public string Naam { get; set; }
        public DateTime BeginTijd { get { BeginTijd = VerbindingViewModel.Verbindingen[0].VertrekTijd; return BeginTijd; } private set { } }
        public bool Actief { get; set; }
        public VerbindingViewModel VerbindingViewModel { get; set; }
        [Required]
        public long TreinId { get; set; }
    }
}
