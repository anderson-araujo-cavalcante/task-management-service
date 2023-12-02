namespace TaskManagement.Domain.DTOs.TaskComment
{
    public class TaskCommentDTO
    {
        public int Id { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Comment { get; set; }
        public int ProjectTaskId { get; set; }
    }
}
