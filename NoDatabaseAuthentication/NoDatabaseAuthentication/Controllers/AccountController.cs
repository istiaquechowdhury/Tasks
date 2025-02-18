using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NoDatabaseAuthentication.Controllers
{
    public class AccountController : Controller
    {

        private readonly List<(string Username, string Password)> validUsers = new()
        {
                ("admin", "password"),
                ("user1", "1234"),
                ("guest", "guestpass")
        };
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

       
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = validUsers.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != default)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "User") 
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Secure", "Home"); 
            }

            ViewBag.Error = "Invalid username or password";
            return View();
        }

       
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
