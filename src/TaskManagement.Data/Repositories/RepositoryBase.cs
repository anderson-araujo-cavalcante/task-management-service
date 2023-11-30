using Microsoft.EntityFrameworkCore;
using TaskManagement.Data.Context;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Data.Repositories
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class, IEntity
    {
        protected TaskManagementContext _context;
        protected DbSet<TEntity> dbSet;

        #region Ctor
        public RepositoryBase(TaskManagementContext dbContext)
        {
            _context = dbContext;
            dbSet = _context.Set<TEntity>();
        }
        #endregion 

        public virtual async Task AddAsync(TEntity entity)
        {
            entity.CreatedAt = DateTime.Now;
            await dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.AsNoTracking().ToListAsync();
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
