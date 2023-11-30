using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Domain.Interfaces.Services;

namespace TaskManagement.Domain.Services
{
    public abstract class ServiceBase<TEntity> : IDisposable, IServiceBase<TEntity> where TEntity : class
    {
        protected readonly IRepositoryBase<TEntity> _repository;

        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(TEntity obj)
        {
            await _repository.AddAsync(obj);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task UpdateAsync(TEntity obj)
        {
            await _repository.UpdateAsync(obj);
        }

        public async Task RemoveAsync(int id)
        {
            await _repository.RemoveAsync(id);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
