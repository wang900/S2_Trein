using System;
using Treinbeheersysteem.BLL.Repositories;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL.MemoryContext;
using Treinbeheersysteem.DAL.Model;
using Xunit;

namespace xUnitTestTreinbeheersysteem
{
    public class TestPerronRepository : RemoveData
    {
        readonly IPerronContext context = new MemoryPerronContext();
        PerronRepository stationVerbindingRepository;

        [Fact]
        public void DeletePerron()
        {
            EmptyLists();

            stationVerbindingRepository = new PerronRepository(context);
            Perron perron = new Perron(1, "naam", false);
            
            Assert.True(stationVerbindingRepository.DeletePerron(perron.Id));
        }

        [Fact]
        public void CreateStation()
        {
            EmptyLists();

            stationVerbindingRepository = new PerronRepository(context);
            Perron perron = new Perron(4, "naam", false);

            

            Assert.Equal(4, stationVerbindingRepository.CreatePerron(perron));
        }

        [Fact]
        public void GetAllStations()
        {
            EmptyLists();

            stationVerbindingRepository = new PerronRepository(context);

            Assert.Equal(2, stationVerbindingRepository.GetAllPerrons().Count);
        }

        [Fact]
        public void GetStationbyId()
        {
            EmptyLists();

            stationVerbindingRepository = new PerronRepository(context);
            Perron perron = new Perron(2, "naam", true);

            Assert.Equal(perron.Id, stationVerbindingRepository.GetPerronbyId(2).Id);
            Assert.Equal(perron.Naam, stationVerbindingRepository.GetPerronbyId(2).Naam);
            Assert.Equal(perron.Actief, stationVerbindingRepository.GetPerronbyId(2).Actief);
        }

        [Fact]
        public void UpdateStation()
        {
            EmptyLists();

            stationVerbindingRepository = new PerronRepository(context);
            Perron perron = new Perron(1, "xx", true);
            
            Assert.True(stationVerbindingRepository.UpdatePerron(perron));
        }
    }
}
