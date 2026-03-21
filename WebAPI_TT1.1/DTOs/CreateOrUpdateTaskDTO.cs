using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace WebAPI_TT1._1.DTOs
{
    public class CreateOrUpdateTaskDTO
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        public Guid ProjectId { get; set; }
        public Guid? AssignedUserId { get; set; }
        public DateTime Deadline { get; set; }
    }
}
