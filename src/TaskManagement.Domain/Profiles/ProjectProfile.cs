﻿using AutoMapper;
using TaskManagement.Domain.DTO;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<ProjectCreateDTO, Project>();
            CreateMap<ProjectEditDTO, Project>();
        }
    }
}
