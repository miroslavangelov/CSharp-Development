using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeisterMask.Data.Models
{
    public class EmployeeTask
    {
        [Required]
        public virtual Employee Employee { get; set; }

        [Key]
        [Required]
        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }

        [Required]
        public virtual Task Task { get; set; }

        [Key]
        [Required]
        [ForeignKey(nameof(Task))]
        public int TaskId { get; set; }
    }
}