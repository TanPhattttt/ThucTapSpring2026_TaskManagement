using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_TT1._1.Data;
using WebAPI_TT1._1.DTOs;
using WebAPI_TT1._1.Exceptions;

namespace WebAPI_TT1._1.Controllers
{
    [Authorize(Roles = "ADMIN")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly AppDBContext _context;
        public ProjectController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var project = await _context.Projects.ToListAsync(); //.Include(p => p.Tasks).ToListAsync();
            return Ok(project);

            //var projects = await _context.Projects
            //    .Include(p => p.Tasks)
            //    .ToListAsync();

            //var result = projects.Select(p => new CreateOrUpdateProjectToTasksDTO
            //{
            //    Id = p.Id,
            //    Name = p.Name,
            //    Tasks = p.Tasks.Select(t => new CreateOrUpdateTaskDTO
            //    {
            //        AssignedUserId = t.Id,
            //        Title = t.Title,
            //        Status = t.Status,
            //    }).ToList()
            //});

            //return Ok(result);
        }


        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateOrUpdateProjectDTO dto)
        {
            var project = new Models.Project
            {
                Id = Guid.NewGuid(),
                Name = dto.Name
            };
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return Ok(project);
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPost("assign-task")]
        public async Task<IActionResult> AssignTask(AssignTaskDTO dto )
        {
            var task = await _context.TaskItems.FindAsync(dto.TaskId);
            if(task == null)
            {
                throw new CustomException("Task not found", 404);
            }
            var user = await _context.Usersss.FindAsync(dto.UserId);
            if(user == null)
            {
                throw new CustomException("User not found", 404);
            }
            task.AssignedUserId = dto.UserId;
            await _context.SaveChangesAsync();
            return Ok("Task assigned successfully");
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("JWT Working");
        }
    }
}
