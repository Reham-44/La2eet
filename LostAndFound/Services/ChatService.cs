using LostAndFound.Models;
using LostAndFound.Models.ViewModels;
using LostAndFound.Repositories;

namespace LostAndFound.Services
{
    public class ChatService
    {
        private readonly ChatRepository repo;

        public ChatService(ChatRepository _repo)
        {
            repo = _repo;
        }

        //public ChatPageVM GetMessagesPage(int currentUserId, int itemId, int receiverId)
        //{
        //    var chats = repo.GetChats(currentUserId);

        //    if (!chats.Any() && itemId != 0 && receiverId != 0)
        //    {
        //        chats.Add(new ChatVM
        //        {
        //            ItemId = itemId,

        //            OtherUserId = receiverId,

        //            OtherUserName = repo.GetUserName(receiverId),

        //            ItemTitle = repo.GetItemTitle(itemId),

        //            Messages = new List<MessageVM>()
        //        });
        //    }

        public ChatPageVM GetMessagesPage(
          int currentUserId,
          int? itemId,
          int? receiverId)
        {
            var chats = repo.GetChats(currentUserId);

            if (itemId.HasValue && receiverId.HasValue)
            {
                var chat = chats.FirstOrDefault(c =>
                    c.ItemId == itemId.Value &&
                    c.OtherUserId == receiverId.Value);

                if (chat == null)
                {
                    chat = new ChatVM
                    {
                        ItemId = itemId.Value,
                        OtherUserId = receiverId.Value,
                        OtherUserName = repo.GetUserName(receiverId.Value),
                        ItemTitle = repo.GetItemTitle(itemId.Value),
                        Messages = new List<MessageVM>()
                    };

                    chats.Insert(0, chat);
                }
            }

            return new ChatPageVM
            {
                Chats = chats,
                CurrentUserId = currentUserId
            };       
        }
        public (int senderId, int receiverId)? GetChatUsers(int itemId)
        {
            return repo.GetChatUsers(itemId);
        }
        public bool MarkItemAsReturned(int itemId)
        {

            return repo.MarkItemAsReturned(itemId);
        }

    }
}