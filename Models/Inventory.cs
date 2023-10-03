using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI.Models
{
    public class Inventory : BaseEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int StockQuantity { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
