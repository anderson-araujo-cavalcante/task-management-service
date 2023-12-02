using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces.Services
{
    public interface ITaskCommentService : IServiceBase<TaskComment>
    {
        Task AddAsync(TaskComment comment, int lastUpdateUser);
        Task UpdateAsync(TaskComment comment, int lastUpdateUser);
        Task<IEnumerable<TaskComment>> GetByTaskIdAsync(int id);
        Task<IEnumerable<TaskComment>> GetByProjectIdAsync(int id);
    }
}
