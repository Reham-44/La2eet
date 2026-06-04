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
    internal class VerificationQuestionConfiguration:IEntityTypeConfiguration<VerificationQuestion>
    {
        public void Configure(EntityTypeBuilder<VerificationQuestion> builder)
        {
            builder.ToTable("VerificationQuestions");
            builder.HasKey(v => v.QId);
            builder.Property(v => v.QuestionText)
                .HasMaxLength(250);
            builder.Property(v => v.ExpectedAnswerHash)
            .HasMaxLength(255);
            builder.Property(v => v.CreatedAt); ;
            builder.Property(v => v.ItemId);





        }
    }
}
