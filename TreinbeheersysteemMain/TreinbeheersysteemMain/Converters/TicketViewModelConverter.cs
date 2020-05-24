using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;
using TreinbeheersysteemMain.Models;

namespace TreinbeheersysteemMain.Converters
{
    public class TicketViewModelConverter
    {
        public Ticket ViewModelToTicket(TicketDetailViewModel vm)
        {
            Ticket t = new Ticket()
            {
                Id = vm.Id,
                Klasse = vm.Klasse,
                BeginStation = new Station() { Id = vm.BeginStationId },
                EindStation = new Station() { Id = vm.EindStationId },
                Persoon = new Persoon(vm.Voornaam, vm.Achternaam, vm.Email),
                Datum = vm.Datum,
            };

            return t;
        }

        public TicketDetailViewModel TicketToViewModel(Ticket t)
        {
            TicketDetailViewModel vm = new TicketDetailViewModel()
            {
                Id = t.Id,
                Klasse = t.Klasse,
                BeginStationId = t.BeginStation.Id,
                EindStationId = t.EindStation.Id,
                Voornaam = t.Persoon.Voornaam,
                Achternaam = t.Persoon.Achternaam,
                Email = t.Persoon.Email,
                Datum = t.Datum,
            };

            return vm;
        }
    }
}
