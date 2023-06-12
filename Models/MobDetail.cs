using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mob_monitoring_api.Models
{
    public class MobDetail
    {
        public String MobName { get; set; }
        public String MobType { get; set; }
        public List<MobCoords> MobCoords { get; set; }
        public String MobOperator { get; set; }
        public String MobOfficer { get; set; }
        public List<RedZone> MobRedzones { get; set; }
    }
}