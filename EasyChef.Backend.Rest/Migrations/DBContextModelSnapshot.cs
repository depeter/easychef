﻿// <auto-generated />
using EasyChef.Backend.Rest;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace EasyChef.Backend.Rest.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EasyChef.Backend.Rest.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AvailableOnUrl");

                    b.Property<long>("ExternalId");

                    b.Property<bool>("HasProducts");

                    b.Property<DateTime?>("LastProductScan");

                    b.Property<DateTime?>("LastScan");

                    b.Property<string>("Link");

                    b.Property<string>("Name");

                    b.Property<int?>("ParentId");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("EasyChef.Backend.Rest.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("LastScan");

                    b.Property<string>("Name");

                    b.Property<string>("Price");

                    b.Property<string>("SKU");

                    b.Property<string>("Unit");

                    b.Property<string>("UnitPrice");

                    b.Property<string>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("EasyChef.Backend.Rest.Models.Category", b =>
                {
                    b.HasOne("EasyChef.Backend.Rest.Models.Category", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("EasyChef.Backend.Rest.Models.Product", b =>
                {
                    b.HasOne("EasyChef.Backend.Rest.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
