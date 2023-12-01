using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain.DTOs.Project;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Services;
using TaskManagement.Domain.Services;

namespace TaskManagement.API.Controllers
{
    [Route("api/task")]
    [ApiController]
    public class ProjectTaskController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProjectTaskService _projectTaskService;
        public ProjectTaskController(IMapper mapper, IProjectTaskService projectTaskService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _projectTaskService = projectTaskService ?? throw new ArgumentNullException(nameof(projectTaskService)); ;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var project = await _projectTaskService.GetByIdAsync(id);
            return Ok(_mapper.Map<ProjectDTO>(project));
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProjectCreateDTO productDto)
        {
            var project = _mapper.Map<Project>(productDto);
            await _projectTaskService.AddAsync(project);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] ProjectEditDTO projectDTO)
        {
            var project = _mapper.Map<Project>(projectDTO);
            await _projectTaskService.UpdateAsync(project);
            return Ok();
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetByUserIdAsync(int id)
        {
            var project = await _projectTaskService.GetByUserIdAsync(id);
            return Ok(_mapper.Map<IEnumerable<ProjectDTO>>(project));
        }
    }
}
