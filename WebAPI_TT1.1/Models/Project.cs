using TaskManagent.Domain_TT1.Entities;

namespace WebAPI_TT1._1.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
