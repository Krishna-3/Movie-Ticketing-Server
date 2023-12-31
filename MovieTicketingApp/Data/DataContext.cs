﻿using Microsoft.EntityFrameworkCore;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Theatre> Theatres { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Seat> Seats { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<MovieLocation> MovieLocations { get; set; }

        public DbSet<MovieTheatre> MovieTheatres { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<State> States { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieLocation>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<MovieLocation>()
                .HasOne(m=>m.Movie)
                .WithMany()
                .HasForeignKey(ml => ml.MovieId);

            modelBuilder.Entity<MovieLocation>()
                .HasOne(l=>l.Location)
                .WithMany()
                .HasForeignKey(ml => ml.LocationId);

            modelBuilder.Entity<Ticket>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Seat)
                .WithMany()
                .HasForeignKey(t => t.SeatId);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.MovieTheatre)
                .WithMany()
                .HasForeignKey(t => t.MovieTheatreId);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<MovieTheatre>()
                .HasKey(mt => mt.Id);

            modelBuilder.Entity<MovieTheatre>()
               .HasOne(m => m.Movie)
               .WithMany()
               .HasForeignKey(mt => mt.MovieId);

            modelBuilder.Entity<MovieTheatre>()
               .HasOne(t=> t.Theatre)
               .WithMany()
               .HasForeignKey(mt => mt.TheatreId);

            modelBuilder.Entity<Theatre>()
               .HasKey(t => t.Id);

            modelBuilder.Entity<Theatre>()
               .HasOne(t => t.Location)
               .WithMany() 
               .HasForeignKey(t => t.LocationId);

            modelBuilder.Entity<Movie>()
               .HasKey(t => t.Id);

            modelBuilder.Entity<Seat>()
               .HasKey(t => t.Id);

            modelBuilder.Entity<Location>()
               .HasKey(t => t.Id);

            modelBuilder.Entity<User>()
               .HasKey(t => t.Id);

            modelBuilder.Entity<RefreshToken>()
                .HasKey(t => t.Id);
            
            modelBuilder.Entity<State>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<State>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<RefreshToken>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(t => t.UserId);
        }
    }
} 
