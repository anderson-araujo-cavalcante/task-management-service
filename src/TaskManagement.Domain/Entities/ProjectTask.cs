namespace TaskManagement.Domain.Entities
{
    public class ProjectTask : IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpirationDate { get; set; }
        public TaskStatus Status { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
