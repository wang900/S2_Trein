using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Umbrella.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;
using System.ComponentModel.DataAnnotations;

namespace TreinbeheersysteemMain.Models
{
    public class VerbindingDetailViewModel
    {
        public long Id { get; set; }
        public string Naam { get; set; }
        [Required]
        public long BeginPerronId { get; set; }
        [Required]
        [NotEqualTo("BeginPerron", ErrorMessage = "De begin- en eindperron mag niet hetzelfde zijn.")]
        public long EindPerronId { get; set; }
        [Required]
        [Range(50, 1000, ErrorMessage = "De waarde mag niet kleiner zijn dan {0} of groter zijn dan {1}")]
        public int Lengte { get; set; }
        [Required]
        public bool Actief { get; set; }
        public PerronViewModel PerronViewModel { get; set; }
        [NotMapped]
        public DateTime AankomstTijd { get; set; }
        [NotMapped]
        public DateTime VertrekTijd { get; set; }
    }
}
