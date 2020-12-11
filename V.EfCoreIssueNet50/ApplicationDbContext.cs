using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace V.EfCoreIssueNet50
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Entities.Organization> Organizations { get; set; }

        public DbSet<Entities.Hierarchy> Hierarchies { get; set; }


        /// <summary>
        /// For LinqPad
        /// </summary>
        /// <param name="connectionString"></param>
        public ApplicationDbContext(string connectionString) : this(new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseSqlServer(connectionString).Options)
        {
        }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }

        /// <summary>
        /// Model creation
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Entities.Hierarchy>().HasKey(p => p.Id);
            modelBuilder.Entity<Entities.Hierarchy>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Entities.Hierarchy>().HasData(new Entities.Hierarchy { Id = Guid.Parse("de7ec252-a532-41d0-8566-be20d449874b"), Name = "Hiearchy A" });
            modelBuilder.Entity<Entities.Hierarchy>().HasData(new Entities.Hierarchy { Id = Guid.Parse("40285c27-ed61-4811-9bed-36d0783897de"), Name = "Hiearchy B" });

            modelBuilder.Entity<Entities.Organization>().HasKey(p => p.Id);
            modelBuilder.Entity<Entities.Organization>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Entities.Organization>().HasOne(x => x.Hierarchy).WithMany().HasForeignKey(x => x.HierarchyId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Entities.Organization>().HasData(new Entities.Organization { Id = Guid.Parse("fd25c099-edd0-43a5-b7db-1e577c4134ee"), Name = "Organization 1", HierarchyId = Guid.Parse("de7ec252-a532-41d0-8566-be20d449874b") });
            modelBuilder.Entity<Entities.Organization>().HasData(new Entities.Organization { Id = Guid.Parse("736987f7-487f-4b1c-8c39-60be3d601a85"), Name = "Organization 2", HierarchyId = Guid.Parse("40285c27-ed61-4811-9bed-36d0783897de") });
        }
    }
}
