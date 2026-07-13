using System.Linq.Expressions;

namespace LostAndFound.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();

        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
