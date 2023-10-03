using System.ComponentModel.DataAnnotations;

namespace PosAPI.Models
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [StringLength(200)]
        public string? Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
