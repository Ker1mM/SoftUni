using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rider.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rider.Data
{
    public class RiderDBContext : IdentityDbContext<Player, IdentityRole, string>
    {
        public DbSet<Bike> Bikes { get; set; }

        public DbSet<Attempt> Attempts { get; set; }

        public DbSet<Part> Parts { get; set; }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<Ware> Wares { get; set; }

        public DbSet<BikeParts> BikeParts { get; set; }

        public DbSet<PlayerParts> PlayerParts { get; set; }

        public RiderDBContext(DbContextOptions<RiderDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<PlayerParts>()
                .HasOne<Player>(x => x.Player)
                .WithMany(x => x.Inventory)
                .HasForeignKey(x => x.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PlayerParts>()
                .HasOne<Part>(x => x.Part)
                .WithMany(x => x.PlayerParts)
                .HasForeignKey(x => x.PartId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Attempt>()
                .HasOne<Player>(x => x.Player)
                .WithMany(x => x.Attempts)
                .HasForeignKey(x => x.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Attempt>()
                 .HasOne<Bike>(x => x.PlayerBike)
                 .WithMany(x => x.Attempts)
                 .HasForeignKey(x => x.PlayerBikeId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Attempt>()
                  .HasOne<Track>(x => x.Track)
                  .WithMany(x => x.Attempts)
                  .HasForeignKey(x => x.TrackId)
                  .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
