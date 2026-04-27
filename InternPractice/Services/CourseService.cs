using InternPractice.Models;
using InternPractice.Repositories;

namespace InternPractice.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepo;

        // Dependency Injection: We ask for the interface, not the implementation
        public CourseService(ICourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _courseRepo.GetAllAsync();
        }

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            return await _courseRepo.GetByIdAsync(id);
        }

        public async Task CreateCourseAsync(Course course)
        {
            // Business Rule Example: You could add logic here to check for duplicate titles
            await _courseRepo.AddAsync(course);
            await _courseRepo.SaveChangesAsync();
        }

        public async Task UpdateCourseAsync(Course course)
        {
            _courseRepo.Update(course);
            await _courseRepo.SaveChangesAsync();
        }

        public async Task DeleteCourseAsync(int id)
        {
            var course = await _courseRepo.GetByIdAsync(id);
            if (course != null)
            {
                _courseRepo.Delete(course);
                await _courseRepo.SaveChangesAsync();
            }
        }
    }
}