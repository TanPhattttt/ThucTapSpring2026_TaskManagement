using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI_TT1._1.DTOs;
using WebAPI_TT1._1.Models;
using WebAPI_TT1._1.Services;

namespace WebAPI_TT1._1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllUsersAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserDTO userDto)
        {
            var createdUser = await _service.CreateUserAsync(userDto);
            return CreatedAtAction(nameof(GetAll), new { id = createdUser.Id }, createdUser);
        }

        //[HttpPost("SignUp")]
        //public async Task<IActionResult> SignUp(UserDTO dto) // Đổi tên phương thức để tránh trùng với Create
        //{
        //    var newacc = new Userss
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = dto.Name,
        //        Email = dto.Email,
        //        Password = dto.Password,
        //        ProjectId = dto.ProjectId
        //    };
        //    await _service.CreateUserAsync(dto);
        //    return Ok(newacc);
        //}

    }
}
