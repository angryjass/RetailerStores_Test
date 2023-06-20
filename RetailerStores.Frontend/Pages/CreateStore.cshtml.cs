using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RetailerStores.Frontend.Interfaces;

namespace RetailerStores.Frontend.Pages
{
    public class CreateStoreModel : PageModel
    {
        private IStoreService _storeService;

        public CreateStoreModel(IStoreService storeService)
        {
            _storeService = storeService;
        }

        public Models.CreateStoreModel Store { get; set; } = new Models.CreateStoreModel();

        public async Task<IActionResult> OnPost(Models.CreateStoreModel store)
        {
            if (!ModelState.IsValid)
                return Page();

            await _storeService.Create(store);
            return Redirect("/Stores");
        }
    }
}
