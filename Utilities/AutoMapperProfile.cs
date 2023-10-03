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

        }
    }
}
