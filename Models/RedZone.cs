//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mob_monitoring_api.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RedZone
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RedZone()
        {
            this.AllotedRedZones = new HashSet<AllotedRedZones>();
            this.RedZoneCoordinates = new HashSet<RedZoneCoordinates>();
        }
    
        public int RedZoneID { get; set; }
        public string Name { get; set; }
        public string IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AllotedRedZones> AllotedRedZones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RedZoneCoordinates> RedZoneCoordinates { get; set; }
    }
}
