namespace WebAPI_TT1._1.DTOs
{
    public class UpdateTaskStatusDTO
    {
        public Guid TaskId { get; set; }
        public TaskStatus Status { get; set; }
    }
}
