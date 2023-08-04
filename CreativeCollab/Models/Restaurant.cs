using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CreativeCollab.Migrations;

namespace CreativeCollab.Models
{
    public class Restaurant
    {
        [Key]
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        
        [ForeignKey("Neighbourhood")]
        public int NeighbourhoodId { get; set; }
        public virtual Neighbourhood Neighbourhood { get; set; }
        
        public string RestaurantLink { get; set; }
    }

    public class RestaurantDto
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string RestaurantLink { get; set; }

        public int NeighbourhoodId { get; set; }
        public string NeighbourhoodName { get; set; }
    }
}