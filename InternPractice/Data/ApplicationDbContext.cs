using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Required for Identity 
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using InternPractice.Models;

namespace InternPractice.Data
{
    
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        
        public DbSet<Student> Students { get; set; }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}