using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces.Repositories
{
    public interface IHistoricRepository
    {
        Task AddRangeAsync(IEnumerable<Historic> historics);
    }
}
