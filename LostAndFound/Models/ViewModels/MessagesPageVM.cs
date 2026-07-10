namespace LostAndFound.Models.ViewModels
{
    public class ChatPageVM
    {
        public int CurrentUserId { get; set; }

        public List<ChatVM> Chats { get; set; } = new();

        public ChatVM? SelectedChat { get; set; }
    }
}
