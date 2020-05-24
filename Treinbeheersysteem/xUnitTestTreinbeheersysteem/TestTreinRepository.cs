using System;
using Treinbeheersysteem.BLL.Repositories;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL.MemoryContext;
using Treinbeheersysteem.DAL.Model;
using Xunit;

namespace xUnitTestTreinbeheersysteem
{
    public class TestTreinRepository : RemoveData
    {
        readonly ITreinContext context = new MemoryTreinContext();
        TreinRepository treinRepository;

        [Fact]
        public void CreateTrein()
        {
            EmptyLists();

            treinRepository = new TreinRepository(context);
            Trein trein = new Trein(4, "naam", 23, new Positie(4, 42, 21));

            Assert.Equal(4, treinRepository.CreateTrein(trein));
        }

        [Fact]
        public void DeleteTrein()
        {
            EmptyLists();

            treinRepository = new TreinRepository(context);
            Trein trein = new Trein(1, "naam", 23, new Positie(4, 42, 21));

            Assert.True(treinRepository.DeleteTrein(trein.Id));
        }

        [Fact]
        public void GetAllTreinen()
        {
            EmptyLists();

            treinRepository = new TreinRepository(context);

            Assert.Equal(3, treinRepository.GetAllTreinen().Count);
        }

        [Fact]
        public void GetTreinbyId()
        {
            EmptyLists();

            treinRepository = new TreinRepository(context);
            Trein trein = new Trein(1, "naam", 23, new Positie(4, 42, 21));

            Assert.Equal(trein.Id, treinRepository.GetTreinbyId(1).Id);
            Assert.Equal("naam" ,trein.Naam);
        }

        [Fact]
        public void UpdateTrein()
        {
            EmptyLists();

            treinRepository = new TreinRepository(context);
            Trein trein = new Trein(1, "naam", 23, new Positie(4, 42, 21));

            Assert.True(treinRepository.UpdateTrein(trein));
        }
    }
}
