using Microsoft.EntityFrameworkCore;
using TaskManagement.Data.Context;

namespace TaskManagement.API.Configuration
{
    public static class DatabaseManagementService
    {
        public static void MigrationInitialisation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                serviceScope.ServiceProvider.GetService<TaskManagementContext>().Database.Migrate();
            }
        }
    }
}
