using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Hackaton.Data;
using Hackaton.Models;
using Hackaton.Models.Advertisement;
using Hackaton.Models.User;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

public class AdvertisementController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdvertisementController(ApplicationDbContext context)
    {
        _context = context;
    }

    private async Task<bool> UserHasRole(int roleId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
        if (user == null)
        {
            return false;
        }

        var userId = user.Id.ToString();
        return await _context.UserRoles.AnyAsync(ur => ur.UserId == (userId) && ur.RoleId == roleId.ToString());
    }

    public async Task<IActionResult> Index()
    {
        var advertisements = await _context.AdvertisementData.ToListAsync();
        return View(advertisements);
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        //if (!await UserHasRole(1) && !await UserHasRole(2))
        //{
        //    return Unauthorized();
        //}

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AdvertisementData model)
    {
        if (!await UserHasRole(1) && !await UserHasRole(2)) 
        {
            return Unauthorized();
        }

        if (ModelState.IsValid)
        {
            var advertisement = new AdvertisementData
            {
                Name = model.Name,
                Description = model.Description,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            _context.Add(advertisement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (!await UserHasRole(1) && !await UserHasRole(2))
        {
            return Unauthorized();
        }

        var advertisement = await _context.AdvertisementData.FindAsync(id);
        _context.AdvertisementData.Remove(advertisement);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool AdvertisementExists(int id)
    {
        return _context.AdvertisementData.Any(e => e.Id == id);
    }
}
