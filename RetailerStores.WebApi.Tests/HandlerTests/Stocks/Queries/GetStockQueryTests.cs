using Microsoft.EntityFrameworkCore;
using RetailerStores.Application.Stocks.Queries;
using RetailerStores.Domain;

namespace RetailerStores.WebApi.Tests.Stocks.Queries
{
    public class GetStockQueryTests : InMemoryTestBase
    {
        private GetStockQuery.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _handler = new GetStockQuery.Handler(DbContext, Mapper, new GetStockQuery.Validator(DbContext));
        }

        [Test]
        public void IsWorking_Test()
        {
            var query = new GetStockQuery() { StoreId = DbContext.Set<Store>().First().Id };
            Assert.DoesNotThrowAsync(async () => await _handler.Handle(query, CancellationToken));
        }

        [Test]
        public async Task IsStockFromCurrentStore_Test()
        {
            var store = DbContext.Set<Store>().Include(a => a.Stocks).Where(a => a.Stocks.Count > 0).First();
            var query = new GetStockQuery() { StoreId = store.Id };

            var stock = await _handler.Handle(query, CancellationToken);

            Assert.That(stock!.StoreId, Is.EqualTo(store.Id));
        }

        public void IsValidationWorks_Test()
        {
            var query = new GetStockQuery() { StoreId = Guid.NewGuid() };

            Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(query, CancellationToken));
        }
    }
}