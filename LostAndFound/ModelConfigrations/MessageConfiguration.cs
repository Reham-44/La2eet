using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LostAndFound.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LostAndFound.ModelConfigrations
{
    internal class MessageConfiguration: IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {

            builder.ToTable("Messages");
            builder.HasKey(m => m.MessageId);

            builder.Property(m => m.Content)
                .HasMaxLength(255);

            builder.Property(m => m.SentAt)
                       .HasDefaultValueSql("GETDATE()");

            builder.HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Item)
                .WithMany(i => i.Messages)
                .HasForeignKey(m => m.ItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
