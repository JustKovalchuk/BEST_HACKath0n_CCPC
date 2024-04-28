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

        public ChatController(ApplicationDbContext context, ILogger<ChatController> logger, UserManager<UserData> userManager, MessageValidator messageValidator)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _messageValidator = messageValidator;
        }

        [HttpGet]
        [Route("/Chat/{chatid}")]
        public IActionResult Chat()
        {
            return View();
        }

        [HttpPost]
        [Route("/Chat/{chatid}")]
        public async Task<IActionResult> Chat(Message msg, int chatid)
        {
            await CreateMessage(msg);
            _logger.LogInformation($" {chatid}");
            return View();
        }

        public async Task CreateMessage(Message msg)
        {
            msg.When = DateTime.Now;
            var sender = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.Email).Value);
            msg.Sender = sender;
            msg.UserID = sender.Id.ToString();

            var validationResult = _messageValidator.Validate(msg);

            if (validationResult.IsValid)
            {
                await _context.Messages.AddAsync(msg);
                await _context.SaveChangesAsync();
            }
            _logger.LogInformation("not valid data post CreateMessage");
        }

        public IActionResult Index() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}