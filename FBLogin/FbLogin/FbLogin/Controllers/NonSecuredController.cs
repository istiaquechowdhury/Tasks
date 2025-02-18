using Microsoft.AspNetCore.Mvc;

namespace FbLogin.Controllers
{
    public class NonSecuredController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
