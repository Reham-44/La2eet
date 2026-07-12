using LostAndFound.DbContexts;
using LostAndFound.Enums;
using LostAndFound.Models;
using Microsoft.EntityFrameworkCore;

namespace LostAndFound.Repositories
{
    public class ItemRepository
    {
        private readonly LostAndFoundDbContext context;

        public ItemRepository(LostAndFoundDbContext _context)
        {
            context = _context;
        }

        public IQueryable<Item> GetApprovedItemsQuery()
        {
            return context.Items
                .Where(i => i.ReportStatus == ReportStatus.Approved && i.User.IsBanned == false)
                .AsQueryable();
        }

        public Item? GetById(int id, bool includeUser = false)
        {
            var query = context.Items.AsQueryable();

            if (includeUser)
                query = query.Include(i => i.User);

            return query.FirstOrDefault(i => i.ItemId == id);
        }

        public void Add(Item item)
        {
            context.Items.Add(item);
        }

        public void AddVerificationQuestions(IEnumerable<VerificationQuestion> questions)
        {
            context.VerificationQuestions.AddRange(questions);
        }

        public bool HasUserClaimedItem(int itemId, int userId)
        {
            return context.Claims.Any(c => c.ItemId == itemId && c.UserId == userId);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}