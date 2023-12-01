namespace TaskManagement.Domain.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
    }
}
