﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FYP_DBEntities : DbContext
    {
        public FYP_DBEntities()
            : base("name=FYP_DBEntities")
        {
            Configuration.ProxyCreationEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AllotedDrones> AllotedDrones { get; set; }
        public virtual DbSet<AllotedRedZones> AllotedRedZones { get; set; }
        public virtual DbSet<Barrier> Barrier { get; set; }
        public virtual DbSet<Drone> Drone { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Mob> Mob { get; set; }
        public virtual DbSet<MobCoords> MobCoords { get; set; }
        public virtual DbSet<MobOfficer> MobOfficer { get; set; }
        public virtual DbSet<MobOperator> MobOperator { get; set; }
        public virtual DbSet<MobTimeline> MobTimeline { get; set; }
        public virtual DbSet<PreDefCoords> PreDefCoords { get; set; }
        public virtual DbSet<QRF> QRF { get; set; }
        public virtual DbSet<RedZone> RedZone { get; set; }
        public virtual DbSet<RedZoneCoordinates> RedZoneCoordinates { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Video> Video { get; set; }
    }
}
