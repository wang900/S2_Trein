using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;

namespace TreinbeheersysteemMain.Models
{
    public class TreinDetailViewModel
    {
        public long Id { get; set; }
        [Required( ErrorMessage = "De naam is een verplicht veld.")]
        public string Naam { get; set; }
        [Required(ErrorMessage = "De max snelheid is een verplicht veld.")]
        public int MaxSnelheid { get; set; }
        [NotMapped]
        public long PositieId { get; set; }
        public bool Actief { get; set; }
    }
}
