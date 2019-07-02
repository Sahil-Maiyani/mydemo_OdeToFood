using System.Collections.Generic;
using OdeToFood.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext db;
        private readonly ILogger logger;

        public SqlRestaurantData(OdeToFoodDbContext db, ILogger<SqlRestaurantData> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        public Restaurant AddRestaurant(Restaurant newRestaurant)
        {
            db.Add(newRestaurant);
            return newRestaurant;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Restaurant DeleteRestaurant(int id)
        {
            var restaurant = GetRestuarantsById(id);
            if (restaurant != null)
            {
                db.Restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public int GetCountOfRestaurants()
        {
            return db.Restaurants.Count();
        }

        public Restaurant GetRestuarantsById(int restaturantId)
        {
            var Restaurant = db.Restaurants.Find(restaturantId);

            var query = from b in db.Bookings
                        where b.RestaurantId == restaturantId
                        select b;

            logger.LogDebug("Pre LOOP Restaurant.Bookings.Count value: {0}", Restaurant.Bookings);
            int debugi = 0;

            foreach (var booking in query.Distinct())
            {
                logger.LogDebug("Loop over query ITER {0}:  booking object customerName value {1}",
                                    debugi++, booking.CustomerName);

                Restaurant.Bookings.Add(booking);
            }

            logger.LogDebug("Restaurant.Bookings.Count value: {0}", Restaurant.Bookings.Count);
            return Restaurant;

        }

        public IEnumerable<Restaurant> GetRestuarantsByName(string name)
        {
            var query = from r in db.Restaurants
                        where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby r.Name
                        select r;
            return query;
        }

        public Restaurant mGetRestuarantsByName(string name)
        {
            return db.Restaurants.Where(r => r.Name == name).FirstOrDefault();
        }

        public bool NewBooking(int restaurantId, Booking newBooking)
        {
            newBooking.RestaurantId = restaurantId;
            db.Bookings.Add(newBooking);
            return true;
        }

        public Restaurant UpdateRestaurant(Restaurant updatedRestaurant)
        {
            var entity = db.Restaurants.Attach(updatedRestaurant);
            entity.State = EntityState.Modified;
            return updatedRestaurant;
        }
    }
}

