using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OdeToFood.Core;
using OdeToFood.Data;

namespace mydemo_OdeToFood.Pages.Restaurants
{
    public class NewBookingModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        private readonly ILogger logger;

        [BindProperty(SupportsGet = true)]
        public int RestaurantId { get; set; }

        [BindProperty]
        public Booking Booking { get; set; }

        public NewBookingModel(IRestaurantData restaurantData, ILogger<NewBookingModel> logger)
        {
            this.restaurantData = restaurantData;
            this.logger = logger;
        }

        public void OnGet(int restaurantId)
        {
            logger.LogError("logging from get");
            RestaurantId = restaurantId;
        }

        public IActionResult OnPost()
        {
            logger.LogError("logging");
            logger.LogError("Restaurant id: {0}, booking object: {1}", RestaurantId, Booking.CustomerName);
            restaurantData.NewBooking(RestaurantId, Booking);
            restaurantData.Commit();
            return RedirectToPage("Detail", new { restaurantId = RestaurantId });
        }
    }
}