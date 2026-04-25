using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternPractice.Models
{
    public class Student
    {
        // Primary Key
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the student's name")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please specify the course")]
        public string Course { get; set; }

        // --- Module C: Relationship with Department ---

        // Foreign Key: This stores the ID of the Department
        [Required(ErrorMessage = "Please select a department")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        // Navigation Property: This allows us to access Department.Name
        [ForeignKey("DepartmentId")]
        public virtual Department? Department { get; set; }
    }
}