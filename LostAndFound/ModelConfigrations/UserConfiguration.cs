using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LostAndFound.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LostAndFound.ModelConfigrations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(u => u.Email)
                .HasMaxLength(100);


            builder.Property(u=>u.PasswordHash)
                .HasMaxLength(255);
            builder.Property(u=>u.Phone)
                .HasMaxLength(20);
            builder.Property(u=>u.Role)
                .HasMaxLength (20)
                .HasDefaultValue("User");
            builder.Property(u => u.IsVerified)
                .HasDefaultValue(false);
            builder.Property(u => u.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
