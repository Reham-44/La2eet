using LostAndFound.Models;

namespace LostAndFound.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<Claim> Claims { get; }
        IGenericRepository<Item> Items { get; }
        IGenericRepository<User> Users { get; }

        Task<int> CompleteAsync(); 
    }
}
