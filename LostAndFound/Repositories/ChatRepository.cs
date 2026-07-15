using LostAndFound.DbContexts;
using LostAndFound.Models;
using LostAndFound.Models.ViewModels;

namespace LostAndFound.Repositories
{
    public class ChatRepository
    {
        private readonly LostAndFoundDbContext context;

        public ChatRepository(LostAndFoundDbContext _context)
        {
            context = _context;
        }

        public List<ChatVM> GetChats(int currentUserId)
        {
          
            return context.Messages
                .Where(m => m.SenderId == currentUserId || m.ReceiverId == currentUserId)
                .GroupBy(m => new
                {
                    m.ItemId,
                    OtherUserId = m.SenderId == currentUserId
                        ? m.ReceiverId
                        : m.SenderId
                })
                .Select(g => new ChatVM
                {
                    ItemId = g.Key.ItemId,

                    OtherUserId = g.Key.OtherUserId,

                    OtherUserName = context.Users
                        .Where(u => u.Id == g.Key.OtherUserId)
    .Select(u => u.FullName)
                        .FirstOrDefault(),

                    ItemTitle = context.Items
                        .Where(i => i.ItemId == g.Key.ItemId)
                        .Select(i => i.Title)
                        .FirstOrDefault(),
                    CanMarkReturned = context.Items
        .Where(i => i.ItemId == g.Key.ItemId)
        .Select(i =>
            (i.Status == Enums.ItemType.Lost && i.UserId == currentUserId)
            ||
            (i.Status == Enums.ItemType.Found &&
                context.Claims.Any(g =>
                    g.ItemId == i.ItemId &&
                    g.UserId == currentUserId &&
                    g.ClaimStatus == Enums.ClaimStatus.Approved)))
        .FirstOrDefault(),
                    LastMessage = g
                        .OrderByDescending(x => x.SentAt)
                        .Select(x => x.Content)
                        .FirstOrDefault(),
                    IsClosed = context.Items
    .Where(i => i.ItemId == g.Key.ItemId)
    .Select(i => i.IsResolved)
    .FirstOrDefault(),
                    LastMessageTime = g.Max(x => x.SentAt),

                    Messages = g.OrderBy(x => x.SentAt)
            .Select(x => new MessageVM
            {
                SenderId = x.SenderId,
                Content = x.Content,
                SentAt = x.SentAt
            })
            .ToList()
                })
               .OrderByDescending(c => c.LastMessageTime)
                .ToList();
        }

        public string GetUserName(int userId)
        {
            return context.Users
                .Where(x => x.Id == userId)
                .Select(x => x.FullName)
                .First();
        }
        public string GetItemTitle(int itemId)
        {
            return context.Items
                .Where(x => x.ItemId == itemId)
                .Select(x => x.Title)
                .First();
        }
        public bool MarkItemAsReturned(int itemId)
        {
            var item = context.Items.FirstOrDefault(i => i.ItemId == itemId);

            if (item == null)
                return false;

            item.Status = Enums.ItemType.Returned;
            item.IsResolved = true;

            context.SaveChanges();

            return true;
        }
        public (int senderId, int receiverId)? GetChatUsers(int itemId)
        {
            var msg = context.Messages
                .Where(m => m.ItemId == itemId)
                .OrderByDescending(m => m.SentAt)
                .FirstOrDefault();

            if (msg == null)
                return null;

            return (msg.SenderId, msg.ReceiverId);
        }
    }
}