using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Hackaton.Data;
using Hackaton.Models.Advertisement;
using Microsoft.AspNetCore.Authorization;

public class AdvertisementController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdvertisementController(ApplicationDbContext context)
    {
        _context = context;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Index()
    {
        var advertisements = _context.AdvertisementData.ToList();
        return View(advertisements);
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AdvertisementData model)
    {
        if (ModelState.IsValid)
        {
            _context.AdvertisementData.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return View(model);
    }

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var advertisement = await _context.AdvertisementData.FindAsync(id);
        if (advertisement != null)
        {
            _context.AdvertisementData.Remove(advertisement);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }


    private bool AdvertisementExists(int id)
    {
        return _context.AdvertisementData.Any(e => e.Id == id);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var advertisement = await _context.AdvertisementData.FindAsync(id);
        if (advertisement == null)
        {
            return NotFound();
        }
        return View(advertisement);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var advertisement = await _context.AdvertisementData.FindAsync(id);
        if (advertisement == null)
        {
            return NotFound();
        }
        return View(advertisement);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, AdvertisementData model)
    {
        if (id != model.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdvertisementExists(model.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index");
        }
        return View(model);
    }
}
