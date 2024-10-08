namespace Spedito.Models.Base
{
    public class BaseModel
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
