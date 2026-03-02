using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostAndFound.models
{
    internal class VerificationQuestion
    {
        public int QId { get; set; }
        public string QuestionText { get; set; }
        public string ExpectedAnswerHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ItemId { get; set; }



    }
}
