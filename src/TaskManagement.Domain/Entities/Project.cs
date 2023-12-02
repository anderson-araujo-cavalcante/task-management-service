namespace TaskManagement.Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public ICollection<ProjectTask> Tasks { get; set; }
    }
}
