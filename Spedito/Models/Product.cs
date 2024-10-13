using Spedito.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spedito.Models
{
    public class Product:BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
      
        public double Price { get; set; }
        public int InStock { get; set; }
        [NotMapped]
        public List<IFormFile>? Files { get; set; }
        // relations


        public List<Size>? Sizes { get; set; }
        public List<Thickness>? Thicknesses { get; set; }
        public List<Ingredient>? Ingredients { get; set; }

    
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public List<Image>? Images { get; set; }

        public List<NutritionalValue>? NutritionalValues { get; set; }
    }
}
