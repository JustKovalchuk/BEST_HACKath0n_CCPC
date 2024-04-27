using Hackaton.Models;
using Hackaton.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Hackaton.Validation.User;
using Hackaton.Data;
using Hackaton.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using MySqlX.XDevAPI.Common;


namespace Hackaton.Controllers
{
    public class UserController : Controller
    {
        public readonly ILogger<UserController> _logger;
        public readonly IApplicationDbContext _db;
        public readonly IUserService _userService;
        private readonly UserSignUpValidator _signUpValidator;
        private readonly UserLoginValidator _logInValidator;

        private readonly UserManager<UserData> _userManager;

        public UserController(ILogger<UserController> logger, IUserService userService, IApplicationDbContext db, UserSignUpValidator signUpValidator, UserLoginValidator logInValidator, UserManager<UserData> userManager)
        {
            _logger = logger;
            _userService = userService;
            _db = db;
            _signUpValidator = signUpValidator;
            _logInValidator = logInValidator;

            _userManager = userManager;
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

            _logger.LogInformation($"validationResult {validationResult}");
            if (validationResult.IsValid)
            {
                var userExists = await _userManager.FindByEmailAsync(model.Email);
                if (userExists != null)
                {
                    _logger.LogInformation("Email already exists! Try LogIn or use another email!");
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                model.UserName = model.Email;

                var result = await _userManager.CreateAsync(model, model.Password);
                if (!result.Succeeded)
                {
                    _logger.LogInformation($"!result.Succeeded {result.ToString()}");
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                _logger.LogInformation("result.Succeeded");
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
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    _logger.LogInformation("No such user! (LogIn User)");
                    return Unauthorized();
                }
                var is_valid_password = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!is_valid_password)
                {
                    _logger.LogInformation("Wrong password! (LogIn User)");
                    return Unauthorized();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, model.Email)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                _logger.LogInformation("Success Login! (LogIn User)");
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

        [HttpGet]
        [HttpPost]
        [Route("/User/Logout")]
        public async Task<IActionResult> LogOut()
        {
            _logger.LogInformation($"Logout!");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
