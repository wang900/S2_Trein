using System;
using Treinbeheersysteem.BLL.Repositories;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL.MemoryContext;
using Treinbeheersysteem.DAL.Model;
using Xunit;

namespace xUnitTestTreinbeheersysteem
{
    public class TestStationRepository : RemoveData
    {
        readonly IStationContext context = new MemoryStationContext();
        StationRepository stationRepository;

        [Fact]
        public void DeleteStation()
        {
            EmptyLists();

            stationRepository = new StationRepository(context);
            Station station = new Station(1, "naam", new Positie(1, 23, 1), false);
            
            Assert.True(stationRepository.DeleteStation(station.Id));
        }

        [Fact]
        public void CreateStation()
        {
            EmptyLists();

            stationRepository = new StationRepository(context);
            Station station = new Station(1, "naam", new Positie(1, 23, 1), true);
            
            Assert.Equal(1, stationRepository.CreateStation(station));
        }

        [Fact]
        public void GetAllStations()
        {
            EmptyLists();

            stationRepository = new StationRepository(context);

            Assert.Equal(2, stationRepository.GetAllStations().Count);
        }

        [Fact]
        public void GetStationbyId()
        {
            EmptyLists();

            stationRepository = new StationRepository(context);
            Station station = new Station(2, "naam", new Positie(1, 23, 32), true);

            Assert.Equal(station.Id, stationRepository.GetStationbyId(2).Id);
            Assert.Equal(station.Naam, stationRepository.GetStationbyId(2).Naam);
            Assert.Equal(station.Actief, stationRepository.GetStationbyId(2).Actief);
        }

        [Fact]
        public void UpdateStation()
        {
            EmptyLists();

            stationRepository = new StationRepository(context);
            Station station = new Station(1, "ds", new Positie(1, 24, 32), true);
            
            Assert.True(stationRepository.UpdateStation(station));
        }
    }
}
