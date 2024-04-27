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
    public class UserController : Controller
    {
        public readonly ILogger<UserController> _logger;
        public readonly IApplicationDbContext _db;
        public readonly IUserService _userService;
        private readonly UserSignUpValidator _signUpValidator;
        private readonly UserLoginValidator _logInValidator;

        public UserController(ILogger<UserController> logger, IUserService userService, IApplicationDbContext db, UserSignUpValidator signUpValidator, UserLoginValidator logInValidator)
        {
            _logger = logger;
            _userService = userService;
            _db = db;
            _signUpValidator = signUpValidator;
            _logInValidator = logInValidator;
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

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(UserData model)
        {
            var validationResult = _signUpValidator.Validate(model);

            if (validationResult.IsValid)
            {
                var is_user_exists = await _userService.IsUserExistsAsync(model.Email);

                if (is_user_exists)
                {
                    _logger.LogInformation("Email already exists! Try LogIn or use another email!");
                    return View(model);
                }

                model.Role = "Helper";
                var id = await _userService.InsertAsync(model);

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
        
        [AllowAnonymous]
        [HttpGet]
        public IActionResult LogIn()
        {
            var user = HttpContext.User.Identity.IsAuthenticated;
            if (user == true)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LogInData model)
        {
            var validationResult = _logInValidator.Validate(model);
            if (validationResult.IsValid)
            {
                var is_exists = await _userService.IsUserExistsAsync(model.Email);
                if (!is_exists)
                {
                    _logger.LogInformation("No such user! (LogIn User)");
                    return View(model);
                }
                else
                {
                    var is_valid_password = await _userService.IsValidPasswordAsync(model.Email, model.Password);
                    if (!is_valid_password)
                    {
                        _logger.LogInformation("Wrong password! (LogIn User)");
                        return View(model);
                    }
                    else
                    {
                        _logger.LogInformation("Success Login! (LogIn User)");
                    }
                }

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

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
