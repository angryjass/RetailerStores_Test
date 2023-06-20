using RetailerStores.Frontend.Models.Dto;

namespace RetailerStores.Frontend.Interfaces
{
    public interface IMetricService
    {
        public Task<StockMetricDto> GetStockAccuracy();
        public Task<StockMetricDto> GetStockMeanAge();
        public Task<StockMetricDto> GetStockOnFloorAvailability();
        public Task<StockMetricDto> GetTotalStock();
    }
}
