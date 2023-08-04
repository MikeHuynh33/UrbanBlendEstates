using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CreativeCollab.Models
{
    public class Neighbourhood
    {
        [Key]
        public int NeighbourhoodId { get; set; }
        public string NeighbourhoodName { get; set; }
    }

    public class NeighbourhoodDto
    {
        public int NeighbourhoodId { get; set; }
        public string NeighbourhoodName { get; set; }
    }
}