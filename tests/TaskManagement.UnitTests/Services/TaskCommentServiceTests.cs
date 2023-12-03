using Moq;
using System.Linq.Expressions;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Domain.Interfaces.Services;
using TaskManagement.Domain.Services;

namespace TaskManagement.UnitTests.Services
{
    public class TaskCommentServiceTests
    {
        private readonly ITaskCommentService _service;
        private readonly Mock<ITaskCommentRepository> _repository;
        private readonly Mock<IHistoricRepository> _historicRepository;

        public TaskCommentServiceTests()
        {
            _repository = new Mock<ITaskCommentRepository>();
            _historicRepository = new Mock<IHistoricRepository>();

            _service = new TaskCommentService(_repository.Object, _historicRepository.Object);
        }

        [Fact]
        public async Task ShouldVerifyCreateComment()
        {
            /// Arrange
            var task = new TaskComment
            {
                Comment = "Test",
                ProjectTaskId = 1,
                UpdateDate = DateTime.Now
            };
            var lastUpdateUser = 1;

            //// Act
            await _service.AddAsync(task, lastUpdateUser);

            /// Assert
            _repository.Verify(x => x.AddAsync(It.IsAny<TaskComment>()), Times.Once);
            _historicRepository.Verify(x => x.AddRangeAsync(It.IsAny<IEnumerable<Historic>>()), Times.Once);
        }

        [Fact]
        public async Task ShouldVerifyUpdateTaskComment()
        {
            /// Arrange
            var task = new TaskComment
            {
                Id = 1,
                Comment = "Test",
                ProjectTaskId = 1,
                UpdateDate = DateTime.Now
            };
            var lastUpdateUser = 1;

            _repository.Setup(x => x.GetByIdAsync(task.Id)).ReturnsAsync(task);

            //// Act
            await _service.UpdateAsync(task, lastUpdateUser);

            /// Assert
            _repository.Verify(x => x.UpdateAsync(It.IsAny<TaskComment>()), Times.Once);
            _historicRepository.Verify(x => x.AddRangeAsync(It.IsAny<IEnumerable<Historic>>()), Times.Once);
        }

        [Fact]
        public async Task VerifyGetByTaskId()
        {
            /// Arrange            
            var taskId = 1;

            //// Act
            await _service.GetByTaskIdAsync(taskId);

            /// Assert
            _repository.Verify(x => x.GetAllAsync(It.IsAny<Expression<Func<TaskComment, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task VerifyGetByProjectId()
        {
            /// Arrange            
            var taskId = 1;

            //// Act
            await _service.GetByProjectIdAsync(taskId);

            /// Assert
            _repository.Verify(x => x.GetAllAsync(It.IsAny<Expression<Func<TaskComment, bool>>>()), Times.Once);
        }
    }
}
