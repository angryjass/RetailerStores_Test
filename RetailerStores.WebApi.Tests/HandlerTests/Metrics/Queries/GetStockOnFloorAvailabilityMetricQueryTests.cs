using RetailerStores.Application.Metrics.Queries;

namespace RetailerStores.WebApi.Tests.Metrics.Queries
{
    public class GetStockOnFloorAvailabilityMetricQueryTests : InMemoryTestBase
    {
        private GetStockOnFloorAvailabilityMetricQuery.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _handler = new GetStockOnFloorAvailabilityMetricQuery.Handler(DbContext);
        }

        [Test]
        public void HandlerIsWorking_Test()
        {
            var query = new GetStockOnFloorAvailabilityMetricQuery();
            Assert.DoesNotThrowAsync(async () => await _handler.Handle(query, CancellationToken));
        }

        [Test]
        public async Task HandlerGetCorrectCountValues_Test()
        {
            var query = new GetStockOnFloorAvailabilityMetricQuery();

            var result = await _handler.Handle(query, CancellationToken);

            Assert.That(result.Values, Is.Not.Null);

            Assert.That(result.Values.Length, Is.EqualTo(3));
        }
    }
}