using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Domain.Interfaces.Services;

namespace TaskManagement.Domain.Services
{
    public class ProjectService : ServiceBase<Project>, IProjectService
    {
        public ProjectService(IProjectRepository projectRepository) : base(projectRepository)
        {
        }
    }
}
