﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VirtualLibDatabase
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class vlEntities : DbContext
    {
        public vlEntities()
            : base("name=vlEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<books> books { get; set; }
        public virtual DbSet<copies> copies { get; set; }
        public virtual DbSet<users> users { get; set; }
        public virtual DbSet<categories> categoriesSet { get; set; }
    }
}
