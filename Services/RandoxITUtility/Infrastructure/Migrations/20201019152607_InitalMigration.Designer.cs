﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RandoxITUtility.Infrastructure.Data.Contexts;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(RandoxITUtilityContext))]
    [Migration("20201019152607_InitalMigration")]
    partial class InitalMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0-rc.1.20451.13");

            modelBuilder.Entity("RandoxITUtility.Domain.Entities.Test", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("RandoxITUtility.Domain.Entities.TimeData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("DayOfTheWeek")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("EndTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("LunchLength")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("StartTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime>("WeekCommencing")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("WeeklyTimes");
                });
#pragma warning restore 612, 618
        }
    }
}
