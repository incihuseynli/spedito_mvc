using Spedito.Models.Base;

namespace Spedito.Models
{
    public class Size:BaseModel
    {
        public string Name { get; set; }

        // Many to Many
        // relations
        public List<Product>? Products { get; set; }
    }
}
