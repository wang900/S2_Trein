using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;

namespace TreinbeheersysteemMain.Models
{
    public class StationDetailViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "De naam is een verplicht veld")]
        [StringLength(100, ErrorMessage = "De {0} moet tenminste {2} en maximaal {1} karakters lang zijn.", MinimumLength = 1)]
        public string Naam { get; set; }
        public bool Actief { get; set; }
        [NotMapped]
        public long PositieId { get; set; }
    }
}
