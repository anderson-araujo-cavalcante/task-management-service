using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManagement.Data.Context;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Data.Repositories
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected TaskManagementContext _context;
        protected DbSet<TEntity> dbSet;

        #region Ctor
        public RepositoryBase(TaskManagementContext dbContext)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            dbSet = _context.Set<TEntity>();
        }
        #endregion        

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.AsNoTracking().CountAsync(predicate);
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task RemoveAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
