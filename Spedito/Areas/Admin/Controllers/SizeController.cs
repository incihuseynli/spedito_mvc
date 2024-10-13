using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spedito.Data;
using Spedito.Models;

namespace Spedito.Areas.Admin.Controllers
{
    [Area("admin")]
    public class SizeController : Controller
    {
        private readonly PizzaDb _context;
        private readonly IWebHostEnvironment _env;

        public SizeController(PizzaDb context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var sizes = await _context.Sizes
                .Where(s => !s.isDeleted).ToListAsync();
            return View(sizes);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Size size)
        {
            if(!ModelState.IsValid) return View(size);
            if(size.Name is null)
            {
                return NotFound();
            }
            size.CreatedAt = DateTime.UtcNow.AddHours(4);
            await _context.Sizes.AddAsync(size);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            var size = await _context.Sizes.FirstOrDefaultAsync(s => s.Id == id);
            if (size == null) { 
                return NotFound();
            }
            return View(size);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Size newSize)
        {
             var size = await _context.Sizes.FirstOrDefaultAsync(s => s.Id == id);
            if (size == null)
            {
                return NotFound();
            }
            size.Name = newSize.Name;
            size.UpdatedAt = DateTime.UtcNow.AddHours(4);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var size = await _context.Sizes.FirstOrDefaultAsync(s => s.Id == id);
            size.isDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
