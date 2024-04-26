using Hackaton.Models;
using Hackaton.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Hackaton.Models.Validation;
namespace Hackaton.Controllers
{
    public class HomeController : Controller
    {
        public readonly ILogger<HomeController> _logger;
        //private readonly UserSignUpValidation _validator;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            //_validator = validator;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult SignUp()
        {
            var user = HttpContext.User.Identity.IsAuthenticated;
            if (user == true)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        //[AllowAnonymous]
        //[HttpPost] // Handle POST requests
        //[ValidateAntiForgeryToken] // Add AntiForgeryToken validation
        //public async Task<IActionResult> SignUp(UserData model)
        //{
        //    var validationResult = _validator.Validate(model);
        //    if (validationResult.IsValid)
        //    {
        //        _logger.LogInformation($"User : {model.name}, Surname: {model.surname}, Email: {model.email}, Age: {model.age}, Password: {model.password},Copy Password: {model.copyPassword}");

        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        return View(model);
        //    }
        //}
        [AllowAnonymous]
        [HttpPost] // Handle POST requests
        [ValidateAntiForgeryToken] // Add AntiForgeryToken validation
        public ActionResult SignUp(UserData model)
        {
            if (ModelState.IsValid)
            {
                // Process user registration here
                // For now, just redirect to the home page
                _logger.LogInformation($"User : {model.Name}, Surname: {model.Surname}, Email: {model.Email}, Age: {model.Age}, Password: {model.Password},Copy Password: {model.CopyPassword}");

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
