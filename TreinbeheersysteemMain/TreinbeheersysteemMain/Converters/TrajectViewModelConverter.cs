using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;
using TreinbeheersysteemMain.Models;

namespace TreinbeheersysteemMain.Converters
{
    public class TrajectViewModelConverter
    {
        VerbindingViewModelConverter Vconverter = new VerbindingViewModelConverter();
        public Traject ViewModelToTraject(TrajectDetailViewModel vm)
        {
            Traject t = new Traject()
            {
                Id = vm.Id,
                Verbindingen = Vconverter.ViewModelListToVerbindingList(vm.VerbindingViewModel.Verbindingen).ToList(),
                Trein = new Trein { Id = vm.Id },
                Naam = vm.Naam,
                Actief = vm.Actief
            };

            return t;
        }

        public TrajectDetailViewModel TrajectToViewModel(Traject t)
        {
            TrajectDetailViewModel vm = new TrajectDetailViewModel()
            {
                Id = t.Id,
                VerbindingViewModel = new VerbindingViewModel
                {
                    Verbindingen = Vconverter.VerbindingListToViewModelList(t.Verbindingen).ToList(),
                },
                TreinId = t.Trein.Id,
                Naam = t.Naam,
                Actief = t.Actief
            };

            return vm;
        }

        public IEnumerable<TrajectDetailViewModel> TrajectListToViewModelList(IEnumerable<Traject> trajecten)
        {
            TrajectViewModel trajectViewModel = new TrajectViewModel
            {
                Trajecten = new List<TrajectDetailViewModel>()
            };
            foreach (Traject traject in trajecten)
            {
                trajectViewModel.Trajecten.Add(TrajectToViewModel(traject));
            }

            return trajectViewModel.Trajecten;
        }
    }
}
