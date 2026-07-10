using LostAndFound.DbContexts;
using LostAndFound.Models;
using Microsoft.AspNetCore.SignalR;

namespace LostAndFound.Hubs
{
    public class ChatHub:Hub
    {
        LostAndFoundDbContext context;
        public ChatHub(LostAndFoundDbContext _context)
        {
            context = _context;
        }
        public async Task SendMessage(
            int senderId,
            int receiverId,
            int itemId,
            string message)
        {
            var item = context.Items.FirstOrDefault(i => i.ItemId == itemId);

            if (item == null || item.IsResolved)
                return;

            var msg = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                ItemId = itemId,
                Content = message,
                SentAt = DateTime.UtcNow
            };

            context.Messages.Add(msg);
            await context.SaveChangesAsync();

            await Clients.All.SendAsync(
                "ReceiveMessage",
                senderId,
                receiverId,
                itemId,
                message);
        }
    }
}
