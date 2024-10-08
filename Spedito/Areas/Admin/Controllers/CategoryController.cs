using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spedito.Data;
using Spedito.Helpers;
using Spedito.Models;
using System.Security.AccessControl;
using System.IO;

namespace Spedito.Areas.Admin.Controllers
{
    [Area("admin")]
    public class CategoryController : Controller
    {
        private readonly PizzaDb _context;
        private readonly IWebHostEnvironment _env;

        public CategoryController(PizzaDb context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.Where(c => !c.isDeleted).ToListAsync();
            return View(categories);
        }
        public async Task<IActionResult> Create()
        {
            //var categories = await _context.Categories.Where(c => !c.isDeleted).ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid) return View(category);

            category.CreatedAt = DateTime.UtcNow.AddHours(4);

            if (!FileHelper.HasValidSize(category.File, 2))
            {
                ModelState.AddModelError("", "This file size couldn't support in here.");
                return View(category);
            }
            if (!FileHelper.IsImage(category.File))
            {
                ModelState.AddModelError("", "Only image type allowed");
            }

            var root = _env.WebRootPath;
            var path = @"img\categoryIcons\";
            var filename = Guid.NewGuid().ToString() + category.File.FileName;
            var fullpath = Path.Combine(root, path, filename);

            using (FileStream stream = new(fullpath, FileMode.Create))
            {
                await category.File.CopyToAsync(stream);
            }

            Image image = new Image();
            image.ImagePath = filename;

            category.Image = image;
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category is null)
            {
                return NotFound();
            }
            category.isDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category is null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Category model)
        {
            var category = await _context.Categories.Include(c=> c.Image).FirstOrDefaultAsync(c => c.Id == id);
            if (model is null)
            {
                return NotFound();
            }
            category.Name = model.Name;

            category.UpdatedAt = DateTime.UtcNow.AddHours(4);

            if (!FileHelper.HasValidSize(model.File, 2))
            {
                ModelState.AddModelError("", "This file size couldn't support in here.");
                return View(model);
            }
            if (!FileHelper.IsImage(model.File))
            {
                ModelState.AddModelError("", "Only image type allowed");
            }

            var root = _env.WebRootPath;
            var path = @"img\categoryIcons\";
            var filename = Guid.NewGuid().ToString() + model.File.FileName;
            var fullpath = Path.Combine(root, path, filename);

            using (FileStream stream = new(fullpath, FileMode.Create))
            {
                await model.File.CopyToAsync(stream);
            }
            
            var oldImgPath = Path.Combine(root, path, category.Image.ImagePath);

            if (System.IO.File.Exists(oldImgPath))
            {
                System.IO.File.Delete(oldImgPath);
            }

            Image image = new Image();
            image = category.Image;
            image.ImagePath = filename;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
