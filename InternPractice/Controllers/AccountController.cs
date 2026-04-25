using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternPractice.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        // Task A2.1: Injecting Identity Managers via Constructor
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // --- REGISTRATION (Task A2.1) ---

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = email, Email = email };
                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    // Sign the user in immediately after registering
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Student");
                }

                // Add errors to ModelState so they show up in the View
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }

        // --- LOGIN (Task A2.2) ---

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password, bool rememberMe)
        {
            if (ModelState.IsValid)
            {
                // Task A2.2: Use SignInManager to validate credentials
                var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Student");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View();
        }

        // --- LOGOUT (Task A2.3) ---

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Task A2.3: Sign out and redirect to Home
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}