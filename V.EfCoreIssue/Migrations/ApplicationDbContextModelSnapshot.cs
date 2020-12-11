﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using V.EfCoreIssue;

namespace V.EfCoreIssue.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("V.EfCoreIssue.Entities.Hierarchy", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Hierarchies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("de7ec252-a532-41d0-8566-be20d449874b"),
                            Name = "Hiearchy A"
                        },
                        new
                        {
                            Id = new Guid("40285c27-ed61-4811-9bed-36d0783897de"),
                            Name = "Hiearchy B"
                        });
                });

            modelBuilder.Entity("V.EfCoreIssue.Entities.Organization", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HierarchyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HierarchyId");

                    b.ToTable("Organizations");

                    b.HasData(
                        new
                        {
                            Id = new Guid("fd25c099-edd0-43a5-b7db-1e577c4134ee"),
                            HierarchyId = new Guid("de7ec252-a532-41d0-8566-be20d449874b"),
                            Name = "Organization 1"
                        },
                        new
                        {
                            Id = new Guid("736987f7-487f-4b1c-8c39-60be3d601a85"),
                            HierarchyId = new Guid("40285c27-ed61-4811-9bed-36d0783897de"),
                            Name = "Organization 2"
                        });
                });

            modelBuilder.Entity("V.EfCoreIssue.Entities.Organization", b =>
                {
                    b.HasOne("V.EfCoreIssue.Entities.Hierarchy", "Hierarchy")
                        .WithMany()
                        .HasForeignKey("HierarchyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
