using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using adopmascotas.Data;
using adopmascotas.Models;
using System.Linq;
using System.Threading.Tasks;

public class AdoptionsController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdoptionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Adoptions/Create
    public async Task<IActionResult> Create()
    {
        // Solo mascotas no adoptadas
        ViewBag.Pets = await _context.Pets
            .Where(p => !p.IsAdopted)
            .ToListAsync();

        ViewBag.Adopters = await _context.Adopters.ToListAsync();

        return View();
    }

    // POST: Adoptions/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int petId, int adopterId)
    {
        var pet = await _context.Pets.FindAsync(petId);
        var adopter = await _context.Adopters.FindAsync(adopterId);

        if (pet == null || adopter == null)
        {
            return NotFound();
        }

        if (pet.IsAdopted)
        {
            ModelState.AddModelError("", "Esta mascota ya fue adoptada.");
            return RedirectToAction(nameof(Create));
        }

        var adoption = new Adoption
        {
            PetId = petId,
            AdopterId = adopterId
        };

        _context.Adoptions.Add(adoption);
        pet.IsAdopted = true;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: Adoptions
    public async Task<IActionResult> Index()
    {
        var adoptions = await _context.Adoptions
            .Include(a => a.Pet)
            .Include(a => a.Adopter)
            .ToListAsync();

        return View(adoptions);
    }
}
