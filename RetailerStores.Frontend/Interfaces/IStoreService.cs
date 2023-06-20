using RetailerStores.Frontend.Models;
using RetailerStores.Frontend.Models.Dto;

namespace RetailerStores.Frontend.Interfaces
{
    public interface IStoreService
    {
        public Task<StoreDto> Create(CreateStoreModel model);
        public Task<StoreDto> Update(UpdateStoreModel model);
        public Task<StoreExtendedDto> Get(Guid id);
        public Task<List<StoreDto>> GetAll();
    }
}
