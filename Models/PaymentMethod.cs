using System.ComponentModel.DataAnnotations;

namespace PosAPI.Models
{
    public class PaymentMethod : BaseEntity
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(300)]
        public string? Description { get; set; }
    }
}
