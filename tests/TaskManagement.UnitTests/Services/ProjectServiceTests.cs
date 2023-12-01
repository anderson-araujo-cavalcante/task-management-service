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
            //arrange
            var projects = CreateProjectList();
            _repository.Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<Project, bool>>>()))
                .ReturnsAsync(projects);

            //act
            var result = await _service.GetByUserIdAsync(1);

            //assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(projects.Count());
            result.Should().BeEquivalentTo(projects);
        }

        [Fact]
        public async Task GetAll_Return_Empty()
        {
            //arrange
            var projects = Enumerable.Empty<Project>();
            _repository.Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<Project, bool>>>()))
                .ReturnsAsync(projects);

            //act
            var result = await _service.GetByUserIdAsync(1);

            //assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        private IEnumerable<Project> CreateProjectList()
        {
            var mock = new List<Project>();
            var userId = 1;
            for (int i = 0; i < 100; i++)
            {
                var faker = new Faker<Project>()
                    .RuleFor(x => x.Id, f =>i)
                    .RuleFor(x => x.CreatedAt, DateTime.Now.AddDays(i))
                    .RuleFor(x => x.UserId, f => userId)
                    .RuleFor(x => x.Name, f => f.Random.String(20));

                mock.Add(faker);
            }
            return mock;
        }
    }
}
