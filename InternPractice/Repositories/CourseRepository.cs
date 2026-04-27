using InternPractice.Data; // Ensure this matches your context namespace
using InternPractice.Models;
using Microsoft.EntityFrameworkCore;

namespace InternPractice.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _db;

        public CourseRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _db.Courses.Include(c => c.Department).ToListAsync();
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _db.Courses.Include(c => c.Department)
                                    .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Course course)
        {
            await _db.Courses.AddAsync(course);
        }

        public void Update(Course course)
        {
            _db.Courses.Update(course);
        }

        public void Delete(Course course)
        {
            _db.Courses.Remove(course);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _db.SaveChangesAsync()) > 0;
        }
    }
}