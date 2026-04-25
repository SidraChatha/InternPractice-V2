using System.ComponentModel.DataAnnotations;

namespace InternPractice.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int Credits { get; set; }
    }
}