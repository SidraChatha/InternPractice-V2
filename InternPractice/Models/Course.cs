using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternPractice.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        

        [Required(ErrorMessage = "Course title is required")]
        
        [StringLength(150, ErrorMessage = "Title cannot exceed 150 characters")]
        
        public string Title { get; set; } = null!;

        [Required]
        
        [Range(1, 6, ErrorMessage = "Credits must be between 1 and 6")]
        
        public int Credits { get; set; }

        // Foreign Key for Department
        
        [Required(ErrorMessage = "Please select a department")]
        
        public int DepartmentId { get; set; }

        // Navigation Property - This allows the Repository to use .Include()
        [ForeignKey("DepartmentId")]
        public virtual Department? Department { get; set; }
    }
}