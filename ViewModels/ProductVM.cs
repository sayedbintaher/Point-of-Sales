using PosAPI.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace PosAPI.ViewModels
{
    public class ProductCreateVM
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
    public class ProductUpdateVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }

    public class ProductVM : CommonProperties
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }


}
