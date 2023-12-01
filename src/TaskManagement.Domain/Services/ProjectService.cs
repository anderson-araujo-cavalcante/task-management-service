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

        public async Task DeleteAsync(int id)
        {
            var project = await _repository.GetByIdAsync(id);
            if (project.Tasks.Any(_ => _.Status != Enuns.TaskStatus.Completed)) throw new Exception("Projeto não pode ser removido, ainda há tarefas pendentes, conclua ou remova as tarefas antes de continuar.");

            await DeleteAsync(id);
        }


    }
}
