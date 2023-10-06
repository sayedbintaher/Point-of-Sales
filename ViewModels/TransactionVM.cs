using PosAPI.ViewModels.Common;
using System.Text.Json.Serialization;

namespace PosAPI.ViewModels
{

    public class TransactionCreateVM
    {
        public string FullName { get; set; }
        public string PhoneNo { get; set; }
        public DateTime TransactionDate { get; set; }
        public int PaymentMethodId { get; set; }
        public List<TransactionItemCreateVM> Items { get; set; }
        [JsonIgnore]
        public Decimal TotalAmount { get; set; }
    }
    public class TransactionUpdateVM
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int PaymentMethodId { get; set; }
        public List<TransactionItemCreateVM> Items { get; set; }
    }
    public class TransactionVM : CommonProperties
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }
        public List<TransactionItemVM> Items { get; set; }
    }
}
