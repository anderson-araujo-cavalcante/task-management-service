using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;


namespace TaskManagement.Data.Context
{
    public class TaskManagementContext : DbContext
    {
        public TaskManagementContext(DbContextOptions<TaskManagementContext> options) : base(options)
        { }

        public DbSet<ProjectTask> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Historic> Historics { get; set; }
    }
}
