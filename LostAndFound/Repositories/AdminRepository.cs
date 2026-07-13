using System.Collections.Generic;
using System.Linq;
using LostAndFound.DbContexts;
using LostAndFound.Enums;
using LostAndFound.Models;

namespace LostAndFound.Repositories
{
    public class AdminRepository
    {
        private readonly LostAndFoundDbContext context;

        public AdminRepository(LostAndFoundDbContext _context)
        {
            context = _context;
        }

        
        public int GetTotalReportsCount() => context.Items.Count();
        public int GetReportsCountByType(ItemType type) => context.Items.Count(i => i.Status == type);
        public int GetActiveClaimsCount() => context.Claims.Count();

        public List<Item> GetRecentItems(int count)
        {
            return context.Items
                   .OrderByDescending(i => i.CreatedAt)
                   .Take(count)
                   .ToList();
        }
        public int GetReturnedItemsCount() {
            return context.Items.Count(i => i.IsResolved == true);
        }
        public List<Item> GetRecentReturnedItems(int count)
        {
            return context.Items
            .Where(i => i.IsResolved == true)
           .OrderByDescending(i => i.CreatedAt)
           .Take(count)
           .ToList();
        }

        public Dictionary<string, int> GetCitySummary(int count)
        {
            return context.Items
                   .GroupBy(i => i.City)
                   .Select(g => new { CityName = g.Key.ToString(), Count = g.Count() })
                   .OrderByDescending(x => x.Count)
                   .Take(count)
                   .ToDictionary(k => k.CityName, v => v.Count);
        }

        
        public Item? GetItemById(int id) => context.Items.Find(id);

        public User? GetUserById(int id) => context.Users.Find(id);

        public List<Item> GetAllItems() => context.Items.ToList();

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}