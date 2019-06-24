using OdeToFood.Core;
using System.Collections.Generic;
using System.Text;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestuarantsByName(string name);
        Restaurant GetRestuarantsById(int restaturantId);

        /*** 
         Author: sahil maiyani
         Date: 21-06-2019

         Input: `string` restaurant name
         Output: `Restaurant` object defined in `OdeToFood.Core`

         Implementation: Method  take restaurant name as parameter and return restaurant matching data from database in `Restaurant` object.
         
         P.S: method name starts with m letter represents this method is additionally defined for extra implematation by author in project
         EI Code (Extra Implemantation- Used for identify changes): 01
         ***/
        Restaurant mGetRestuarantsByName(string name);

        Restaurant UpdateRestaurant(Restaurant updatedRestaurant);

        Restaurant AddRestaurant(Restaurant newRestaurant);

        Restaurant DeleteRestaurant(int id);

        int Commit();
    }
}

