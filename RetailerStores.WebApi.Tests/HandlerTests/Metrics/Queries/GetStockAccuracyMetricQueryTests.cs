using AutoMapper;
using RetailerStores.Application.Metrics.Queries;
using RetailerStores.Application.Stocks;
using RetailerStores.Application.Stores;

namespace RetailerStores.WebApi.Tests.Metrics.Queries
{
    public class GetStockAccuracyMetricQueryTests : InMemoryTestBase
    {
        private GetStockAccuracyMetricQuery.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _handler = new GetStockAccuracyMetricQuery.Handler(DbContext);
        }

        [Test]
        public void HandlerIsWorking_Test()
        {
            var query = new GetStockAccuracyMetricQuery();
            Assert.DoesNotThrowAsync(async () => await _handler.Handle(query, CancellationToken));
        }

        [Test]
        public async Task HandlerGetCorrectCountValues_Test()
        {
            var query = new GetStockAccuracyMetricQuery();

            var result = await _handler.Handle(query, CancellationToken);

            Assert.That(result.Values, Is.Not.Null);

            Assert.That(result.Values.Length, Is.EqualTo(3));
        }
    }
}