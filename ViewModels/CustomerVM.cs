using PosAPI.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace PosAPI.ViewModels
{
    public class CustomerCreateVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string PhoneNo { get; set; }
        public string? Address { get; set; }
    }

    public class CustomerUpdateVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string PhoneNo { get; set; }
        public string? Address { get; set; }
    }

    public class CustomerVM : CommonProperties
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string PhoneNo { get; set; }
        public string? Address { get; set; }
    }
}
