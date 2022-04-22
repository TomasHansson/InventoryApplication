﻿// <auto-generated />
using AfryAnalytics.Capture.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AfryAnalytics.Capture.Migrations
{
    [DbContext(typeof(CapturePriceDbContext))]
    partial class CapturePriceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.4");

            modelBuilder.Entity("AfryAnalytics.Capture.Models.MarketPrices", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("EurosPerMw")
                        .HasColumnType("REAL");

                    b.Property<double>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<double>("Longitude")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("MarketPrices");
                });

            modelBuilder.Entity("AfryAnalytics.Capture.Models.WindSite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Availability")
                        .HasColumnType("REAL");

                    b.Property<double>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<double>("Longitude")
                        .HasColumnType("REAL");

                    b.Property<double>("MaxCapacityMw")
                        .HasColumnType("REAL");

                    b.Property<double>("WindSpeedAtMaxCapacity")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("WindSites");
                });

            modelBuilder.Entity("AfryAnalytics.Capture.Models.WindSpeed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("AverageWindSpeed")
                        .HasColumnType("REAL");

                    b.Property<double>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<double>("Longitude")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("WindSpeeds");
                });
#pragma warning restore 612, 618
        }
    }
}