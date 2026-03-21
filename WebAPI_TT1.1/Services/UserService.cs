using WebAPI_TT1._1.DTOs;
using WebAPI_TT1._1.Models;
using WebAPI_TT1._1.Repositories;

namespace WebAPI_TT1._1.Services
{
    public class UserService : IUserService
    {
        private readonly IURepository _userRepo;
        public UserService(IURepository uRepository)
        {
            _userRepo = uRepository;
        }
        public async Task<Userss> CreateUserAsync(UserDTO userDto)
        {
            var user = new Userss
            {
                Id = Guid.NewGuid(),
                Name = userDto.Name,
                Email = userDto.Email
            };
            await _userRepo.AddAsync(user);
            return user;
        }


        public async Task<IEnumerable<Userss>> GetAllUsersAsync()
        {
            return await _userRepo.GetAllAsync();
        }

    }
}
