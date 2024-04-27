using Hackaton.Data;
using Hackaton.Hubs;
using Hackaton.Models;
using Hackaton.Models.Chats;
using Hackaton.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Hackaton.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        public readonly ILogger<ChatController> _logger;
        private readonly UserManager<UserData> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ChatHub _hub;

        public ChatController(ApplicationDbContext context, ILogger<ChatController> logger, UserManager<UserData> userManager, ChatHub hub)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _hub = hub;
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(Message msg)
        {
            if (ModelState.IsValid)
            {
                msg.Username = User.Identity.Name;
                var sender = await _userManager.GetUserAsync(User);
                msg.UserID = sender.Id.ToString();
                await _context.Messages.AddAsync(msg);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return Error();
        }
       

        public IActionResult Index() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
