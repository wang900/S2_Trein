using System;
using Treinbeheersysteem.BLL.Repositories;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.DAL.MemoryContext;
using Treinbeheersysteem.DAL.Model;
using Xunit;

namespace xUnitTestTreinbeheersysteem
{
    public class TestWagonRepository : RemoveData
    {
        readonly IWagonContext context = new MemoryWagonContext();
        WagonRepository wagonRepository;

        [Fact]
        public void CreateWagon()
        {
            EmptyLists();

            wagonRepository = new WagonRepository(context);
            Wagon wagon = new Wagon(4, "naam", 23, 45);

            Assert.Equal(4, wagonRepository.CreateWagon(wagon));
        }

        [Fact]
        public void DeleteWagon()
        {
            EmptyLists();

            wagonRepository = new WagonRepository(context);
            Wagon wagon = new Wagon(1, "naam", 13, 25);

            Assert.True(wagonRepository.DeleteWagon(wagon.Id));
        }

        [Fact]
        public void GetAllWagons()
        {
            EmptyLists();

            wagonRepository = new WagonRepository(context);

            Assert.Equal(3, wagonRepository.GetAllWagons().Count);
        }

        [Fact]
        public void GetWagonbyId()
        {
            EmptyLists();

            wagonRepository = new WagonRepository(context);
            Wagon wagon = new Wagon(1, "naam", 13, 25);

            Assert.Equal(wagon.Id, wagonRepository.GetWagonbyId(1).Id);
            Assert.Equal(13, wagon.Stoelaantal_Klasse1);
        }

        [Fact]
        public void UpdateWagon()
        {
            EmptyLists();

            wagonRepository = new WagonRepository(context);
            Wagon wagon = new Wagon(1, "naam", 13, 25);

            Assert.True(wagonRepository.UpdateWagon(wagon));
        }
    }
}
