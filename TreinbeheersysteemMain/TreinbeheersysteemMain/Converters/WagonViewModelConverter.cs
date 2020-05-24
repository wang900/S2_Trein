using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;
using TreinbeheersysteemMain.Models;

namespace TreinbeheersysteemMain.Converters
{
    public class WagonViewModelConverter
    {
        public Wagon ViewModelToWagon(WagonDetailViewModel vm)
        {
            Wagon w = new Wagon()
            {
                Id = vm.Id,
                Naam = vm.Naam,
                Stoelaantal_Klasse1 = vm.Stoelaantal_Klasse1,
                Stoelaantal_Klasse2 = vm.Stoelaantal_Klasse2,
                TreinId = vm.TreinId,
                Actief = vm.Actief
            };

            return w;
        }

        public WagonDetailViewModel WagonToViewModel(Wagon w)
        {
            WagonDetailViewModel vm = new WagonDetailViewModel()
            {
                Id = w.Id,
                Naam = w.Naam,
                Stoelaantal_Klasse1 = w.Stoelaantal_Klasse1,
                Stoelaantal_Klasse2 = w.Stoelaantal_Klasse2,
                TreinId = w.TreinId,
                Actief = w.Actief
            };

            return vm;
        }
    }
}
