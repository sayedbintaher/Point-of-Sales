using AutoMapper;
using PosAPI.Models;
using PosAPI.ViewModels;

namespace PosAPI.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Mapper for Product
            CreateMap<Product, ProductCreateVM>().ReverseMap();
            CreateMap<Product, ProductUpdateVM>().ReverseMap();
            CreateMap<Product, ProductVM>().ReverseMap();

            //Mapper for Customer
            CreateMap<Customer, CustomerCreateVM>().ReverseMap();
            CreateMap<Customer, CustomerUpdateVM>().ReverseMap();
            CreateMap<Customer, CustomerVM>().ReverseMap();

            //Mapper for Transaction
            CreateMap<Transaction, TransactionCreateVM>().ReverseMap();
            CreateMap<Transaction, TransactionUpdateVM>().ReverseMap();
            CreateMap<Transaction, TransactionVM>().ReverseMap();

             //Mapper for Transaction Items
            CreateMap<TransactionItems, TransactionItemCreateVM>().ReverseMap();
            CreateMap<TransactionItems, TransactionItemUpdateVM>().ReverseMap();
            CreateMap<TransactionItems, TransactionItemVM>().ReverseMap();

        }
    }
}
