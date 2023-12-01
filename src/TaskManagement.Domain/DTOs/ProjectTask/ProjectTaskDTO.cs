using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Enuns;

namespace TaskManagement.Domain.DTOs.ProjectTask
{
    public class ProjectTaskDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpirationDate { get; set; }
        public TaskPriority Priority { get; set; }
        public int ProjectId { get; set; }
    }
}
