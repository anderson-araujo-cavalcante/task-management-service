namespace TaskManagement.Domain.Entities
{
    public class TaskComment
    {
        public int Id { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Comment { get; set; }
        public int ProjectTaskId { get; set; }
        public ProjectTask ProjectTask { get; set; }
    }
}
