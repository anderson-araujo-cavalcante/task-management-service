using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces.Services
{
    public interface IProjectTaskService : IServiceBase<ProjectTask>
    {
        Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(int id);
        Task AddAsync(ProjectTask projectTask, int lastUpdateUser);
    }
}
