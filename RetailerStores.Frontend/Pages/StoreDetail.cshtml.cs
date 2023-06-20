using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RetailerStores.Frontend.Interfaces;
using RetailerStores.Frontend.Models.Dto;

namespace RetailerStores.Frontend.Pages
{
    public class StoreDetailModel : PageModel
    {
        private IStoreService _storeService;
        private IStockService _stockService;

        public StoreDetailModel(IStoreService storeService, IStockService stockService)
        {
            _storeService = storeService;
            _stockService = stockService;
        }

        public StoreExtendedDto Store { get; set; } = new StoreExtendedDto();
        public StockDto Stock { get; set; } = new StockDto();

        public async Task OnGet(Guid id)
        {
            Store = await _storeService.Get(id);
            Stock = await _stockService.Get(id);
        }
    }
}
