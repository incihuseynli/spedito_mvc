using Spedito.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spedito.Models
{
    public class Image : BaseModel
    {
        public string ImagePath { get; set; }
        // relations
        // add cover for main image

        //public bool Cover { get; set; }
        public bool IsMain { get; set; }
        public Category? Category { get; set; }
        public Ingredient? Ingredient { get; set; }
        [ForeignKey(nameof(Product))]
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
