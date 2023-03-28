﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Weather;

#nullable disable

namespace weather.Migrations
{
    [DbContext(typeof(WeatherContext))]
    [Migration("20230323093727_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Weather.SWeather", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LocationID")
                        .HasColumnType("int");

                    b.Property<int>("TemperatureC")
                        .HasColumnType("int");

                    b.Property<double>("WindSpeed")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.HasIndex("LocationID");

                    b.ToTable("weathers");
                });

            modelBuilder.Entity("weather.Location", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("locations");
                });

            modelBuilder.Entity("Weather.SWeather", b =>
                {
                    b.HasOne("weather.Location", "Location")
                        .WithMany("Weathers")
                        .HasForeignKey("LocationID");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("weather.Location", b =>
                {
                    b.Navigation("Weathers");
                });
#pragma warning restore 612, 618
        }
    }
}
