using LostAndFound.DbContexts;
using LostAndFound.Models;

namespace LostAndFound.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LostAndFoundDbContext _context;

        public IGenericRepository<Claim> Claims { get; }
        public IGenericRepository<Item> Items { get; }
        public IGenericRepository<User> Users { get; }

        public UnitOfWork(LostAndFoundDbContext context)
        {
            _context = context;
            Claims = new GenericRepository<Claim>(_context);
            Items = new GenericRepository<Item>(_context);
            Users = new GenericRepository<User>(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
