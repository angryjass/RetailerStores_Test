using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RetailerStores.Frontend.Interfaces;
using RetailerStores.Frontend.Models.Dto;

namespace RetailerStores.Frontend.Pages
{
    public class CreateStockModel : PageModel
    {
        private IStockService _stockService;
        private IStoreService _storeService;

        public CreateStockModel(IStockService stockService, IStoreService storeService)
        {
            _stockService = stockService;
            _storeService = storeService;
        }

        public Models.CreateStockModel Stock { get; set; } = new Models.CreateStockModel();
        public List<SelectListItem> Stores { get; set; } = new List<SelectListItem>();


        public async Task OnGet(Guid? id)
        {
            var storesList = new List<StoreDto>();

            if (id.HasValue)
            {
                storesList = new List<StoreDto> { await _storeService.Get(id.Value) };
            }
            else
            {
                storesList = await _storeService.GetAll();
            }

            Stores = storesList.Select(a =>
                  new SelectListItem
                  {
                      Value = a.Id.ToString(),
                      Text = a.Name
                  }).ToList();

            if (Request.Headers.TryGetValue("Referer", out var referer))
            {
                ViewData["Referer"] = referer;
            }
        }

        public async Task<IActionResult> OnPost(Models.CreateStockModel stock)
        {
            if (!ModelState.IsValid)
                return Page();

            await _stockService.Create(stock);
            return Redirect("Stores");
        }
    }
}
