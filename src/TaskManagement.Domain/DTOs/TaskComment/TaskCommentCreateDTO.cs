namespace TaskManagement.Domain.DTOs.TaskComment
{
    public class TaskCommentCreateDTO
    {
        public int ProjectTaskId { get; set; }
        public string Comment { get; set; }       
    }
}
