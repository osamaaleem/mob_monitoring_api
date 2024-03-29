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
    
    public partial class Image
    {
        public int ImgID { get; set; }
        public Nullable<int> MobID_FK { get; set; }
        public Nullable<System.DateTime> ImgDateTime { get; set; }
        public string ImgPath { get; set; }
        public Nullable<double> ImgLat { get; set; }
        public Nullable<double> ImgLon { get; set; }
        public Nullable<int> DroneID_FK { get; set; }
    
        public virtual Drone Drone { get; set; }
        public virtual Drone Drone1 { get; set; }
        public virtual Mob Mob { get; set; }
    }
}
