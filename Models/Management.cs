using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mob_monitoring_api.Models
{
    public class Management
    {
        public List<String> Mobs { get; set; }
        public List<String> Users { get; set; }
        public List<String> Operators { get; set; }
        public List<String> Drones { get; set; }
        public List<String> RedZones { get; set; }
    }
}