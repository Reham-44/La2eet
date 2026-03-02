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
    internal class MessageConfiguration: IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {

            builder.ToTable("Messages");
            builder.HasKey(m => m.MessageId);

            builder.Property(m => m.Content)
                .HasMaxLength(255);

            builder.Property(m => m.SentAt);
            builder.Property(m => m.IsRead);
            builder.Property(m => m.SenderId);
            builder.Property(m => m.ReceiverId);
            builder.Property(m => m.ItemId);




        }
    }
}
