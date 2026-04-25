using InternPractice.Data;
using InternPractice.Hubs;
using InternPractice.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace InternPractice.Services
{
    public class StudentService
    {
        private readonly ApplicationDbContext _db;
        private readonly IHubContext<NotificationHub> _hub;

        public StudentService(ApplicationDbContext db, IHubContext<NotificationHub> hub)
        {
            _db = db;
            _hub = hub; 
        }

        public async Task CreateAsync(Student student)
        {
            _db.Students.Add(student);
             await _db.SaveChangesAsync(); 

             await _hub.Clients.All.SendAsync("ReceiveNotification", $"New student {student.Name} was added."); 
        }

        
    }
}