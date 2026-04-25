using InternPractice.Data;
using InternPractice.Hubs;
using InternPractice.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- 1. DATABASE CONFIGURATION ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- 2. IDENTITY SERVICES ---
// Task A1.3: Password and Sign-in policies
builder.Services.AddDefaultIdentity<IdentityUser>(options => {
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6; 
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>() 
.AddEntityFrameworkStores<ApplicationDbContext>();

// --- 3. API & MVC SERVICES ---
builder.Services.AddControllersWithViews(); 
builder.Services.AddControllers(); // Task B1.1: For API Controllers
builder.Services.AddSignalR();
builder.Services.AddScoped<StudentService>();

// --- 4. SWAGGER / API DOCUMENTATION ---
// Task B1.5: Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- 5. MIDDLEWARE PIPELINE ---
if (app.Environment.IsDevelopment())
{
    // Task B1.5: Enable Swagger UI in development mode
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// IMPORTANT: Authentication must come before Authorization
app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await SeedData.Initialize(services);
    }
    catch (Exception)
    {
        // Log errors here if seeding fails
    }
}

// --- 7. ROUTES ---
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Required for Identity UI
app.MapControllers(); // Task B1.1: Maps the API routes
app.MapHub<NotificationHub>("/hubs/notifications");

app.Run();