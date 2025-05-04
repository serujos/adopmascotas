using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using adopmascotas.Data;  
using adopmascotas.Models; 

public class PetsController : Controller
{
    private readonly ApplicationDbContext _context;

    public PetsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Pets/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Pets/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Pet pet)
    {
        if (ModelState.IsValid)
        {
             
            _context.Add(pet);  
            await _context.SaveChangesAsync();  
            return RedirectToAction("Index", "Home");  
        }
        return View(pet);  
    }
}
