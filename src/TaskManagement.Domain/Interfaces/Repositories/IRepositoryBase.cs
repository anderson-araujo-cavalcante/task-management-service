using System.Linq.Expressions;

namespace TaskManagement.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity obj);
        Task<TEntity> GetByIdAsync(int id);
       Task UpdateAsync(TEntity entity);
        Task RemoveAsync(int entity);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        void Dispose();
    }
}
