using OdeToFood.Core;
using System.Linq;
using System.Collections.Generic;
using static OdeToFood.Core.Restaurant;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            var booking1 = new Booking { Id = 1, CustomerName = "micheal m", Person = 4, Phone = "8469692636", Time = "10:00" };
            var booking2 = new Booking { Id = 2, CustomerName = "joy j", Person = 2, Phone = "9876765645", Time = "09:00" };
            var booking3 = new Booking { Id = 3, CustomerName = "broad b", Person = 6, Phone = "9876765456", Time = "08:00" };
            var booking4 = new Booking { Id = 4, CustomerName = "alex a", Person = 2, Phone = "5467878987", Time = "01:00" };

            restaurants = new List<Restaurant>()
            {
                new Restaurant { Id = 1, Name = "Scott's Pizza", Location="Maryland", Cuisine=CuisineType.Italian, Bookings= new List<Booking>{ booking1, booking2 } },
                new Restaurant { Id = 2, Name = "Cinnamon Club", Location="London", Cuisine=CuisineType.Italian, Bookings= new List<Booking>{ booking3, booking4 }},
                new Restaurant { Id = 3, Name = "La Costa", Location = "California", Cuisine=CuisineType.Mexican, Bookings= new List<Booking>{ booking1, booking2 }}
            };
        }

        public Restaurant AddRestaurant(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }

        public int Commit()
        {
            return 0;
        }

        public Restaurant DeleteRestaurant(int id)
        {
            var restaurant = restaurants.FirstOrDefault(r => r.Id == id);
            if (restaurant != null)
            {
                restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public int GetCountOfRestaurants()
        {
            return restaurants.Count();
        }

        public IEnumerable<Restaurant> GetRestuarantsByName(string name = null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }

        public Restaurant UpdateRestaurant(Restaurant updatedRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);
            if (restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }
            return restaurant;
        }

        Restaurant IRestaurantData.GetRestuarantsById(int restaurantId)
        {
            return restaurants.SingleOrDefault(r => r.Id == restaurantId);
        }

        Restaurant IRestaurantData.mGetRestuarantsByName(string restaurantName)
        {
            return restaurants.SingleOrDefault(r => r.Name == restaurantName);
        }


        /***
        Author: sahil maiyani
        Date: 27-06-2019

        Input: 

        ***/
        public bool NewBooking(int restaurantId, Booking newBooking)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.Id == restaurantId);
            if (restaurant != null)
            {
                restaurant.Bookings.Add(newBooking);
            }

            return true;
        }
    }
}

