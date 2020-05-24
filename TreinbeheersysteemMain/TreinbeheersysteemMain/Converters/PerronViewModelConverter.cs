using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Treinbeheersysteem.DAL.Model;
using TreinbeheersysteemMain.Models;

namespace TreinbeheersysteemMain.Converters
{
    public class PerronViewModelConverter
    {

        public PerronDetailViewModel PerronToViewModel(Perron p)
        {
            PerronDetailViewModel vm = new PerronDetailViewModel()
            {
                Id = p.Id,
                StationId = p.StationId,
                Naam = p.Naam,
                Actief = p.Actief
            };

            return vm;
        }
    }
}
