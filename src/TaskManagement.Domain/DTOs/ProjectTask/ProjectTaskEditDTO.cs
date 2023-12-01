﻿using TaskManagement.Domain.Enuns;

namespace TaskManagement.Domain.DTOs.ProjectTask
{
    public class ProjectTaskEditDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Enuns.TaskStatus Status { get; set; }
        public TaskPriority TaskPriority { get; set; }
        public int ProjectId { get; set; }
    }
}
