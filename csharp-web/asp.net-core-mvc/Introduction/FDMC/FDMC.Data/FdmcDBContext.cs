using FDMC.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace FDMC.Data
{
    public class FdmcDBContext : DbContext

    {
        public DbSet<Cat> Cats { get; set; }

        public FdmcDBContext(DbContextOptions<FdmcDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
