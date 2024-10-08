using Spedito.Models.Base;

namespace Spedito.Models
{
    public class Thickness:BaseModel
    {
        public string Name { get; set; }

        // Many to Many
        // relations
        public List<Product> Products { get; set; }
    }
}
