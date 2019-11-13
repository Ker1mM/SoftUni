using Messages.Web.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messages.Web.Data
{
    public class MessagesDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public DbSet<User> Users { get; set; }

        public MessagesDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
