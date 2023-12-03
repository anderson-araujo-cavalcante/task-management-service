using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Extensions;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Domain.Interfaces.Services;
using TaskManagement.Domain.Responses;

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

            var projectTaskEdit = await _repository.GetByIdAsync(projectTask.Id) ?? throw new Exception("Tarefa não existe.");

            if (projectTaskEdit.TaskPriority != projectTask.TaskPriority) throw new Exception("Não é permitido alterar a prioridade de uma tarefa depois que ela foi criada.");

            var historics = Historic2<ProjectTask>.Build(newData: projectTask, oldData: projectTaskEdit, lastUpdateUser, projectTask.Id, x => x.Name != "Id" && x.Name != "Project" && x.Name != "Comments");

            projectTaskEdit.Title = projectTask.Title;
            projectTaskEdit.Description = projectTask.Description;
            projectTaskEdit.ExpirationDate = projectTask.ExpirationDate;
            projectTaskEdit.ProjectId = projectTask.ProjectId;
            projectTaskEdit.TaskPriority = projectTask.TaskPriority;

            await _repository.UpdateAsync(projectTaskEdit);

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

        public async Task<IEnumerable<PerformanceResponse>> GetTaskPerformanceAsync(string userId, int lastDays)
        {
            if (userId != "admin") throw new Exception("Acesso restrito.");

            var historics = await _historicRepository.GetCompletedTasks(lastDays);

            var group = historics.GroupBy(x => x.UserId);

            var performance = group.Select(x => new PerformanceResponse()
            {
                UserId = x.Key,
                TotalTaskCompleteds = x.Count()
            });

            return performance;
        }


        #region IDisposable Members

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _repository?.Dispose();
                    _historicRepository?.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Members
    }
}
