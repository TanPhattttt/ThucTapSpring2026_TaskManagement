namespace WebAPI_TT1._1.Models
{
    public class UserRole
    {
        public Guid UserId { get; set; }
        public Userss User { get; set; } = null!;

        public Guid RoleId { get; set; }
        public Roles Role { get; set; } = null!;
    }
}
