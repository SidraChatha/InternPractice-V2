using System.ComponentModel.DataAnnotations;

namespace InternPractice.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required]
        [Range(16, 60, ErrorMessage = "Age must be between 16 and 60")]
        public int Age { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; } = DateTime.Today;
    }
}