using WebAPI_TT1._1.Enums;

namespace WebAPI_TT1._1.Models
{
    public class TaskItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = null!;
        public TaskStatusss Status { get; set; }

        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public Guid? AssignedUserId { get; set; }
        public Userss? AssignedUser { get; set; }
    }
}
