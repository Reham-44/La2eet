namespace LostAndFound.Models.ViewModels
{

        public class ChatVM
        {
            public int ItemId { get; set; }

            public int OtherUserId { get; set; }

            public string OtherUserName { get; set; }

            public string ItemTitle { get; set; }

            public string LastMessage { get; set; }

            public DateTime LastMessageTime { get; set; }
        public string LastMessageTimeText =>
            LastMessageTime.ToLocalTime().ToString("hh:mm tt");
        public bool IsClosed { get; set; }
        public bool CanMarkReturned { get; set; }
        public List<MessageVM> Messages { get; set; } = new();
        }
    
}
