using Spedito.Models.Base;

namespace Spedito.Models
{
    public class NutritionalValue:BaseModel
    {
        public string Name { get; set; }
        public int Value { get; set; }

        // relations
        // One To Many

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
