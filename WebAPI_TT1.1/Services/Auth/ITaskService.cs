using WebAPI_TT1._1.DTOs;
using WebAPI_TT1._1.Models;

namespace WebAPI_TT1._1.Services.Auth
{
    public interface ITaskService
    {
        Task<List<TaskResponseDTO>> GetAll();
        Task<TaskItem> GetById(Guid id);
        Task<TaskItem> Create(CreateOrUpdateTaskDTO dto);
        Task<TaskItem> Assign(AssignTaskDTO dTO);
        Task<TaskItem> UpdateStatus(UpdateTaskStatusDTO dto);
        Task<TaskItem> Delete(Guid taskId);
        Task<List<TaskItem>> GetByProjectId(Guid projectId);
        Task<List<TaskItem>> GetByUser(Guid userId);
    }
}
