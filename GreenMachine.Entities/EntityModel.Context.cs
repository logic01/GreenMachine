﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GreenMachine.Layer.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GreenMachineContext : DbContext
    {
        public GreenMachineContext()
            : base("name=GreenMachineContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Log> Logs { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Plant> Plant { get; set; }
        public DbSet<PlantTrait> PlantTrait { get; set; }
        public DbSet<Trait> Trait { get; set; }
    }
}
