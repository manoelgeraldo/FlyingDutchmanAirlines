﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using FlyingDutchmanAirlines.DatabaseLayer.Models;

namespace FlyingDutchmanAirlines.DatabaseLayer
{
    public class FlyingDutchmanAirlinesContext : DbContext
    {
        public FlyingDutchmanAirlinesContext()
        {
        }

        public FlyingDutchmanAirlinesContext(DbContextOptions<FlyingDutchmanAirlinesContext> options)
            : base(options)
        {
            base.Database.EnsureDeleted();
        }

        public virtual DbSet<Airport> Airports { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Flight> Flights { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = Environment.GetEnvironmentVariable("FlyingDutchmanAirlines_Database_Connection_String") ?? string.Empty;
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airport>(entity =>
            {
                entity.ToTable("Airport");

                entity.Property(e => e.AirportId)
                    .ValueGeneratedNever()
                    .HasColumnName("AirportID");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Iata)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("IATA");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");

                entity.Property(e => e.BookingId).HasColumnName("BookingID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Booking__Customer");

                entity.HasOne(d => d.FlightNumberNavigation)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.FlightNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Booking__FlightNumberNavigation");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasKey(e => e.FlightNumber);

                entity.ToTable("Flight");

                entity.Property(e => e.FlightNumber).ValueGeneratedNever();

                entity.HasOne(d => d.DestinationNavigation)
                    .WithMany(p => p.FlightDestinationNavigations)
                    .HasForeignKey(d => d.Destination)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flight_AirportDestination");

                entity.HasOne(d => d.OriginNavigation)
                    .WithMany(p => p.FlightOriginNavigations)
                    .HasForeignKey(d => d.Origin)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
