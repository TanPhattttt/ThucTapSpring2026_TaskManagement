using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebAPI_TT1._1.Data;
using WebAPI_TT1._1.DTOs;
using WebAPI_TT1._1.Enums;
using WebAPI_TT1._1.Exceptions;
using WebAPI_TT1._1.Models;
using WebAPI_TT1._1.Services.Auth;

namespace WebAPI_TT1._1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService service)
        {
            _taskService = service;
        }

        /// <summary>
        /// Đăng nhập rồi vào trang chủ sẽ thấy tất cả task, có thể lọc theo project hoặc user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllTask()
        {
            var tasks = await _taskService.GetAll();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            var tasks = await _taskService.GetById(id);
            return Ok(new APIResponse<TaskItem>
            {
                Code = 200,
                Message = "Get Id successfully",
                Data = tasks
            });
        }

        /// <summary>
        /// Tạo task mới, chỉ có admin hoặc project manager 
        /// mới có quyền tạo task, khi tạo task cần chỉ định project và có thể chỉ 
        /// định người thực hiện (assigned user) nếu muốn
        /// </summary>
        /// <param name="taskItem"></param>
        /// <returns>Tạo Task mới thành công</returns>
        [HttpPost]
        public async Task<IActionResult> CreateNewTask(CreateOrUpdateTaskDTO taskItem)
        {
            var createdTask = await _taskService.Create(taskItem);
            return Ok(new APIResponse<TaskItem>
            {
                Code = 200,
                Message = "Task created successfully",
                Data = createdTask
            });
        }
        /// <summary>
        /// Assign task cho user, chỉ có admin hoặc project manager mới có quyền gán task cho người khác
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Assign thành công</returns>
        [HttpPut("assign")]
        public async Task<IActionResult> AssignUser(AssignTaskDTO dto)
        {
            var asignedTask = await _taskService.Assign(dto);
            return Ok(new APIResponse<TaskItem>
            {
                Code = 200,
                Message = "Task assigned successfully",
                Data = asignedTask
            });
        }
        /// <summary>
        /// Update status của task, chỉ có assigned user mới có quyền cập nhật status của task đó,
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Update status thành công</returns>
        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateStatusTask(UpdateTaskStatusDTO dto)
        {

            var updatedTask = await _taskService.UpdateStatus(dto);
            return Ok(new APIResponse<TaskItem>
            {
                Code = 200,
                Message = "Task status updated successfully",
                Data = updatedTask
            });
        }
        /// <summary>
        /// Xóa task, chỉ có admin hoặc project manager mới có quyền xóa task, khi xóa task sẽ xóa vĩnh viễn khỏi database
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(Guid taskId)
        {
            var deletedTask = await _taskService.Delete(taskId);
            return Ok(new APIResponse<TaskItem>
            {
                Code = 200,
                Message = "Task deleted successfully",
                Data = deletedTask
            });
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetByProjectId(Guid projectId)
        {
            var tasks = await _taskService.GetByProjectId(projectId);
            return Ok(new APIResponse<TaskItem>
            {
                Code = 200,
                Message = "Tasks retrieved successfully",
                Data = null
            });
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(Guid userId)
        {
            var task = await _taskService.GetByUser(userId);
            return Ok(new APIResponse<List<TaskItem>>
            {
                Code = 200,
                Message = "Tasks retrieved successfully",
                Data = task
            });
        }

    }
}