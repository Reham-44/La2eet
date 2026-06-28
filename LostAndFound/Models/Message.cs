using System;
using System.Collections.Generic;

namespace LostAndFound.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int ItemId { get; set; }

        public User Sender { get; set; } = null!;
        public User Receiver { get; set; } = null!;
        public Item Item { get; set; } = null!;
    }
}
