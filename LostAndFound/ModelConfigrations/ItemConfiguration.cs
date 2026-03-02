using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LostAndFound.models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

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
            builder.Property(i => i.CreatedAt);
            builder.Property(i => i.LostOrFoundDate)
                .HasColumnType("date");
            builder.Property(i => i.IsResolved);
            builder.Property(i => i.UserID);




             }

    }
}