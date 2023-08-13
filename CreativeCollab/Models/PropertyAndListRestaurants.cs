using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativeCollab.Models
{
    public class PropertyAndListRestaurants
    {
        public PropertyDetailDTO Properties { get; set; }
        public IEnumerable<RestaurantDto> AllRestaurants { get; set; }
    }
}