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
    internal class ClaimConfiguration : IEntityTypeConfiguration<Claim>
    {
        public void Configure(EntityTypeBuilder<Claim> builder)
        {
            builder.ToTable("Claims");
            builder.HasKey(c=>c.ClaimId);
            builder.Property(c=>c.Status)
                .HasMaxLength(20);
            builder.Property(c => c.CreatedAt);
            builder.Property(c => c.VerificationAnswer)
                .HasMaxLength(255);
            builder.Property(c => c.UserId);
            builder.Property(c => c.ItemId);


        }

    }
}
