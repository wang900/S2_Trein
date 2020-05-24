using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;
using TreinbeheersysteemMain.Models;

namespace TreinbeheersysteemMain.Converters
{
    public class TreinViewModelConverter
    {
        public Trein ViewModelToTrein(TreinDetailViewModel vm)
        {
            Trein t = new Trein()
            {
                Id = vm.Id,
                MaxSnelheid = vm.MaxSnelheid,
                Naam = vm.Naam,
                Actief = vm.Actief
            };

            return t;
        }

        public TreinDetailViewModel TreinToViewModel(Trein t)
        {
            TreinDetailViewModel vm = new TreinDetailViewModel()
            {
                Id = t.Id,
                MaxSnelheid = t.MaxSnelheid,
                Naam = t.Naam,
                Actief = t.Actief
            };
            return vm;
        }
    }
}
