using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostAndFound.models
{
    internal class Message
    {
        public int MessageId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int ItemId { get; set; }


    }
}
