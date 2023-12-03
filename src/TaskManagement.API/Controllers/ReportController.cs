using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain.DTOs.ProjectTask;
using TaskManagement.Domain.Interfaces.Services;
using TaskManagement.Domain.Services;

namespace TaskManagement.API.Controllers
{
    [Route("api/report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IProjectTaskService _projectTaskService;
        public ReportController(IProjectTaskService projectTaskService)
        {
            _projectTaskService = projectTaskService ?? throw new ArgumentNullException(nameof(projectTaskService)); ;
        }

        [HttpGet("{userId}/performance/{lastDays}")]
        public async Task<IActionResult> GetTaskPerformanceAsync(string userId, int lastDays)
        {
            return Ok(await _projectTaskService.GetTaskPerformanceAsync(userId, lastDays));
        }
    }
}
