﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OrkIdea.QC.DAL
{
    using OrkIdea.QC.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QCEntities : DbContext
    {
        public QCEntities()
            : base("name=QCEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomerParameter> CustomerParameter { get; set; }
        public virtual DbSet<Document> Document { get; set; }
        public virtual DbSet<DocumentType> DocumentType { get; set; }
        public virtual DbSet<DocumentVersion> DocumentVersion { get; set; }
        public virtual DbSet<GenericMenu> GenericMenu { get; set; }
        public virtual DbSet<GenericParameter> GenericParameter { get; set; }
        public virtual DbSet<Page> Page { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleMenu> RoleMenu { get; set; }
        public virtual DbSet<SideBar> SideBar { get; set; }
        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<WorkFlow> WorkFlow { get; set; }
        public virtual DbSet<Process> Process { get; set; }
    }
}