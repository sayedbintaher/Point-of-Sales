using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI.Models
{
    public class TransactionItems : BaseEntity
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Transaction Transaction { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
