using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InternPractice.Data;
using InternPractice.Models;

namespace InternPractice.Controllers.Api
{
    [Route("api/[controller]")] // Task B1.1: Standard API Route
    [ApiController]
    public class StudentsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/StudentsApi (Task B1.2: Retrieve all students)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        // GET: api/StudentsApi/5 (Task B1.2: Retrieve a single student)
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound(); // Returns 404 Status Code
            }

            return student;
        }

        // POST: api/StudentsApi (Task B1.3: Create a new student via API)
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        // DELETE: api/StudentsApi/5 (Task B1.4: Delete a student via API)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent(); // Returns 204 Status Code
        }
    }
}