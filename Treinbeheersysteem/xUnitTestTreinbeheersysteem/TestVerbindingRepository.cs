using System;
using Treinbeheersysteem.BLL.Repositories;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL.Model;
using Treinbeheersysteem.DAL.MemoryContext;
using Xunit;

namespace xUnitTestTreinbeheersysteem
{
    public class TestVerbindingRepository : RemoveData
    {
        readonly IVerbindingContext context = new MemoryVerbindingContext();
        VerbindingRepository verbindingRepository;

        [Fact]
        public void GetAllVerbindingen()
        {
            EmptyLists();

            verbindingRepository = new VerbindingRepository(context);
            Assert.Equal(3, verbindingRepository.GetAllVerbindingen().Count);
        }

        [Fact]
        public void GetVerbindingbyId()
        {
            EmptyLists();

            verbindingRepository = new VerbindingRepository(context);
            Verbinding verbinding = new Verbinding(1, "naam", 50, new Perron(1, "naam", true), new Perron(2, "naam", true), true);
            Verbinding verbinding2 = verbindingRepository.GetVerbindingbyId(1);

            Assert.Equal(verbinding.Id, verbinding2.Id);
            Assert.Equal(verbinding.Naam, verbinding2.Naam);
            Assert.Equal(verbinding.Lengte, verbinding2.Lengte);
        }

        [Fact]
        public void CreateVerbinding()
        {
            EmptyLists();

            verbindingRepository = new VerbindingRepository(context);
            Verbinding verbinding = new Verbinding(3, "naam", 50, new Perron(1, "naam", true), new Perron(2, "naam", true), true);

            verbindingRepository.CreateVerbinding(verbinding);
            
            Assert.Equal(3, verbindingRepository.CreateVerbinding(verbinding));
        }

        [Fact]
        public void UpdateVerbinding()
        {
            EmptyLists();

            verbindingRepository = new VerbindingRepository(context);
            Verbinding verbinding = new Verbinding(1, "Andernaam", 50, new Perron(1, "naam", true), new Perron(2, "naam", true), true);
            

            Assert.True(verbindingRepository.UpdateVerbinding(verbinding));
        }

        [Fact]
        public void DeleteVerbinding()
        {
            EmptyLists();

            verbindingRepository = new VerbindingRepository(context);
            Verbinding verbinding = new Verbinding(1, "Andernaam", 50, new Perron(1, "naam", true), new Perron(2, "naam", true), true);
            
            Assert.True(verbindingRepository.DeleteVerbinding(verbinding.Id));
        }
    }
}
