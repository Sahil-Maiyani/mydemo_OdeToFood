using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace mydemo_OdeToFood.Pages.Restaurants
{
    public class DetailModel : PageModel
    {
        private readonly IRestaurantData restaurantData;

        [TempData]
        public string Message { get; set; }
        public Restaurant Restaurant;

        public DetailModel(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }

        public IActionResult OnGet(int restaurantId)
        {
            /* EI code: 01
             * Note: also change in `OnGet` method paratmeter from `string restaurantName` to `int restaurantId`
             * 
             *  Restaurant = restaurantData.GetRestuarantsById(restaurantId);
             */
            Restaurant = restaurantData.GetRestuarantsById(restaurantId);

            if (Restaurant == null)
            {
                return RedirectToPage("NotFound");
            }
            return Page();
        }
    }
}