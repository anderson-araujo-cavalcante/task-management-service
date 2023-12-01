using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces.Services
{
    public interface IProjectService : IServiceBase<Project>
    {
        Task<IEnumerable<Project>> GetByUserIdAsync(int id);
        Task DeleteAsync(int id);
    }
}
