using System.Collections.Generic;
using Treinbeheersysteem.DAL.Model;
using TreinbeheersysteemMain.Models;

namespace TreinbeheersysteemMain.Converters
{
    public class VerbindingViewModelConverter
    {
        public Verbinding ViewModelToVerbinding(VerbindingDetailViewModel vm)
        {
            Verbinding v = new Verbinding()
            {
                Id = vm.Id,
                BeginPerron = new Perron() { Id = vm.BeginPerronId},
                EindPerron = new Perron() { Id = vm.EindPerronId},
                Naam = vm.Naam,
                Lengte = vm.Lengte,
                Actief = vm.Actief
            };

            return v;
        }

        public VerbindingDetailViewModel VerbindingToViewModel(Verbinding v)
        {
            VerbindingDetailViewModel vm = new VerbindingDetailViewModel()
            {
                Id = v.Id,
                BeginPerronId = v.BeginPerron.Id,
                EindPerronId = v.EindPerron.Id,
                Naam = v.Naam,
                Lengte = v.Lengte,
                Actief = v.Actief
            };

            return vm;
        }

        public IEnumerable<VerbindingDetailViewModel> VerbindingListToViewModelList(IEnumerable<Verbinding> verbindingen)
        {
            VerbindingViewModel verbindingViewModel = new VerbindingViewModel
            {
                Verbindingen = new List<VerbindingDetailViewModel>()
            };
            foreach (Verbinding verbinding in verbindingen)
            {
                verbindingViewModel.Verbindingen.Add(VerbindingToViewModel(verbinding));
            }

            return verbindingViewModel.Verbindingen;
        }

        public IEnumerable<Verbinding> ViewModelListToVerbindingList(IEnumerable<VerbindingDetailViewModel> verbindingen)
        {
            List<Verbinding> Verbindingen = new List<Verbinding>();
            foreach (VerbindingDetailViewModel verbinding in verbindingen)
            {
                Verbindingen.Add(ViewModelToVerbinding(verbinding));
            }

            return Verbindingen;
        }
    }
}
