using AutoMapper;
using TaskManagement.Domain.DTOs.TaskComment;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Profiles
{
    public class TaskCommentProfile : Profile
    {
        public TaskCommentProfile()
        {
            CreateMap<TaskComment, TaskCommentDTO>().ReverseMap();
            CreateMap<TaskCommentCreateDTO, TaskComment>();
            CreateMap<TaskCommentEditDTO, TaskComment>();
        }
    }
}
