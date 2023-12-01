using TaskManagement.Data.Context;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Data.Repositories
{
    public class HistoricRepository : IHistoricRepository
    {
        private readonly TaskManagementContext _context;
       // private readonly IHistoricRepository _historicRepository;
        public HistoricRepository(TaskManagementContext dbContext)
        {
            //_historicRepository = historicRepository ?? throw new ArgumentNullException(nameof(historicRepository));
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddRangeAsync(IEnumerable<Historic> historics)
        {
            await _context.Set<Historic>().AddRangeAsync(historics);
        }
    }
}
