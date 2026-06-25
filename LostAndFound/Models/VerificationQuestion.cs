using System;
using System.Collections.Generic;

namespace LostAndFound.Models
{
    public class VerificationQuestion
    {
        public int QId { get; set; }
        public string QuestionText { get; set; }
        public string ExpectedAnswerHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ItemId { get; set; }

        public Item Item { get; set; } = null!;
    }
}
