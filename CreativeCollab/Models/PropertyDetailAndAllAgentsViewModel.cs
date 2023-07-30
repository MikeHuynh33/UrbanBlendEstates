using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativeCollab.Models
{
    public class PropertyDetailAndAllAgentsViewModel
    {
        public PropertyDetailDTO Properties { get; set; }
        public IEnumerable<EstateAgentDTO> AllAgents { get; set; }
    }
}