using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAPI.Models
{
    public class Transaction : BaseEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int PaymentMethodId { get; set; }
        [ForeignKey("PaymentMethodId")]
        public virtual PaymentMethod PaymentMethod { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        public List<TransactionItems> TransactionItems { get; set; }
    }
}
