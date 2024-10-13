using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spedito.Areas.Admin.AdminViewModels;
using Spedito.Data;
using Spedito.Helpers;
using Spedito.Models;

namespace Spedito.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ProductController : Controller
    {
        private readonly PizzaDb _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(PizzaDb context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                //.Include(p => p.Category)
                .Where(p => !p.isDeleted).ToListAsync();
            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _context.Categories.Where(c=>!c.isDeleted).ToListAsync();
            var sizes = await _context.Sizes.Where(s=>!s.isDeleted).ToListAsync();
            var thicknesses = await _context.Thicknesses.Where(t=>!t.isDeleted).ToListAsync();

            var vm = new ProductViewModel
            {
                Categories = categories,
                Sizes = sizes,
                Thicknesses = thicknesses
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Categories = await _context.Categories.ToListAsync();
                return View(vm);
            }
         
            List<Image> images = new List<Image>();

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == vm.CategoryId);

            var product = new Product()
            {
                Name = vm.Name,
                Description = vm.Description,
                Category = category,
                InStock = vm.InStock,
                Price = vm.Price,
               
                //Size = vm.Size
            };

            var i = 0;
            if (vm.Files != null && vm.Files.Count > 0)
            {
                foreach (var file in vm.Files)
                {
                    if (!FileHelper.HasValidSize(file, 2))
                    {
                        ModelState.AddModelError("", "This file size couldn't support in here.");
                        return View(vm);
                    }
                    if (!FileHelper.IsImage(file))
                    {
                        ModelState.AddModelError("", "Only image type allowed");
                    }
                    var root = _env.WebRootPath;
                    var path = @"img\products\";
                    var filename = Guid.NewGuid().ToString() + file.FileName;
                    var fullpath = Path.Combine(root, path, filename);

                    using (FileStream stream = new(fullpath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    


                    Image newImage = new Image
                    {
                        ImagePath = filename,
                        IsMain = i == 0,
                        Product = product
                    };
                    images.Add(newImage);
                    
                }
                i++;
            }

            

            product.Images = images;
            

            product.CreatedAt = DateTime.UtcNow.AddHours(4);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            product.isDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
