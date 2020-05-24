using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;
using TreinbeheersysteemMain.Models;

namespace TreinbeheersysteemMain.Converters
{
    public class StationViewModelConverter
    {
        public Station ViewModelToStation(StationDetailViewModel vm)
        {
            Station s = new Station()
            {
                Id = vm.Id,
                Naam = vm.Naam,
                Actief = vm.Actief
            };

            return s;
        }

        public StationDetailViewModel StationToViewModel(Station s)
        {
            StationDetailViewModel vm = new StationDetailViewModel()
            {
                Id = s.Id,
                Naam = s.Naam,
                Actief = s.Actief
            };

            return vm;
        }

        public IEnumerable<StationDetailViewModel> StationListToViewModelList(IEnumerable<Station> stations)
        {
            StationViewModel stationViewModel = new StationViewModel
            {
                Stations = new List<StationDetailViewModel>()
            };
            foreach (Station station in stations)
            {
                stationViewModel.Stations.Add(StationToViewModel(station));
            }

            return stationViewModel.Stations;
        }
    }
}
