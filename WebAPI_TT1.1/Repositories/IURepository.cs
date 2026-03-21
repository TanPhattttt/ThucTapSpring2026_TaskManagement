using TaskManagent.Domain_TT1.Entities;
using WebAPI_TT1._1.Models;

namespace WebAPI_TT1._1.Repositories
{
    public interface IURepository
    {
        Task<IEnumerable<Userss>> GetAllAsync();
        Task<Userss?> GetByIdAsync(Guid id);
        Task AddAsync(Userss user);
    }
}
