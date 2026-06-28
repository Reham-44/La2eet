using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LostAndFound.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using LostAndFound.Enums;

namespace LostAndFound.ModelConfigrations
{
    internal class ItemConfiguration:IEntityTypeConfiguration<Item>
    {
        
            public void Configure(EntityTypeBuilder<Item> builder)
            {
            builder.ToTable("Items");
            builder.HasKey(i => i.ItemId);
            builder.Property(i => i.Title)
                .IsRequired()
                .HasMaxLength(55);
            builder.Property(i => i.Description)
                .HasMaxLength(255);
            builder.Property(i => i.City)
                  .IsRequired()
                  .HasConversion<string>();
            builder.Property(i => i.Location)
                .HasMaxLength(100);
            builder.Property(i => i.Status)
               .IsRequired();
            builder.Property(i => i.ReportStatus)
               .HasDefaultValue(ReportStatus.Pending);
            builder.Property(i => i.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
            builder.Property(i => i.LostOrFoundDate)
                .HasColumnType("date");
            builder.Property(i => i.IsResolved)
                .HasDefaultValue(false);
    
            builder.HasOne(i => i.User)
                .WithMany(u => u.Items)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Restrict);

             }

    }
}