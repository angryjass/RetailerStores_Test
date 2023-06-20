using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RetailerStores.Frontend.Interfaces;
using RetailerStores.Frontend.Models.Dto;

namespace RetailerStores.Frontend.Pages
{
    public class EditStoreModel : PageModel
    {
        private IStoreService _storeService { get; set; }

        public EditStoreModel(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [BindProperty]
        public StoreExtendedDto Store { get; set; } = new StoreExtendedDto();

        public async Task OnGet(Guid id)
        {
            Store = await _storeService.Get(id);
        }

        public async Task<IActionResult> OnPost(StoreExtendedDto store)
        {
            if (!ModelState.IsValid)
                return Page();

            await _storeService.Update(store.ToUpdateStoreModel());
            return Redirect("/Stores");
        }
    }
}
