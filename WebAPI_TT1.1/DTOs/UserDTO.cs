using WebAPI_TT1._1.Models;

namespace WebAPI_TT1._1.DTOs
{
    public class UserDTO
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public Guid ProjectId { get; set; }
    }
}
