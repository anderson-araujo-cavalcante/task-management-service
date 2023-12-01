using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain.DTO;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Services;

namespace TaskManagement.API.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProjectService _projectService;
        public ProjectController(IMapper mapper, IProjectService projectService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService)); ;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var project = await _projectService.GetByIdAsync(id);
            return Ok(_mapper.Map<ProjectDTO>(project));
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProjectCreateDTO productDto)
        {
            var project = _mapper.Map<Project>(productDto);
            await _projectService.AddAsync(project);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] ProjectEditDTO projectDTO)
        {
            var project = _mapper.Map<Project>(projectDTO);
            await _projectService.UpdateAsync(project);
            return Ok();
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetByUserIdAsync(int id)
        {
            var project = await _projectService.GetByUserIdAsync(id);
            return Ok(_mapper.Map<IEnumerable<ProjectDTO>>(project));
        }
    }
}
