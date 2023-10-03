using PosAPI.ViewModels.Common;

namespace PosAPI.ViewModels
{

    public class TransactionCreateVM
    {
        public DateTime TransactionDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int PaymentMethodId { get; set; }
        public List<TransactionItemCreateVM> Items { get; set; }
    }
    public class TransactionUpdateVM
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int PaymentMethodId { get; set; }
    }
    public class TransactionVM : CommonProperties
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }
    }
}
