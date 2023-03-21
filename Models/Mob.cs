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
    
    public partial class Mob
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Mob()
        {
            this.Barrier = new HashSet<Barrier>();
            this.Image = new HashSet<Image>();
            this.MobDetail = new HashSet<MobDetail>();
            this.Video = new HashSet<Video>();
        }
    
        public int MobID { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> ProputedStrength { get; set; }
        public Nullable<int> ActualStrength { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<double> MobStartLat { get; set; }
        public Nullable<double> MobStartLon { get; set; }
        public Nullable<double> MobEndLat { get; set; }
        public Nullable<double> MobEndLon { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Barrier> Barrier { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Image> Image { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MobDetail> MobDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Video> Video { get; set; }
    }
}
