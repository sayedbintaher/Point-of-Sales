using PosAPI.ViewModels.Common;

namespace PosAPI.ViewModels
{
    public class TransactionItemCreateVM
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
    }

    public class TransactionItemUpdateVM
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
    }

    public class TransactionItemVM : CommonProperties
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public int ProductId { get; set; }
        public string ProductName {get; set;}
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
    }
}
