namespace LostAndFound.Models.ViewModels
{
    public class MessageVM
    {
      
            public int SenderId { get; set; }

            public string Content { get; set; }

            public DateTime SentAt { get; set; }
        public string SentTime =>
    SentAt.ToLocalTime().ToString("hh:mm tt");
        }
    
}
