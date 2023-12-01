using FluentAssertions;
using Moq;
using TaskManagement.Data.Context;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Domain.Services;

namespace TaskManagement.UnitTests.Services
{
    public class ServiceBaseTests
    {
        #region Fields

        private readonly Mock<IRepositoryBase<TaskManagementContext>> _mockRepository;

        #endregion Fields

        #region Ctor

        public ServiceBaseTests()
        {
            _mockRepository = new Mock<IRepositoryBase<TaskManagementContext>>();
        }

        #endregion Ctor

        [Fact]
        public void Constructor_Initialize_ThrowArgumentNullException()
        {
            // Arrange

            // Act
            Action actionRepositoryNull = () => Mock.Of<ServiceBase<Project>>(null, _mockRepository.Behavior);

            // Assert
            actionRepositoryNull.Should().Throw<ArgumentNullException>();
        }
    }
}
