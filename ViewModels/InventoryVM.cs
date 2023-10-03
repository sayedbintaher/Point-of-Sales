using PosAPI.ViewModels.Common;

namespace PosAPI.ViewModels
{

    public class InventoryCreateVM
    {
        public int ProductId { get; set; }
        public int StockQuantity { get; set; }
    }
    public class InventoryUpdateVM
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int StockQuantity { get; set; }
    }

    public class InventoryVM : CommonProperties
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int StockQuantity { get; set; }
    }
}
