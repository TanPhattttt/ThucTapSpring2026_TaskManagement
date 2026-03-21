using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagent.Domain_TT1.Entities
{
    public class Project_Entity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
