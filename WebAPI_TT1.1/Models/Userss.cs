namespace WebAPI_TT1._1.Models
{
    public class Userss
    {
        public Guid Id { get; set; }
        public string Name { get; set; } =null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public ICollection<UserRole> UserRoleee { get; set; } = new List<UserRole>();
    }
}
