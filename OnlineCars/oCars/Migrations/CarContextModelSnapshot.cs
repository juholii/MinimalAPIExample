﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace oCars.Migrations
{
    [DbContext(typeof(CarContext))]
    partial class CarContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Cars");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Car");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("ElectricCar", b =>
                {
                    b.HasBaseType("Car");

                    b.Property<int>("BatteryCapacity")
                        .HasColumnType("INTEGER");

                    b.HasDiscriminator().HasValue("ElectricCar");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Brand = "BMW",
                            Model = "i4",
                            Year = 2022,
                            BatteryCapacity = 81
                        },
                        new
                        {
                            Id = 2,
                            Brand = "Audi",
                            Model = "e-tron",
                            Year = 2020,
                            BatteryCapacity = 71
                        },
                        new
                        {
                            Id = 3,
                            Brand = "Mercedes-Benz",
                            Model = "EQE",
                            Year = 2022,
                            BatteryCapacity = 100
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
