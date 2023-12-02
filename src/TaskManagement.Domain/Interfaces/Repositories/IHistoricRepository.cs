using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces.Repositories
{
    public interface IHistoricRepository : IRepositoryBase<Historic>
    {
        Task AddRangeAsync(IEnumerable<Historic> historics);
        Task<IEnumerable<Historic>> GetCompletedTasks(int lastDays);
    }
}
