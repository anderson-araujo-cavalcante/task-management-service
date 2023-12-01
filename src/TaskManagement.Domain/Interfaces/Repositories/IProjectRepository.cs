using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces.Repositories
{
    public interface IProjectRepository : IRepositoryBase<Project>
    {
        Task<Project> GetByIdAsync(int id);
    }
}
