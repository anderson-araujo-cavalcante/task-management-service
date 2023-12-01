using AutoMapper;
using TaskManagement.Domain.DTOs.ProjectTask;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Profiles
{
    public class ProjectTaskProfile : Profile
    {
        public ProjectTaskProfile()
        {
            CreateMap<ProjectTask, ProjectTaskDTO>().ReverseMap();
            CreateMap<ProjectTaskCreateDTO, ProjectTask>();
            CreateMap<ProjectTaskEditDTO, ProjectTask>();
        }
    }
}
