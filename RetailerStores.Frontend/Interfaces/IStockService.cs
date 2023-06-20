using RetailerStores.Frontend.Models;
using RetailerStores.Frontend.Models.Dto;

namespace RetailerStores.Frontend.Interfaces
{
    public interface IStockService
    {
        public Task<StockDto?> Get(Guid storeId);
        public Task<StockDto> Create(CreateStockModel model);
    }
}
