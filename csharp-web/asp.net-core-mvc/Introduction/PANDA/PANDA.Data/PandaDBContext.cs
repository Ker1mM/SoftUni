using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PANDA.Domain;
using System;

namespace PANDA.Data
{
    public class PandaDBContext : IdentityDbContext<PandaUser>
    {

        public DbSet<Package> Packages { get; set; }
        public DbSet<Receipt> Receipts { get; set; }

        public PandaDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Receipt>()
                .HasOne<Package>(x => x.Package)
                .WithOne(x => x.Receipt)
                .HasForeignKey<Package>(x => x.ReceiptId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Receipt>()
                .HasOne(x => x.Recipient)
                .WithMany(x => x.Receipts)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Package>()
                .HasOne(x => x.Receipt)
                .WithOne(x => x.Package)
                .HasForeignKey<Receipt>(x => x.PackageId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
