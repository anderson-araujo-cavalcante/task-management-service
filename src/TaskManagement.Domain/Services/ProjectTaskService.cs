﻿using System.Globalization;
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
            _historicRepository = historicRepository ?? throw new ArgumentNullException(nameof(historicRepository)); ;
        }

        public async Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(int id)
            => await _repository.GetAllAsync(x => x.ProjectId == id);

        public async Task UpdateAsync(ProjectTask projectTask, int lastUpdateUser)
        {
            var projectTaskEdit = await _repository.GetByIdAsync(projectTask.Id);

            if (projectTaskEdit.Status != projectTask.Status) throw new Exception("Não é permitido alterar a prioridade de uma tarefa depois que ela foi criada.");

           var historics = projectTask.BuildHistoric(lastUpdateUser: lastUpdateUser, projectTaskEdit);

            projectTaskEdit.Title = projectTask.Title;
            projectTaskEdit.Description = projectTask.Description;
            projectTaskEdit.ExpirationDate = projectTask.ExpirationDate;
            projectTaskEdit.ProjectId = projectTask.ProjectId;        
            projectTask.TaskPriority = projectTask.TaskPriority;

            await _repository.UpdateAsync(projectTask);
            await _historicRepository.AddRangeAsync(historics);
        }

        public async Task AddAsync(ProjectTask projectTask, int lastUpdateUser)
        {
            if (projectTask.Status == 0) throw new Exception("A tarefa deve ter uma prioridade atribuída (baixa, média, alta).");

            var totalProjects = await _repository.CountAsync(_ => _.ProjectId == projectTask.ProjectId);

            var taskLimite = 20;
            if(totalProjects >= taskLimite) throw new Exception("Limite de tarefas atingido para este projeto.");
            
            await _repository.AddAsync(projectTask);
            await _historicRepository.AddRangeAsync(projectTask.BuildHistoric(lastUpdateUser: lastUpdateUser));
        }
    }
}
