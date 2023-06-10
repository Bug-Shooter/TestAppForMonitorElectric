﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TestAppForMonitorElectric.Data;

#nullable disable

namespace TestAppForMonitorElectric.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230610020432_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TestAppForMonitorElectric.Models.CarModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ManufacturerID")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("StartSellPrice")
                        .HasColumnType("integer");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerID");

                    b.ToTable("CarModels");
                });

            modelBuilder.Entity("TestAppForMonitorElectric.Models.Manufacturer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Counrty")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .HasDatabaseName("Name");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("TestAppForMonitorElectric.Models.CarModel", b =>
                {
                    b.HasOne("TestAppForMonitorElectric.Models.Manufacturer", "Manufacturer")
                        .WithMany("CarModels")
                        .HasForeignKey("ManufacturerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("TestAppForMonitorElectric.Models.Manufacturer", b =>
                {
                    b.Navigation("CarModels");
                });
#pragma warning restore 612, 618
        }
    }
}
