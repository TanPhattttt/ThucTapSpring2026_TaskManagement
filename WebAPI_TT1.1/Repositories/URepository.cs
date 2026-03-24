using Microsoft.EntityFrameworkCore;
using WebAPI_TT1._1.Data;
using WebAPI_TT1._1.Models;

namespace WebAPI_TT1._1.Repositories
{
    public class URepository : IURepository
    {
        private readonly AppDBContext _context;
        public URepository(AppDBContext context)
        {
            _context = context;
        }

        public Task AddAsync(Userss user)
        {
            _context.Usersss.AddAsync(user);
            return _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Userss>> GetAllAsync()
        {
            return await _context.Usersss.ToListAsync();
        }

        public async Task<Userss?> GetByIdAsync(Guid id)
        {
            return await _context.Usersss.FindAsync(id);
        }
    }
}
