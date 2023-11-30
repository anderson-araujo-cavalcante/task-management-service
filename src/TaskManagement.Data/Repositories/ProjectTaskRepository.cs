using TaskManagement.Data.Context;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Data.Repositories
{
    public class ProjectTaskRepository : RepositoryBase<ProjectTask>, IProjectTaskRepository
    {
        public ProjectTaskRepository(TaskManagementContext dbContext) : base(dbContext)
        {
        }
    }
}
