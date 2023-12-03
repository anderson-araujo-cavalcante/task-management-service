using FluentAssertions;
using Moq;
using TaskManagement.Data.Context;
using TaskManagement.Data.Repositories;
using TaskManagement.Domain.Entities;

namespace TaskManagement.UnitTests.Repositories
{
    public class RepositoryBaseTests
    {
        #region Fields

        private readonly Mock<TaskManagementContext> _taskManagementContext;

        #endregion Fields

        #region Ctor

        public RepositoryBaseTests()
        {
            _taskManagementContext = new Mock<TaskManagementContext>();
        }

        #endregion Ctor

        [Fact]
        public void Constructor_Initialize_ThrowArgumentNullException()
        {
            // Arrange

            // Act
            Action actionContextNull = () => Mock.Of<RepositoryBase<Project>>(null, _taskManagementContext.Behavior);

            // Assert
            actionContextNull.Should().Throw<ArgumentNullException>();
        }
    }
}
