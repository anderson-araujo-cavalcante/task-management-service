using System.Globalization;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Domain.Interfaces.Services;
using TaskManagement.Domain.Extensions;

namespace TaskManagement.Domain.Services
{
    public class ProjectTaskService : ServiceBase<ProjectTask>, IProjectTaskService
    {
        private readonly IHistoricRepository _historicRepository;
        public ProjectTaskService(IProjectTaskRepository projectTaskRepository,
            IHistoricRepository historicRepository) 
            : base(projectTaskRepository)
        {
            _historicRepository = historicRepository ?? throw new ArgumentNullException(nameof(historicRepository)); 
        }

        public async Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(int id)
            => await _repository.GetAllAsync(x => x.ProjectId == id);

        public async Task UpdateAsync(ProjectTask projectTask, int lastUpdateUser)
        {
            await ValidateTaskLimite(projectTask);

            var projectTaskEdit = await _repository.GetByIdAsync(projectTask.Id);
            if (projectTaskEdit.TaskPriority != projectTask.TaskPriority) throw new Exception("Não é permitido alterar a prioridade de uma tarefa depois que ela foi criada.");

            await _repository.UpdateAsync(projectTask);

            var historics = Historic2<ProjectTask>.Build(newData: projectTask, oldData: projectTaskEdit, lastUpdateUser, projectTask.Id, x => x.Name != "Id" && x.Name != "Project");
            await _historicRepository.AddRangeAsync(historics);
        }

        public async Task AddAsync(ProjectTask projectTask, int lastUpdateUser)
        {
            if (projectTask.TaskPriority == 0) throw new Exception("A tarefa deve ter uma prioridade atribuída (baixa, média, alta).");
           
            await ValidateTaskLimite(projectTask);

            await _repository.AddAsync(projectTask);

            var historics = Historic2<ProjectTask>.Build(newData: projectTask, oldData: null, lastUpdateUser, projectTask.Id, x => x.Name != "Id" && x.Name != "Project" && x.Name != "Comments");
            await _historicRepository.AddRangeAsync(historics);
        }

        private async Task ValidateTaskLimite(ProjectTask projectTask)
        {
            var totalProjects = await _repository.CountAsync(_ => _.ProjectId == projectTask.ProjectId);
            var taskLimite = 20;

            if (totalProjects >= taskLimite) throw new Exception("Limite de tarefas atingido para este projeto.");
        }
    }
}
