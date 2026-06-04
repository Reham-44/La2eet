using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using LostAndFound.Models;

namespace LostAndFound.DbContexts
{
    public class LostAndFoundDbContext : DbContext
    {
        public LostAndFoundDbContext(DbContextOptions<LostAndFoundDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=.;database=LostAndFoundDB;Trusted_connection=True;TrustServerCertificate=True");
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<VerificationQuestion> VerificationQuestions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
