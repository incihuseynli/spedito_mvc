using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spedito.Data;
using Spedito.Models;
using System.Drawing;

namespace Spedito.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ThicknessController : Controller
    {
        private readonly PizzaDb _context;

        public ThicknessController(PizzaDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var thickness = await _context.Thicknesses.Where(t => !t.isDeleted).ToListAsync();
            return View(thickness);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Thickness thickness)
        {
            if (!ModelState.IsValid) return View(thickness);
            if (thickness.Name is null)
            {
                return NotFound();
            }
            thickness.CreatedAt = DateTime.UtcNow.AddHours(4);
            await _context.Thicknesses.AddAsync(thickness);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            var thickness = await _context.Thicknesses.FirstOrDefaultAsync(t => t.Id == id);
            if (thickness == null)
            {
                return NotFound();
            }
            return View(thickness);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Thickness newThickness)
        {
            var thickness = await _context.Thicknesses.FirstOrDefaultAsync(t => t.Id == id);
            if (thickness == null)
            {
                return NotFound();
            }
            thickness.Name = newThickness.Name;
            thickness.UpdatedAt = DateTime.UtcNow.AddHours(4);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var thickness = await _context.Thicknesses.FirstOrDefaultAsync(t => t.Id == id);
            thickness.isDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
