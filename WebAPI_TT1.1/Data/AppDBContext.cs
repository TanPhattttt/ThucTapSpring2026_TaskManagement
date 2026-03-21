using Microsoft.EntityFrameworkCore;
using TaskManagent.Domain_TT1.Entities;
using WebAPI_TT1._1.Models;

namespace WebAPI_TT1._1.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<Userss> Usersss { get; set; }
        public DbSet<Models.Project> Projects { get; set; }
        public DbSet<Models.TaskItem> TaskItems { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Roles> Roless { get; set; }    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.Project>()
                .HasMany(p => p.Tasks)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId);

            modelBuilder.Entity<Models.TaskItem>()
                .HasOne(t => t.AssignedUser)
                .WithMany()
                .HasForeignKey(t => t.AssignedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Roles>().HasData(
                new Roles
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "USER"
                },
                new Roles
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "ADMIN"
                });

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });
        }
    }
}
