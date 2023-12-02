﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain.DTOs.Project;
using TaskManagement.Domain.DTOs.ProjectTask;
using TaskManagement.Domain.DTOs.TaskComment;
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
            var projectTask = await _projectTaskService.GetByIdAsync(id);
            return Ok(_mapper.Map<ProjectTaskDTO>(projectTask));
        }

        [HttpPost("{userUpdate}")]
        public async Task<IActionResult> AddAsync(int userUpdate, ProjectTaskCreateDTO productTaskDto)
        {
            var projectTask = _mapper.Map<ProjectTask>(productTaskDto);
            await _projectTaskService.AddAsync(projectTask, userUpdate);
            return Ok();
        }

        [HttpPut("{userUpdate}")]
        public async Task<IActionResult> EditAsync(int userUpdate, [FromBody] ProjectTaskEditDTO projectTaskDTO)
        {
            var projectTask = _mapper.Map<ProjectTask>(projectTaskDTO);
            await _projectTaskService.UpdateAsync(projectTask, userUpdate);
            return Ok();
        }        
    }
}
