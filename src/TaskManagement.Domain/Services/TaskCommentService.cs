using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Extensions;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Domain.Interfaces.Services;

namespace TaskManagement.Domain.Services
{
    public class TaskCommentService : ServiceBase<TaskComment>, ITaskCommentService
    {
        private readonly IHistoricRepository _historicRepository;
        public TaskCommentService(ITaskCommentRepository projectRepository,
                                                      IHistoricRepository historicRepository
            ) : base(projectRepository)
        {
            _historicRepository = historicRepository ?? throw new ArgumentNullException(nameof(historicRepository)); 
        }

        public async Task AddAsync(TaskComment comment, int lastUpdateUser)
        {
            comment.UpdateDate = DateTime.UtcNow;
            await _repository.AddAsync(comment);

            var historics = Historic2<TaskComment>.Build(newData: comment, oldData: null, lastUpdateUser, comment.ProjectTaskId, x => x.Name != "Id" && x.Name != "ProjectTask");
            await _historicRepository.AddRangeAsync(historics);
        }

        public async Task UpdateAsync(TaskComment comment, int lastUpdateUser)
        {
            var commentEdit = await _repository.GetByIdAsync(comment.Id);
            comment.UpdateDate = DateTime.UtcNow;
            await _repository.UpdateAsync(comment);

            var historics = Historic2<TaskComment>.Build(newData: comment, oldData: commentEdit, lastUpdateUser, comment.Id, x => x.Name != "Id" && x.Name != "ProjectTask");
            await _historicRepository.AddRangeAsync(historics);
        }

        public async Task<IEnumerable<TaskComment>> GetByTaskIdAsync(int id)
            => await _repository.GetAllAsync(x => x.ProjectTaskId == id);

        public async Task<IEnumerable<TaskComment>> GetByProjectIdAsync(int id)
            => await _repository.GetAllAsync(x => x.ProjectTask.Project.Id == id);
    }
}
