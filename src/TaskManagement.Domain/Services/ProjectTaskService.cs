using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Domain.Interfaces.Services;

namespace TaskManagement.Domain.Services
{
    public class ProjectTaskService : ServiceBase<ProjectTask>, IProjectTaskService
    {
        public ProjectTaskService(IProjectTaskRepository projectTaskRepository) : base(projectTaskRepository)
        {
        }
    }
}
