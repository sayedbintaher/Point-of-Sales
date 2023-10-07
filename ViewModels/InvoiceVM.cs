namespace PosAPI.ViewModels
{
    public class InvoiceVM
    {
        public string CustomerName { get; set; }
        public string PhoneNo { get; set; }
        public int Quantity { get; set; }
        public Decimal SubTotal { get; set; }
        public string ProductName { get; set; }
        public DateTime TransactionDate { get; set; }
        public string PaymentMethod { get; set; }
        public string InvoiceNo { get; set; }
        public Decimal Total { get; set; }
    }
}
