using Bogus;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Domain.Interfaces.Services;
using TaskManagement.Domain.Services;

namespace TaskManagement.UnitTests.Services
{
    public class ProjectTaskServiceTests
    {
        private readonly IProjectTaskService _service;
        private readonly Mock<IProjectTaskRepository> _repository;
        private readonly Mock<IHistoricRepository> _historicRepository;

        private static readonly Faker Faker = new();

        public ProjectTaskServiceTests()
        {
            _repository = new Mock<IProjectTaskRepository>();
            _historicRepository = new Mock<IHistoricRepository>();

            _service = new ProjectTaskService(_repository.Object, _historicRepository.Object);
        }

        [Fact]
        public async Task AddTaskShouldHavePriorityAssigned()
        {
            /// Arrange
            var task = new ProjectTask();
            var lastUpdateUser = 1;

            /// Act

            /// Assert
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _service.AddAsync(task, lastUpdateUser));
            Assert.Equal("A tarefa deve ter uma prioridade atribuída (baixa, média, alta).", exception.Message);
        }

        [Fact]
        public void ShouldDispose()
        {
            //// Act
            _service.Dispose();

            /// Assert
            _repository.Verify(x => x.Dispose(), Times.Once);
            _historicRepository.Verify(x => x.Dispose(), Times.Once);
        }

        [Fact]
        public async Task ShouldVerifyCreateProjectTask()
        {
            /// Arrange
            var task = new ProjectTask
            {
                ProjectId = Faker.Random.Int(1, 10),
                Description = "description",
                ExpirationDate = DateTime.UtcNow,
                Status = Domain.Enuns.TaskStatus.Completed,
                TaskPriority = Domain.Enuns.TaskPriority.Medium,
                Title = "title"
            };
            var lastUpdateUser = 1;

            //// Act
            await _service.AddAsync(task, lastUpdateUser);

            /// Assert
            _repository.Verify(x => x.AddAsync(It.IsAny<ProjectTask>()), Times.Once);
            _historicRepository.Verify(x => x.AddRangeAsync(It.IsAny<IEnumerable<Historic>>()), Times.Once);
        }

        [Fact]
        public async Task ShouldVerifyUpdateProjectTask()
        {
            /// Arrange
            var task = new ProjectTask
            {
                Id = Faker.Random.Int(1, 10),
                ProjectId = Faker.Random.Int(1, 10),
                Description = "description",
                ExpirationDate = DateTime.UtcNow,
                Status = Domain.Enuns.TaskStatus.Completed,
                TaskPriority = Domain.Enuns.TaskPriority.Medium,
                Title = "title"
            };
            var lastUpdateUser = 1;

            _repository.Setup(x => x.GetByIdAsync(task.Id)).ReturnsAsync(task);

            //// Act
            await _service.UpdateAsync(task, lastUpdateUser);

            /// Assert
            _repository.Verify(x => x.UpdateAsync(It.IsAny<ProjectTask>()), Times.Once);
            _repository.Verify(x => x.CountAsync(It.IsAny<Expression<Func<ProjectTask, bool>>>()), Times.Once);
            _historicRepository.Verify(x => x.AddRangeAsync(It.IsAny<IEnumerable<Historic>>()), Times.Once);
        }

        [Fact]
        public async Task GetPerformanceShouldValidateAccess()
        {
            /// Arrange
            string userId = "suporte";
            int lastDays = 30;

            /// Act
            var result = async () => await _service.GetTaskPerformanceAsync(userId, lastDays);

            /// Assert
            result.Should().ThrowExactlyAsync<Exception>().WithMessage("Acesso restrito.");
        }

        [Fact]
        public async Task GetPerformanceShould()
        {
            /// Arrange
            string userId = "admin";
            int lastDays = 30;
            var historic = CreateHistorictList();

            _historicRepository.Setup(x => x.GetCompletedTasks(lastDays)).ReturnsAsync(historic);

            /// Act
            var result = await _service.GetTaskPerformanceAsync(userId, lastDays);

            /// Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().BeEquivalentTo(new[] { new { UserId = 1, TotalTaskCompleteds = 2 }, new { UserId = 2, TotalTaskCompleteds = 1 }, new { UserId = 3, TotalTaskCompleteds = 4 } });
        }

        private IEnumerable<Historic> CreateHistorictList()
        {
            var mock = new List<Historic>()
            {
                new Historic{ UserId= 1, ProjectTaskId=1},
                new Historic{ UserId= 1, ProjectTaskId=2},
                new Historic{ UserId= 2, ProjectTaskId=3},
                new Historic{ UserId= 3, ProjectTaskId=4},
                new Historic{ UserId= 3, ProjectTaskId=5},
                new Historic{ UserId= 3, ProjectTaskId=6},
                new Historic{ UserId= 3, ProjectTaskId=7},
            };

            return mock;
        }
    }
}
