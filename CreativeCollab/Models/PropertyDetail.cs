using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CreativeCollab.Models
{
    public class PropertyDetail
    {
        [Key]
        public int PropertyID { get; set; }
        public string PropertyType { get; set; }
        public string PropertyAddress { get; set; }
        public string PropertySize { get; set; }
        public int NumberOfBedrooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public string Amenities { get; set; }
        public Double PropertyPrice { get; set; }
        public string PropertyDescription { get; set; }
        public string PropertyStatus { get; set; }
        public string ImageFileNames { get; set; }
        
        [ForeignKey("Neighbourhood")]
        public int NeighbourhoodId { get; set; }
        public virtual Neighbourhood Neighbourhood { get; set; }
        public DateTime ListingDate { get; set; }
        //declare MXM relationship
        public ICollection<EstateAgent> Agents { get; set; }

    }
    public class PropertyDetailDTO
    {
        public int PropertyID { get; set; }
        public string PropertyType { get; set; }
        public string PropertyAddress { get; set; }
        public string PropertySize { get; set; }
        public int NumberOfBedrooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public string Amenities { get; set; }
        public string ImageFileNames { get; set; }
        public Double PropertyPrice { get; set; }
        public string PropertyDescription { get; set; }
        public string PropertyStatus { get; set; }
        public int NeighbourhoodId { get; set; }
        public DateTime ListingDate { get; set; }

        public List<EstateAgentDTO> Agents { get; set; }
    }
}