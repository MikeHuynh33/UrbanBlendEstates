using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativeCollab.Models.ViewModels
{
    public class DetailsNeighbourhood
    {
        public NeighbourhoodDto SelectedNeighbourhood { get; set; }
        public IEnumerable<RestaurantDto> RelatedRestaurants { get; set; }
    }
}