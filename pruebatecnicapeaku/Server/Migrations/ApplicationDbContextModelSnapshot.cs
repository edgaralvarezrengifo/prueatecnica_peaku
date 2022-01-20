﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using pruebatecnicapeaku.Server.Models;

namespace pruebatecnicapeaku.Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("pruebatecnicapeaku.Shared.Entities.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Creationdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdServiceProvider")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("serviceproviderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("serviceproviderId");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("pruebatecnicapeaku.Shared.Entities.ServiceProvider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Creationdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Nit")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("ServiceProviders");
                });

            modelBuilder.Entity("pruebatecnicapeaku.Shared.Entities.Service", b =>
                {
                    b.HasOne("pruebatecnicapeaku.Shared.Entities.ServiceProvider", "serviceprovider")
                        .WithMany()
                        .HasForeignKey("serviceproviderId");

                    b.Navigation("serviceprovider");
                });
#pragma warning restore 612, 618
        }
    }
}
