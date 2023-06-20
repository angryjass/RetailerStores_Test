using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailerStores.Application.Dto;
using RetailerStores.Application.Interfaces;
using RetailerStores.Domain;

namespace RetailerStores.Application.Metrics.Queries
{
    public class GetStockAccuracyMetricQuery : IRequest<StockMetricDto>
    {
        public class Handler : IRequestHandler<GetStockAccuracyMetricQuery, StockMetricDto>
        {
            private readonly IRetailerStoresDbContext _dbContext;

            public Handler(IRetailerStoresDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<StockMetricDto> Handle(GetStockAccuracyMetricQuery request, CancellationToken cancellationToken)
            {
                var actualStocks = await _dbContext.Set<Stock>().AsNoTracking()
                    .Include(a => a.Store)
                    .GroupBy(a => new { a.Id, a.StoreId, a.Store.Name })
                    .Select(a => new
                    {
                        a.Key.Id,
                        a.Key.StoreId,
                        RecordingDate = a.Max(c => c.RecordingDate)
                    })
                    .Select(a => a.Id)
                    .ToListAsync(cancellationToken);

                var valuesQuery = _dbContext.Set<Stock>().AsNoTracking()
                    .Include(a => a.Store)
                    .Where(a => actualStocks.Contains(a.Id))
                    .GroupBy(e => new
                    {
                        e.Store.Name,
                        e.StoreId,
                        e.RecordingDate,
                        e.Accuracy
                    })
                    .Select(e => new
                    {
                        e.Key.StoreId,
                        e.Key.Name,
                        e.Key.Accuracy,
                        e.Key.RecordingDate
                    });

                var minValue = await valuesQuery.MinAsync(a => a.Accuracy, cancellationToken);

                var maxValue = await valuesQuery.MaxAsync(a => a.Accuracy, cancellationToken);

                var avgValue = await valuesQuery.AverageAsync(a => a.Accuracy, cancellationToken);

                return new StockMetricDto()
                {
                    Values = new decimal[] { minValue, avgValue, maxValue }
                };
            }
        }
    }
}
