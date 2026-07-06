using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LostAndFound.Models;

namespace LostAndFound.DbContexts
{
    public class LostAndFoundDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public LostAndFoundDbContext(DbContextOptions<LostAndFoundDbContext> options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<VerificationQuestion> VerificationQuestions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
