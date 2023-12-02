using TaskManagement.Data.Context;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Data.Repositories
{
    public class TaskCommentRepository : RepositoryBase<TaskComment>, ITaskCommentRepository
    {
        public TaskCommentRepository(TaskManagementContext dbContext) : base(dbContext)
        {
        }
    }
}
