using Microsoft.EntityFrameworkCore;
using TaskManagement.Data.Context;

namespace TaskManagement.API.Configuration
{
    public static class DatabaseManagementService
    {
        //public static void MigrationInitialisation(IApplicationBuilder app)
        //{
        //    using var serviceScope = app.ApplicationServices.CreateScope();
        //    var service = serviceScope?.ServiceProvider?.GetService<TaskManagementContext>();
        //    if (service == null) throw new ArgumentNullException(nameof(serviceScope));

        //    service.Database.Migrate();
        //}

        public static void MigrationInitialisation(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var service = serviceScope?.ServiceProvider?.GetService<TaskManagementContext>();
            if (service == null) throw new InvalidOperationException(nameof(serviceScope));

            service.Database.Migrate();
        }
    }
}
