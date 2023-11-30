using TaskManagement.Data.Context;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Domain.Interfaces.Services;

namespace TaskManagement.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<TaskManagementContext>();

            services.AddScoped<IProjectService, IProjectService>();

            services.AddScoped<IProjectTaskRepository, IProjectTaskRepository>();

            return services;
        }
    }
}
