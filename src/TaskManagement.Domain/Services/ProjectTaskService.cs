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

        public async Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(int id)
            => await _repository.GetAllAsync(x => x.ProjectId == id);

        public async Task UpdateAsync(ProjectTask projectTask)
        {
            var projectTaskEdit = await _repository.GetByIdAsync(projectTask.Id);

            if (projectTaskEdit.Status != projectTask.Status) throw new Exception("Não é permitido alterar a prioridade de uma tarefa depois que ela foi criada.");

            projectTaskEdit.Title = projectTask.Title;
            projectTaskEdit.Description = projectTask.Description;
            projectTaskEdit.ExpirationDate = projectTask.ExpirationDate;
            projectTaskEdit.ProjectId = projectTask.ProjectId;            

            await _repository.UpdateAsync(projectTask);
        }

        public async Task AddAsync(ProjectTask projectTask)
        {
            if (projectTask.Status == 0) throw new Exception("A tarefa deve ter uma prioridade atribuída (baixa, média, alta).");

            var totalProjects = await _repository.CountAsync(_ => _.ProjectId == projectTask.ProjectId);

            var taskLimite = 20;
            if(totalProjects >= taskLimite) throw new Exception("Limite de tarefas atingido para este projeto.");

            await _repository.AddAsync(projectTask);
        }
    }
}
