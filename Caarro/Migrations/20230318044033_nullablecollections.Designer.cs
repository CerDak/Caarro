﻿// <auto-generated />
using System;
using Caarro.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Caarro.Migrations
{
    [DbContext(typeof(CaarroDbContext))]
    [Migration("20230318044033_nullablecollections")]
    partial class nullablecollections
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("Caarro.Data.Expense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Driver")
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<uint>("Odometer")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("TEXT");

                    b.Property<string>("Reason")
                        .HasColumnType("TEXT");

                    b.Property<int?>("VehicleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("Caarro.Data.Income", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Driver")
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<uint>("Odometer")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("VehicleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("Income");
                });

            modelBuilder.Entity("Caarro.Data.Refueling", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Driver")
                        .HasColumnType("TEXT");

                    b.Property<string>("Fuel")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("FuelAmount")
                        .HasColumnType("REAL");

                    b.Property<string>("Location")
                        .HasColumnType("TEXT");

                    b.Property<uint>("Odometer")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<bool>("ToFull")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("VehicleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("Refueling");
                });

            modelBuilder.Entity("Caarro.Data.Reminder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<uint>("ReminderPeriodDistance")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan?>("ReminderPeriodTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("Service")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("VehicleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("Reminders");
                });

            modelBuilder.Entity("Caarro.Data.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Driver")
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .HasColumnType("TEXT");

                    b.Property<uint>("Odometer")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("VehicleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("Caarro.Data.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<double>("FuelCapacity")
                        .HasColumnType("REAL");

                    b.Property<int>("FuelType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LicensePlate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("TEXT");

                    b.Property<string>("Model")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UnitOfMeasure")
                        .HasColumnType("INTEGER");

                    b.Property<string>("VehicleIdentificationNumber")
                        .HasColumnType("TEXT");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("Caarro.Data.Expense", b =>
                {
                    b.HasOne("Caarro.Data.Vehicle", null)
                        .WithMany("Expenses")
                        .HasForeignKey("VehicleId");
                });

            modelBuilder.Entity("Caarro.Data.Income", b =>
                {
                    b.HasOne("Caarro.Data.Vehicle", null)
                        .WithMany("Income")
                        .HasForeignKey("VehicleId");
                });

            modelBuilder.Entity("Caarro.Data.Refueling", b =>
                {
                    b.HasOne("Caarro.Data.Vehicle", null)
                        .WithMany("Refuelings")
                        .HasForeignKey("VehicleId");
                });

            modelBuilder.Entity("Caarro.Data.Reminder", b =>
                {
                    b.HasOne("Caarro.Data.Vehicle", null)
                        .WithMany("Reminders")
                        .HasForeignKey("VehicleId");
                });

            modelBuilder.Entity("Caarro.Data.Service", b =>
                {
                    b.HasOne("Caarro.Data.Vehicle", null)
                        .WithMany("Services")
                        .HasForeignKey("VehicleId");
                });

            modelBuilder.Entity("Caarro.Data.Vehicle", b =>
                {
                    b.Navigation("Expenses");

                    b.Navigation("Income");

                    b.Navigation("Refuelings");

                    b.Navigation("Reminders");

                    b.Navigation("Services");
                });
#pragma warning restore 612, 618
        }
    }
}
