using Spedito.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spedito.Models
{
    public class Ingredient:BaseModel
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public double Price { get; set; }
        public int CookTime { get; set; }
        public int InStock { get; set; }
        [NotMapped]
        public IFormFile? File { get; set; }
        //relations

        // Many to Many
        [ForeignKey(nameof(Image))]
        public int ImageId { get; set; }
        public Image Image { get; set; }
        public List<Product> Products { get; set; }

    }
}
