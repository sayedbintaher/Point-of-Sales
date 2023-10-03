using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI.Models
{
    public class Customer : BaseEntity
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(100)]
        public string? Email { get; set; }
        [StringLength(20)]
        public string PhoneNo { get; set; }
        [StringLength(500)]
        public string? Address { get; set; }
    }
}
