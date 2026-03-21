using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagent.Domain_TT1.Enum;


namespace TaskManagent.Domain_TT1.Entities
{
    public class TaskItem
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public TaskItemStatus Status { get; private set; }
        public User? AssignedUser { get; private set; }
        
        public TaskItem(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be null or empty", nameof(title));
            }
            Id = Guid.NewGuid();
            Title = title;
            Status = TaskItemStatus.TODO;
        }

        public void AssignUser(User user)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            AssignedUser = user;
        }

        public void MoveToInProgress()
        {
            if (Status != TaskItemStatus.TODO)
            {
                throw new InvalidOperationException("Task must be TODO");
            }
        }

        
    }
}
