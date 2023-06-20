using RetailerStores.Application.Stores.Queries;
using RetailerStores.Domain;

namespace RetailerStores.WebApi.Tests.Stores.Queries
{
    public class GetStoreQueryTests : InMemoryTestBase
    {
        private GetStoreQuery.Handler _handler;
        [SetUp]
        public void Setup()
        {
            _handler = new GetStoreQuery.Handler(DbContext, Mapper, new GetStoreQuery.Validator(DbContext));
        }

        [Test]
        public void IsWorking_Test()
        {
            var query = new GetStoreQuery() { Id = DbContext.Set<Store>().First().Id };
            Assert.DoesNotThrowAsync(async () => await _handler.Handle(query, CancellationToken));
        }

        [Test]
        public async Task IsQueryReturnCurrentStore_Test()
        {
            var store = DbContext.Set<Store>().First();
            var query = new GetStoreQuery() { Id = store.Id };

            var queriesStore = await _handler.Handle(query, CancellationToken);

            Assert.That(queriesStore.Id, Is.EqualTo(query.Id));
        }

        [Test]
        public void IsValidationWorks_Test()
        {
            var query = new GetStoreQuery() { Id = Guid.NewGuid() };

            Assert.ThrowsAsync<FluentValidation.ValidationException>(async () => await _handler.Handle(query, CancellationToken));
        }
    }
}