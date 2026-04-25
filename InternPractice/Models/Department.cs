using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternPractice.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        // Navigation property: One department has many students
        public virtual ICollection<Student>? Students { get; set; }
    }
}