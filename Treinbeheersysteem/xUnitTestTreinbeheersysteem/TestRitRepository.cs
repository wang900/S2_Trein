using System;
using Treinbeheersysteem.BLL.Repositories;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL.MemoryContext;
using Treinbeheersysteem.DAL.Model;
using Xunit;

namespace xUnitTestTreinbeheersysteem
{
    public class TestTrajectRepository : RemoveData
    {
        readonly ITrajectContext context = new MemoryTrajectContext();
        TrajectRepository treinreisRepository;

        [Fact]
        public void CreateTraject()
        {
            EmptyLists();

            treinreisRepository = new TrajectRepository(context);
            Traject treinreis = new Traject(5, "naam", DateTime.Today, new Trein(5, "naam", 231, new Positie(1, 32, 42)));

            Assert.Equal(5, treinreisRepository.CreateTraject(treinreis));
        }

        [Fact]
        public void DeleteTraject()
        {
            EmptyLists();

            treinreisRepository = new TrajectRepository(context);
            Traject treinreis = new Traject(1, "naam", DateTime.Today, new Trein(1, "naam", 231, new Positie(1, 32, 42)));

            Assert.True(treinreisRepository.DeleteTraject(treinreis.Id));
        }

        [Fact]
        public void GetAllTrajecten()
        {
            EmptyLists();

            treinreisRepository = new TrajectRepository(context);

            Assert.Equal(3, treinreisRepository.GetAllTrajecten().Count);
        }

        [Fact]
        public void GetTrajectbyId()
        {
            EmptyLists();

            treinreisRepository = new TrajectRepository(context);
            Traject treinreis = new Traject(1, "naam", DateTime.Today, new Trein(1, "naam", 231, new Positie(1, 32, 42)));

            Assert.Equal(treinreis.Id, treinreisRepository.GetTrajectbyId(1).Id);
            Assert.Equal("naam", treinreis.Naam);
        }

        [Fact]
        public void UpdateTraject()
        {
            EmptyLists();

            treinreisRepository = new TrajectRepository(context);
            Traject treinreis = new Traject(1, "naam", DateTime.Today, new Trein(1, "naam", 231, new Positie(1, 32, 42)));

            Assert.True(treinreisRepository.UpdateTraject(treinreis));
        }
    }
}
