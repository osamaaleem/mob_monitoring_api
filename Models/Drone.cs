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
    
    public partial class Drone
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Drone()
        {
            this.AllotedDrones = new HashSet<AllotedDrones>();
            this.Image = new HashSet<Image>();
            this.Image1 = new HashSet<Image>();
            this.Video = new HashSet<Video>();
            this.Video1 = new HashSet<Video>();
        }
    
        public int DroneID { get; set; }
        public string Name { get; set; }
        public Nullable<bool> IsAvailable { get; set; }
        public Nullable<double> Battery { get; set; }
        public Nullable<bool> IsCharged { get; set; }
        public Nullable<int> BufferSizeMb { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AllotedDrones> AllotedDrones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Image> Image { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Image> Image1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Video> Video { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Video> Video1 { get; set; }
    }
}
