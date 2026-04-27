using Moq;
using InternPractice.Models;
using InternPractice.Repositories;
using InternPractice.Services;
using Xunit;

namespace InternPractice.Tests
{
    public class CourseServiceTests
    {
        private readonly Mock<ICourseRepository> _mockRepo;
        private readonly CourseService _courseService;

        public CourseServiceTests()
        {
            
            _mockRepo = new Mock<ICourseRepository>();
            _courseService = new CourseService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllCoursesAsync_ShouldReturnAllCourses()
        {
            // Arrange - Set up fake data
            var fakeCourses = new List<Course>
            {
                new Course { Id = 1, Title = "Test Course 1" },
                new Course { Id = 2, Title = "Test Course 2" }
            };
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(fakeCourses);

            // Act - Call the service
            var result = await _courseService.GetAllCoursesAsync();

            // Assert - Check if it worked
            Assert.Equal(2, result.Count());
            _mockRepo.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetCourseByIdAsync_ShouldReturnCourse_WhenCourseExists()
        {
            // Arrange
            var fakeCourse = new Course { Id = 1, Title = "Specialized C#" };
            _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(fakeCourse);

            // Act
            var result = await _courseService.GetCourseByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Specialized C#", result.Title);
        }
    }
}