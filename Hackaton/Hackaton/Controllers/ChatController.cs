using Hackaton.Data;
using Hackaton.Hubs;
using Hackaton.Models;
using Hackaton.Models.Chats;
using Hackaton.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Hackaton.Validation;

namespace Hackaton.Controllers
{
    //[Authorize]
    public class ChatController : Controller
    {
        public readonly ILogger<ChatController> _logger;
        private readonly UserManager<UserData> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly MessageValidator _messageValidator;
        private readonly ChatHub _hub;

        public ChatController(ApplicationDbContext context, ILogger<ChatController> logger, UserManager<UserData> userManager, ChatHub hub, MessageValidator messageValidator)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _hub = hub;
            _messageValidator = messageValidator;
        }

        [HttpGet]
        //[Route("/Chat/{Id}")]
        public IActionResult Chat()
        {
            var user = HttpContext.User.Identity.IsAuthenticated;
            if (user == true)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateMessage()
        {
            _logger.LogInformation("get CreateMessage");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(Message msg)
        {
            msg.When = DateTime.Now;
            
            _logger.LogInformation($"post CreateMessage {User.FindFirst(ClaimTypes.Email).Value} {User.Identity.IsAuthenticated}");
            var sender = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.Email).Value);
            msg.Sender = sender;
            msg.UserID = sender.Id.ToString();
            _logger.LogInformation($"post CreateMessage {msg.Id} {msg.Username} {msg.UserID} {msg.Sender} {msg.When}");

            var validationResult = _messageValidator.Validate(msg);

            if (validationResult.IsValid)
            {
                //msg.Username = User.Identity.Name;
                await _context.Messages.AddAsync(msg);
                await _context.SaveChangesAsync();
                return Ok();
            }
            _logger.LogInformation("not valid data post CreateMessage");
            return Error();
        }



        public IActionResult Index() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
