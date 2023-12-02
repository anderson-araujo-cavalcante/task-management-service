using AutoMapper;
using TaskManagement.Domain.Profiles;

namespace TaskManagement.API.Configuration
{
    public static class AutomapperConfig
    {
        public static IServiceCollection MapperConfig(this IServiceCollection services)
        {
            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProjectProfile>();
                cfg.AddProfile<ProjectTaskProfile>();
                cfg.AddProfile<TaskCommentProfile>();
            });

            services.AddSingleton(autoMapperConfig.CreateMapper());

            return services;
        }
    }
}
