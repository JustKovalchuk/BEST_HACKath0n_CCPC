using Hackaton.Models;
using Hackaton.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Hackaton.Validation.User;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Hackaton.Data;
using Hackaton.Services;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Hackaton.Controllers
{
    public class HomeController : Controller
    {
        public readonly ILogger<HomeController> _logger;
        public readonly IApplicationDbContext _db;
        public readonly IUserService _userService;
        private readonly UserSignUpValidation _validator;

        public HomeController(ILogger<HomeController> logger, IUserService userService, IApplicationDbContext db, UserSignUpValidation validator)
        {
            _logger = logger;
            _userService = userService;
            _db = db;
            _validator = validator;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult SignUpHelper()
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
        public async Task<IActionResult> SignUpHelper(UserData model)
        {
            var validationResult = model.Validator.Validate(model); // ������������� ���������� ��������
            if (validationResult.IsValid)
            {
                _logger.LogInformation(model.ToString());

                model.Role = "Helper";
                var id = await _userService.InsertAsync(model);
                _logger.LogInformation("success");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(model);
            }
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
