namespace WebAPI_TT1._1.Models
{
    public class Roles
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserRole> UserRoleee { get; set; } = new List<UserRole>();
    }
}
