using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain.DTOs.ProjectTask;
using TaskManagement.Domain.DTOs.TaskComment;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Services;
using TaskManagement.Domain.Services;

namespace TaskManagement.API.Controllers
{
    [Route("api/task-comment")]
    [ApiController]
    public class TaskCommentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITaskCommentService _taskCommentService;

        public TaskCommentController(IMapper mapper, ITaskCommentService taskCommentService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _taskCommentService = taskCommentService ?? throw new ArgumentNullException(nameof(taskCommentService)); ;
        }

        [HttpPost("{userUpdate}")]
        public async Task<IActionResult> AddCommentAsync(int userUpdate, TaskCommentCreateDTO taskCommentDTO)
        {
            var taskComment = _mapper.Map<TaskComment>(taskCommentDTO);
            await _taskCommentService.AddAsync(taskComment, userUpdate);
            return Ok();
        }

        [HttpPut("{userUpdate}")]
        public async Task<IActionResult> EditCommentAsync(int userUpdate, TaskCommentEditDTO taskCommentDTO)
        {
            var taskComment = _mapper.Map<TaskComment>(taskCommentDTO);
            await _taskCommentService.UpdateAsync(taskComment, userUpdate);
            return Ok();
        }

        [HttpGet("task/{id}")]
        public async Task<IActionResult> GetByTaskIdAsync(int id)
        {
            var taskComment = await _taskCommentService.GetByTaskIdAsync(id);
            return Ok(_mapper.Map<IEnumerable<TaskCommentDTO>>(taskComment));
        }

        [HttpGet("project/{id}")]
        public async Task<IActionResult> GetByProjectIdAsync(int id)
        {
            var taskComment = await _taskCommentService.GetByProjectIdAsync(id);
            return Ok(_mapper.Map<IEnumerable<TaskCommentDTO>>(taskComment));
        }
    }
}
