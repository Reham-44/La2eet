using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LostAndFound.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LostAndFound.ModelConfigrations
{
    internal class ItemConfiguration:IEntityTypeConfiguration<Item>
    {
        
            public void Configure(EntityTypeBuilder<Item> builder)
            {
            builder.ToTable("Items");
            builder.HasKey(i => i.ItemId);
            builder.Property(i => i.Title)
                .HasMaxLength(55);
            builder.Property(i => i.Description)
                .HasMaxLength(255);
          builder.Property(i=>i.City)
                .HasMaxLength(50);
            builder.Property(i=>i.Status)
                .HasMaxLength(10);
            builder.Property(i => i.CreatedAt)
                .HasDefaultValueSql("GETDATE()"); ;
            builder.Property(i => i.LostOrFoundDate)
                .HasColumnType("date");
            builder.Property(i => i.IsResolved);
            builder.Property(i => i.UserId);
            builder.Property(i => i.Image)
                .HasMaxLength(500);

            builder.HasOne(i => i.User)
                .WithMany(u => u.Items)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(i => i.VerificationQuestions)
                .WithOne(v => v.Item)
                .HasForeignKey(v => v.ItemId)
                .OnDelete(DeleteBehavior.Restrict);


             }

    }
}