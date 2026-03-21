using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagent.Domain_TT1.Entities
{
    public class Project
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        private readonly List<TaskItem> _task;
        public IReadOnlyCollection<TaskItem> Tasks => _task.AsReadOnly();

        public Project(string name )
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Project name is required ", nameof(name));
            }

            Id = Guid.NewGuid();
            Name = name;
            _task = new List<TaskItem>();
        }

        public void AddTask(TaskItem task)
        {
            if(task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }
            _task.Add(task);
        }

        public void GetTaskById(Guid id)
        {
            var task = _task.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                throw new InvalidOperationException("Task not found");
            }
            _task.Remove(task);
        }
    }
}
