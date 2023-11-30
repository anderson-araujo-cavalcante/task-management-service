namespace TaskManagement.Domain.Entities
{
    public class Project : IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public ICollection<ProjectTask> Tasks { get; set; }
    }
}
