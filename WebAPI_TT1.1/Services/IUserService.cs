using WebAPI_TT1._1.DTOs;
using WebAPI_TT1._1.Models;

namespace WebAPI_TT1._1.Services
{
    public interface IUserService
    {
        Task<IEnumerable<Userss>> GetAllUsersAsync();
        Task<Userss> CreateUserAsync(UserDTO userDto);
    }
}
