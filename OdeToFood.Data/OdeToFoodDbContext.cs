using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdeToFood.Data
{
    public class OdeToFoodDbContext : IdentityDbContext
    {
        public OdeToFoodDbContext(DbContextOptions<OdeToFoodDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
