using Microsoft.EntityFrameworkCore;
using TaskManagement.Data.Context;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Data.Repositories
{
    public class HistoricRepository :  RepositoryBase<Historic>, IHistoricRepository
    {
        public HistoricRepository(TaskManagementContext dbContext) : base(dbContext)
        {
        }

        public async Task AddRangeAsync(IEnumerable<Historic> historics)
        {
            await dbSet.AddRangeAsync(historics);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Historic>> GetCompletedTasks(int lastDays)
        {
            var startDate = DateTime.Now.AddDays(-lastDays);
            var historic = await _context.Set<Historic>().AsNoTracking()
                                                                            .Where(_ => _.PropertyName == "Status"
                                                                                && _.UpdateDate >= startDate)
                                                                            .ToListAsync();

            return historic;
        }
    }
}
