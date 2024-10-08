using Spedito.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spedito.Models
{
    public class Category : BaseModel
    {
        [NotMapped]
        public IFormFile? File { get; set; }
        public string Name { get; set; }
        // relations
        [ForeignKey(nameof(Image))]
        public int ImageId { get; set; }
        public Image? Image { get; set; }
        public List<Product>? Products { get; set; }

    }
}
