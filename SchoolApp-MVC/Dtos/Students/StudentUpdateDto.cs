using System.ComponentModel.DataAnnotations;

namespace SchoolApp_MVC.Dtos.Students
{
    public class StudentUpdateDto
    {
        [Required]
        public int StudentID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string StudentName { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string StudentSurname { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string StudentEmail { get; set; } = null!;
    }
}
