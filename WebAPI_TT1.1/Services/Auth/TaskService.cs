using Microsoft.EntityFrameworkCore;
using TaskManager.Test.Repositories;
using WebAPI_TT1._1.Data;
using WebAPI_TT1._1.DTOs;
using WebAPI_TT1._1.Enums;
using WebAPI_TT1._1.Exceptions;
using WebAPI_TT1._1.Models;

namespace WebAPI_TT1._1.Services.Auth
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;   
        private readonly CurrentUserService _currentUserService;
        public TaskService(CurrentUserService currentUserService, ITaskRepository repository)
        {
            _currentUserService = currentUserService;
            _taskRepository = repository;
        }

        public async Task<List<TaskResponseDTO>> GetAll()
        {
            var query = _taskRepository.GetQuery();

            if (_currentUserService.Role != "ADMIN")
            {
                query = query.Where(t => t.AssignedUserId == _currentUserService.UserId);
            }

            return await query
                .Select(t => new TaskResponseDTO
                {
                    Id = t.Id,
                    Title = t.Title,
                    Status = t.Status,
                    ProjectName = t.Project.Name,
                    AssignedUserName = t.AssignedUser != null ? t.AssignedUser.Name : null
                })
                .ToListAsync();
        }

        public async Task<TaskItem> GetById(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
                throw new CustomException("Task not found", 404);

            return task;
        }

        
        public async Task<TaskItem> Create(CreateOrUpdateTaskDTO dto)
        {
            var project = await _taskRepository.ProjectExists(dto.ProjectId);

            if(!project)
            {
                throw new CustomException("Project not found", 404);
            }
            if(dto.Deadline <= DateTime.Today)
            {
                throw new CustomException("Deadline must be in the future", 400);
            }

            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Status = (TaskStatusss)dto.Status,
                ProjectId = dto.ProjectId,
                AssignedUserId = dto.AssignedUserId,
            };

            await _taskRepository.AddAsync(task);
            await _taskRepository.SaveChangesAsync();
            return task;
        }

        

        public async Task<TaskItem> UpdateStatus(UpdateTaskStatusDTO dto)
        {
            var task = await _taskRepository.GetByIdAsync(dto.TaskId);
            if (task == null)
            {
                throw new CustomException("Task not found", 404);
            }

            if(_currentUserService.Role != "ADMIN" && task.AssignedUserId != _currentUserService.UserId)
            {
                throw new CustomException("Forbidden", 403);
            }

            if(task.Status == TaskStatusss.DONE)
            {
                throw new CustomException("Task is already completed", 400);
            }

            if (task.Status == TaskStatusss.TODO && (TaskStatusss)dto.Status != TaskStatusss.IN_PROGRESS)
            {
                throw new CustomException("TODO can only move to IN_PROGRESS", 400);
            }

            if (task.Status == TaskStatusss.IN_PROGRESS && (TaskStatusss)dto.Status != TaskStatusss.DONE)
            {
                throw new CustomException("IN_PROGRESS can only move to DONE", 400);
            }

            task.Status = (TaskStatusss)dto.Status;

            _taskRepository.Update(task);

            await _taskRepository.SaveChangesAsync();

            return task;
        }

        public async Task<TaskItem> Delete(Guid taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if(task == null)
            {
                throw new CustomException("Task not found", 404);
            }   

            _taskRepository.Delete(task);
            await _taskRepository.SaveChangesAsync();
            return task;
        }

        public async Task<TaskItem> Assign(AssignTaskDTO dTO)
        {
            var task = await _taskRepository.GetByIdAsync(dTO.TaskId);

            if (task == null)
            {
                throw new CustomException("Task not found", 404);
            }

            var user = await _taskRepository.UserExists(dTO.UserId);
            if (user == null)
            {
                throw new CustomException("User not found", 404);
            }

            if (task.Status == TaskStatusss.DONE)
            {
                throw new CustomException("Cannot assign a completed task", 400);
            }

            task.AssignedUserId = dTO.UserId;
            _taskRepository.Update(task);
            await _taskRepository.SaveChangesAsync();
            return task;
        }


        public async Task<List<TaskItem>> GetByProjectId(Guid projectId)
        {
            return await _taskRepository.GetQuery()
                .Where(t => t.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<List<TaskItem>> GetByUser(Guid userId)
        {
            return await _taskRepository.GetQuery()
                .Where(t => t.AssignedUserId == userId)
                .ToListAsync();
        }

        // Removed the invalid 'public' modifier from the explicit interface implementation
        public async Task<List<TaskItem>> GetMyTasks()
        {
            var query = _taskRepository.GetQuery();

            if (_currentUserService.Role != "ADMIN")
            {
                query = query.Where(t => t.AssignedUserId == _currentUserService.UserId);
            }

            return await query.ToListAsync();
        }
    }
}
