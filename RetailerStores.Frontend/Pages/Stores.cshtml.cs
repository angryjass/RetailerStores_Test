using Microsoft.AspNetCore.Mvc.RazorPages;
using RetailerStores.Frontend.Interfaces;
using RetailerStores.Frontend.Models.Dto;

namespace RetailerStores.Frontend.Pages
{
    public class StoresModel : PageModel
    {
        private readonly IStoreService _storeService;

        public StoresModel(IStoreService storeService)
        {
            _storeService = storeService;
        }

        public List<StoreDto> Stores { get; set; } = new List<StoreDto>();

        public async Task OnGet()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            Stores = await _storeService.GetAll();
        }
    }
}