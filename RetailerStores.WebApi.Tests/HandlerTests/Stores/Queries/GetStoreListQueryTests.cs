using RetailerStores.Application.Stores.Queries;

namespace RetailerStores.WebApi.Tests.Stores.Queries
{
    public class GetStoreListQueryTests : InMemoryTestBase
    {
        private GetStoreListQuery.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _handler = new GetStoreListQuery.Handler(DbContext, Mapper);
        }

        [Test]
        public void IsWorking_Test()
        {
            var query = new GetStoreListQuery();
            Assert.DoesNotThrowAsync(async () => await _handler.Handle(query, CancellationToken));
        }

        [Test]
        public async Task IsResultNotNullOrEmpty_Test()
        {
            var query = new GetStoreListQuery();
            var result = await _handler.Handle(query, CancellationToken);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
        }
    }
}