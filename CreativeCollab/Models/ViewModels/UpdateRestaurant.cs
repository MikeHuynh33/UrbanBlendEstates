using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativeCollab.Models.ViewModels
{
    public class UpdateRestaurant
    {
        public RestaurantDto SelectedRestaurant { get; set; }

        //all species to choose from when updating this review
        public IEnumerable<NeighbourhoodDto> NeighbourhoodOptions { get; set; }
    }
}