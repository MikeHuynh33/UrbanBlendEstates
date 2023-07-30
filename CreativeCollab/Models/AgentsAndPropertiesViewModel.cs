﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativeCollab.Models
{
    public class AgentsAndPropertiesViewModel
    {
        public IEnumerable<EstateAgentDTO> Agents { get; set; }
        public IEnumerable<PropertyDetailDTO> Properties { get; set; }
    }
}