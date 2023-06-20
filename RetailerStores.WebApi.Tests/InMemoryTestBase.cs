using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Internal;
using Moq;
using RetailerStores.Application.Stocks;
using RetailerStores.Application.Stores;
using RetailerStores.Domain;
using RetailerStores.WebApi.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailerStores.WebApi.Tests
{
    public class InMemoryTestBase
    {
        protected static readonly DateTimeOffset DefaultDateTime = DateTimeOffset.Now;
        protected RetailerStoresDbContext DbContext { get; }
        protected ISystemClock Clock { get; }
        protected IMapper Mapper { get; set; }
        protected CancellationToken CancellationToken = new CancellationToken();

        protected InMemoryTestBase() 
        {
            var conn = new SqliteConnectionStringBuilder()
            {
                DataSource = ":memory:"
            };

            var connection = new SqliteConnection(conn.ConnectionString);
            connection.Open();

            var options = new DbContextOptionsBuilder<RetailerStoresDbContext>()
           .UseSqlite(connection)
           .EnableSensitiveDataLogging()
           .Options;

            DbContext = new RetailerStoresDbContext(options);

            var mockClock = new Mock<ISystemClock>();
            mockClock.Setup(x => x.UtcNow).Returns(DefaultDateTime);
            Clock = mockClock.Object;

            DbContext.Database.EnsureDeleted();
            DbContext.Database.EnsureCreated();

            //test fill data
            for (var i = 0; i != 5; i++)
            {
                var store = DbContext.Set<Store>().Add(CreateStore()).Entity;
                DbContext.Set<Stock>().Add(CreateStock(store.Id));
            }

            DbContext.SaveChanges();

            DetachEntries<Store>();
            DetachEntries<Manager>();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<StockMappingProfile>();
                c.AddProfile<StoreMappingProfile>();
            });
            Mapper = new Mapper(mapperConfig);
        }

        public void DetachEntries<T>() where T : BaseEntity
        {
            foreach (var dbEntityEntry in DbContext.ChangeTracker.Entries<T>())
            {
                dbEntityEntry.State = EntityState.Detached;
            }
        }

        public Store CreateStore()
        {
            return new Store()
            {
                CountryCode = "RU",
                Name = "Test",
                StoreEmail = "test@test.test",
                Manager = CreateManager()
            };
        }

        public Manager CreateManager()
        {
            return new Manager()
            {
                FirstName = "Test",
                LastName = "Testov",
                Email = "testov_test@test.test",
                Category = 5
            };
        }

        public Stock CreateStock(Guid storeId) 
        {
            return new Stock()
            {
                Accuracy = 100,
                Backstock = 25,
                Frontstock = 25,
                MeanAge = 5,
                OnFloorAvailability = 5,
                RecordingDate = Clock.UtcNow,
                ShoppingWindow = 1,
                StoreId = storeId
            };
        }
    }
}
