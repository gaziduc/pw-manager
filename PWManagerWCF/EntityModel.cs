using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PWManagerWCF.Models;

namespace PWManagerWCF
{
    public class EntityModel : DbContext
    {
        public EntityModel()
            : base(Models.Infrastructure.PWManagerConfiguration.ConnectionString)
        {
        }

        public virtual DbSet<categories> Categories { get; set; }
        public virtual DbSet<service_credentials> ServiceCredentials { get; set; }
        public virtual DbSet<users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<categories>();
            modelBuilder.Entity<service_credentials>();
            modelBuilder.Entity<users>();
        }
    }
}