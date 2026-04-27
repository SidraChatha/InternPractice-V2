using InternPractice.Models;

namespace InternPractice.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<Course?> GetCourseByIdAsync(int id);
        Task CreateCourseAsync(Course course);
        Task UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(int id);
    }
}