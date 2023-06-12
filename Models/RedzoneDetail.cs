using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mob_monitoring_api.Models
{
    public class RedzoneDetail
    {
        public RedZone redZone { get; set; }
        public List<RedZoneCoordinates> redZoneCoords { get; set; }
    }
}