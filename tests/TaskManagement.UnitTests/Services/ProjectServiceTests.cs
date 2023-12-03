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
    public class ProjectServiceTests
    {
        private readonly IProjectService _service;
        private readonly Mock<IProjectRepository> _repository;

        public ProjectServiceTests()
        {
            _repository = new Mock<IProjectRepository>();
            _service = new ProjectService(_repository.Object);
        }

        [Fact]
        public async Task GetAll_Return_Data()
        {
            /// Arrange
            var projects = CreateProjectList();
            _repository.Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<Project, bool>>>()))
                .ReturnsAsync(projects);

            /// Act
            var result = await _service.GetByUserIdAsync(1);

            /// Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(projects.Count());
            result.Should().BeEquivalentTo(projects);
        }

        [Fact]
        public async Task GetAll_Return_Empty()
        {
            /// Arrange
            var projects = Enumerable.Empty<Project>();
            _repository.Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<Project, bool>>>()))
                .ReturnsAsync(projects);

            /// Act
            var result = await _service.GetByUserIdAsync(1);

            /// Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task RemoveProjectShouldValidatePendingTasks()
        {
            /// Arrange
            var taskPendente = new ProjectTask { ProjectId = 1 , Status = Domain.Enuns.TaskStatus.Pending };
            var taskCompleted = new ProjectTask { ProjectId = 1, Status = Domain.Enuns.TaskStatus.Completed };
            var project = new Project { Id = 1, Name = "", UserId = 1, Tasks = new List<ProjectTask> { taskPendente, taskCompleted } };
            
            _repository.Setup(x => x.GetByIdAsync(project.Id)).ReturnsAsync(project);

            /// Act
            var result = async () => await _service.DeleteAsync(project.Id);

            /// Assert
            result.Should().ThrowExactlyAsync<Exception>().WithMessage("Projeto não pode ser removido, ainda há tarefas pendentes, conclua ou remova as tarefas antes de continuar");
        }

        [Fact]
        public async Task RemoveProjectShouldNotExist()
        {
            /// Arrange

            /// Act
            var result = async () => await _service.DeleteAsync(1);

            /// Assert
            result.Should().ThrowExactlyAsync<Exception>().WithMessage("Projeto não existe.");
        }

        [Fact]
        public void ShouldDispose()
        {
            //// Act
            _service.Dispose();

            /// Assert
            _repository.Verify(x => x.Dispose(), Times.Once);
        }


        private IEnumerable<Project> CreateProjectList()
        {
            var mock = new List<Project>();
            var userId = 1;
            for (int i = 0; i < 100; i++)
            {
                var faker = new Faker<Project>()
                    .RuleFor(x => x.Id, f =>i)
                    .RuleFor(x => x.UserId, f => userId)
                    .RuleFor(x => x.Name, f => f.Random.String(20));

                mock.Add(faker);
            }
            return mock;
        }
    }
}
