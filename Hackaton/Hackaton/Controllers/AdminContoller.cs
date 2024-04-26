using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hackaton.Controllers
{
    public class AdminContoller : Controller
    {
        public IActionResult SignUp()
        {
            return View();
        }

    }
}
