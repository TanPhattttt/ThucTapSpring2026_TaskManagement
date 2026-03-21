using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_TT1._1.Data;
using WebAPI_TT1._1.DTOs;
using WebAPI_TT1._1.Models;
using WebAPI_TT1._1.Services.Auth;

namespace WebAPI_TT1._1.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly JwtService _jwtService;
        public AuthController(AppDBContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Tạo user mới và gán role USER cho user đó
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Tạo thành công user mới</returns>
        [HttpPost("register")] 
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            try
            {
                var user = new Userss
                {
                    Id = Guid.NewGuid(),
                    Name = dto.Name,
                    Email = dto.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
                };

                _context.Usersss.Add(user);
                await _context.SaveChangesAsync();

                var userRole = new UserRole
                {
                    UserId = user.Id,
                    RoleId = Guid.Parse("11111111-1111-1111-1111-111111111111") // USER role
                };

                _context.UserRoles.Add(userRole);

                await _context.SaveChangesAsync();
                return Ok("Register success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }
        /// <summary>
        /// Đăng nhập bằng email và password, nếu thành công sẽ trả về token JWT có chứa thông tin user và role của user đó
        /// Rồi token này sẽ được dán vào header Authorization của các request tiếp theo để xác thực và phân quyền truy cập vào các endpoint khác nhau trong hệ thống
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Đăng nhập thành công bằng email và password</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var user = await _context.Usersss.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if(user == null)
            {
                return Unauthorized("Invalid email");
            }

            var check = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!check)
            {
                return Unauthorized("Invalid password");
            }

            var role = await _context.UserRoles
                .Include(x => x.Role)
                .Where(x => x.UserId == user.Id)
                .Select(x => x.Role.Name)
                .FirstOrDefaultAsync();

            var token = _jwtService.GenerateToken(user, role);

            return Ok(new
            {
                token
            });
        }
    }
}
