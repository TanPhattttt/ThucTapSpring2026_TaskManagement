using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
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
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDBContext _context;
        public TaskRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TaskItem task)
        {
            await _context.TaskItems.AddAsync(task);

        }

        public void Delete(TaskItem task)
        {
            _context .TaskItems.Remove(task);
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id)
        {
            return await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == id);

        }

        public IQueryable<TaskItem> GetQuery()
        {
            return _context.TaskItems.AsQueryable();
        }

        public async Task<bool> ProjectExists(Guid projectId)
        {
            return await _context.Projects.AnyAsync(p => p.Id == projectId);

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(TaskItem task)
        {
            _context.TaskItems.Update(task);
        }

        public async Task<bool> UserExists(Guid userId)
        {
            return await _context.Usersss.AnyAsync(u => u.Id == userId);

        }
    }
}
