using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PosAPI.Models.Common;
using PosAPI.ViewModels;

namespace PosAPI.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        Task<CustomerVM> GetCustomerById(int id);
        Task<ServiceResponse> GetAllCustomerList();
        Task<ServiceResponse> AddCustomer(CustomerCreateVM model);
        Task<ServiceResponse> UpdateCustomer(CustomerUpdateVM model);
        Task<ServiceResponse> DeleteCustomer(int id);
    }
}