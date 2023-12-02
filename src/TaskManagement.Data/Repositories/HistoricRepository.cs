using TaskManagement.Data.Context;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Data.Repositories
{
    public class HistoricRepository : IHistoricRepository
    {
        private readonly TaskManagementContext _context;
        public HistoricRepository(TaskManagementContext dbContext)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddRangeAsync(IEnumerable<Historic> historics)
        {
            await _context.Set<Historic>().AddRangeAsync(historics);
        }
    }
}
