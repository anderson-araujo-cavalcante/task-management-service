using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Data.Context;
using TaskManagement.Data.Repositories;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Domain.Services;

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
