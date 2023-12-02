using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Responses;

namespace TaskManagement.Domain.Interfaces.Services
{
    public interface IProjectTaskService : IServiceBase<ProjectTask>
    {
        Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(int id);
        Task AddAsync(ProjectTask projectTask, int lastUpdateUser);
        Task UpdateAsync(ProjectTask projectTask, int lastUpdateUser);
        Task<IEnumerable<PerformanceResponse>> GetTaskPerformanceAsync(string userId, int lastDays);
    }
}
