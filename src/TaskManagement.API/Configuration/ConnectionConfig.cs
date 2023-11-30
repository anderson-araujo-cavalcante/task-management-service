using Microsoft.EntityFrameworkCore;
using TaskManagement.Data.Context;

namespace TaskManagement.API.Configuration
{
    public static class ConnectionConfig
    {
        public static WebApplicationBuilder AddDbContext(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContextPool<TaskManagementContext>(
            options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)
            ));

            return builder;
        }
    }
}
