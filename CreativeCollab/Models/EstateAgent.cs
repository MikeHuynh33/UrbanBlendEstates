using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativeCollab.Models
{
    public class EstateAgent
    {
        [Key]
        public int EstateAgentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }

        public ICollection<PropertyDetail> Properties { get; set; }
    }
    public class EstateAgentDTO
    {
        public int EstateAgentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }

        public List<PropertyDetailDTO> Properties { get; set; }
    }
}