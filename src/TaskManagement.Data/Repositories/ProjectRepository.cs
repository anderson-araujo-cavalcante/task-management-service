using Microsoft.EntityFrameworkCore;
using TaskManagement.Data.Context;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Data.Repositories
{
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(TaskManagementContext dbContext) : base(dbContext)
        {
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await dbSet.AsNoTracking().Include(_ => _.Tasks).FirstOrDefaultAsync(_ => _.Id == id);
        }
    }
}
