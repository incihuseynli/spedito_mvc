using Spedito.Models;

namespace Spedito.Areas.Admin.AdminViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }
        public int InStock { get; set; }
        public List<Category>? Categories { get; set; }
        public int CategoryId { get; set; }
        public List<Size>? Sizes { get; set; }
        public List<Thickness>? Thicknesses { get; set; }
        public List<NutritionalValue>? NutritionalValues { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
