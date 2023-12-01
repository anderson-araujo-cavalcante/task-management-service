namespace TaskManagement.Domain.Entities
{
    public class Historic
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProjectTaskId { get; set; }
        public DateTime UpdateDate { get; set; }        
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}