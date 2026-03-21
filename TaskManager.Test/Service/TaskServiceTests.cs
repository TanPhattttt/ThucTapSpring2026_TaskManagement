using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Test.Repositories;
using WebAPI_TT1._1.Data;
using WebAPI_TT1._1.DTOs;
using WebAPI_TT1._1.Enums;
using WebAPI_TT1._1.Exceptions;
using WebAPI_TT1._1.Models;
using WebAPI_TT1._1.Services.Auth;
using Xunit;

namespace TaskManager.Test.Service
{
    
    public class TaskServiceTests
    {
        private readonly AppDBContext _context;
        private readonly CurrentUserService _currentUser;
        private readonly TaskService _taskService;
        private readonly ITaskRepository _taskRepository;

        public TaskServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDBContext(options);

            _currentUser = new CurrentUserService();
            _taskRepository = new TaskRepository(_context);

            _taskService = new TaskService(_currentUser, _taskRepository);
        }

        [Fact]
        public async Task Assign_ShouldCallUpdate_WithMoq()
        {
            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = "Test Task",
                Status = 0
            };

            var mockRepo = new Mock<ITaskRepository>();
            mockRepo.Setup(x => x.GetByIdAsync(task.Id)).ReturnsAsync(task);

            var currentUser = new CurrentUserService()
            {
                FakeRole = "ADMIN"
            };

            var service = new TaskService(currentUser, mockRepo.Object);

            var dto = new AssignTaskDTO
            {
                TaskId = task.Id,
                UserId = Guid.NewGuid()
            };

            await service.Assign(dto);

            mockRepo.Verify(x => x.Update(It.IsAny<TaskItem>()), Times.Once);
            mockRepo.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Assign_ShouldThrow_WhenTaskDone_WithMoq()
        {
            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = "Test Task",
                Status = TaskStatusss.DONE
            };

            var mockRepo = new Mock<ITaskRepository>();

            mockRepo.Setup(x => x.GetByIdAsync(task.Id)).ReturnsAsync(task);

            var currentUser = new CurrentUserService()
            {
                FakeRole = "ADMIN"
            };

            var service = new TaskService(currentUser, mockRepo.Object);

            var dto = new AssignTaskDTO
            {
                TaskId = task.Id,
                UserId = Guid.NewGuid()
            };

            await Assert.ThrowsAsync<CustomException>(() => service.Assign(dto));
        }


        [Fact]
        public async Task GetMyTasks_ReturnsAllTasks_ForAdmin()
        {
            // Arrange
            _currentUser.FakeRole = "ADMIN";
            _context.TaskItems.AddRange(
                new TaskItem { Id = Guid.NewGuid(), Title = "Task 1" },
                new TaskItem { Id = Guid.NewGuid(), Title = "Task 2" }
            );
            await _context.SaveChangesAsync();
            // Act
            var result = await _taskService.GetMyTasks();
            // Assert
            Assert.Equal(2, result.Count);
        }
        [Fact]
        public async Task Create_ShouldCreateTask_whenValid()
        {
            var projectId = Guid.NewGuid();

            _context.Projects.Add(new Project
            {
                Id = projectId,
                Name = "Test Project"
            });

            await _context.SaveChangesAsync();

            var dto = new CreateOrUpdateTaskDTO
            {
                Title = "Task 1",
                Status = 0,
                ProjectId = projectId,
                Deadline = DateTime.UtcNow.AddDays(1)
            };

            var result = await _taskService.Create(dto);

            Assert.NotNull(result);
            Assert.Equal(dto.Title, result.Title);
        }

        [Fact]
        public async Task Create_ShouldThrow_WhenProjectNotFound()
        {
            var dto = new CreateOrUpdateTaskDTO
            {
                Title = "Task 1",
                Status = 0,
                ProjectId = Guid.NewGuid(),
                Deadline = DateTime.UtcNow.AddDays(1)
            };
            await Assert.ThrowsAsync<CustomException>(() => _taskService.Create(dto));
        }

        [Fact]
        public async Task Create_ShouldThrow_WhenDeadlineInvalid()
        {
            var projectId = Guid.NewGuid();

            _context.Projects.Add(new Project
            {
                Id = projectId,
                Name = "Test Project" // 🔥 bắt buộc
            }); await _context.SaveChangesAsync();

            var dto = new CreateOrUpdateTaskDTO
            {
                Title = "Task 1",
                Status = 0,
                ProjectId = projectId,
                Deadline = DateTime.Today // sai
            };

            await Assert.ThrowsAsync<CustomException>(() => _taskService.Create(dto));
        }

        [Fact]
        public async Task UpdateStatus_ShouldThrow_WhenForbidden()
        {
            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = "Test Task",
                Status = 0,
                AssignedUserId = Guid.NewGuid()
            };

            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();

            _currentUser.FakeRole = "USER";
            _currentUser.FakeUserId = Guid.NewGuid();

            var dto = new UpdateTaskStatusDTO
            {
                TaskId = task.Id,
                Status = 0
            };

            await Assert.ThrowsAsync<CustomException>(() => _taskService.UpdateStatus(dto));
        }

        [Fact]
        public async Task Assign_ShouldAssignUser_WhenValid()
        {
            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = "Task 1",
                Status = 0
            };

            var user = new Userss
            {
                Id = Guid.NewGuid(),
                Name = "User A",
                Email = "A@gmail.com",
                PasswordHash = "hashedpassword"
            };

            _context.TaskItems.Add(task);
            _context.Usersss.Add(user);
            await _context.SaveChangesAsync();

            var dto = new AssignTaskDTO
            {
                TaskId = task.Id,
                UserId = user.Id
            };

            var result = await _taskService.Assign(dto);

            Assert.Equal(user.Id, result.AssignedUserId);
        }

        [Fact]
        public async Task Assign_ShouldThrow_WhenTaskDone()
        {
            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = "Test Task",
                Status = TaskStatusss.DONE
            };

            var user = new Userss
            {
                Id = Guid.NewGuid(),
                Name = "User A",
                Email = "A@gmail.com",
                PasswordHash = "hashedpassword"
            };

            _context.TaskItems.Add(task);
            _context.Usersss.Add(user);
            await _context.SaveChangesAsync();

            var dto = new AssignTaskDTO
            {
                TaskId = task.Id,
                UserId = user.Id
            };

            await Assert.ThrowsAsync<CustomException>(() => _taskService.Assign(dto));
        }

        [Fact]
        public async Task GetMyTasks_ShouldReturnAll_WhenAdmin()
        {
            _currentUser.FakeRole = "ADMIN";

            _context.TaskItems.Add(new TaskItem { Title = "Task 1"});
            _context.TaskItems.Add(new TaskItem { Title = "Task 2"});

            await _context.SaveChangesAsync();

            var result = await _taskService.GetMyTasks();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetMyTasks_ShouldReturnOnlyMine()
        {
            var userId = Guid.NewGuid();

            _currentUser.FakeRole = "USER";
            _currentUser.FakeUserId = userId;

            _context.TaskItems.Add(new TaskItem
            {
                Title = "My Task",
                AssignedUserId = userId
            });

            _context.TaskItems.Add(new TaskItem
            {
                Title = "Other Task",
                AssignedUserId = Guid.NewGuid()
            });

            await _context.SaveChangesAsync();

            var result = await _taskService.GetMyTasks();

            Assert.Single(result);
        }
    }
}
