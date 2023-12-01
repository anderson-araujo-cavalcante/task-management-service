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

        public async Task<IEnumerable<Project>> GetByUserIdAsync(int id)
            => await _repository.GetAllAsync(x => x.UserId == id);
    }
}
