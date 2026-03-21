using WebAPI_TT1._1.Enums;

namespace WebAPI_TT1._1.DTOs
{
    public class TaskResponseDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public TaskStatusss Status { get; set; }
        public string ProjectName { get; set; }
        public string AssignedUserName { get; set; }
    }
}
