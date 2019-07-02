using System;
using System.Collections.Generic;
using System.Text;

namespace OdeToFood.Core
{
    public class Booking
    {
        public int Id {get; set;}

        public int RestaurantId { get; set; }

        public string CustomerName { get; set; }

        public string Time { get; set; }

        public int Person { get; set; }

        public string Phone { get; set; }

    }
}
