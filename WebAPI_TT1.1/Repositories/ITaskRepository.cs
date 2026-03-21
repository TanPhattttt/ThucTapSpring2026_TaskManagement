using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI_TT1._1.Data;
using WebAPI_TT1._1.DTOs;
using WebAPI_TT1._1.Models;

namespace TaskManager.Test.Repositories
{
    public interface ITaskRepository
    {
        IQueryable<TaskItem> GetQuery();

        Task<TaskItem?> GetByIdAsync(Guid id);

        Task AddAsync(TaskItem task);

        void Update(TaskItem task);

        void Delete(TaskItem task);

        Task SaveChangesAsync();
        Task<bool> ProjectExists(Guid projectId);
        Task<bool> UserExists(Guid userId);
    }
}
