using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using PosAPI.Data;
using PosAPI.Models;
using PosAPI.Models.Common;
using PosAPI.Repository.Interfaces;
using PosAPI.UnitOfWork;
using PosAPI.Utilities;
using PosAPI.ViewModels;
using static PosAPI.Models.Common.AppConstants;

namespace PosAPI.Repository.Implementation
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DapperContext _dapper;
        public CustomerRepository(ApplicationDbContext db, IUnitOfWork unitOfWork, IMapper mapper, DapperContext dapper)
        {
            _db = db;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dapper = dapper;
        }
        public async Task<ServiceResponse> AddCustomer(CustomerCreateVM model)
        {
            var customerExist = await _db.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.PhoneNo == model.PhoneNo && x.StatusId == (byte)StatusId.Active);
            if (customerExist != null)
            {
                return Utility.GetAlreadyExistMsg("Customer");
            }
            var customerToCreate = _mapper.Map<Customer>(model);
            customerToCreate.StatusId = (byte)StatusId.Active;
            customerToCreate.CreatedBy = 1;
            customerToCreate.CreatedAt = CommonMethods.GetBDCurrentTime();
            await _db.AddAsync(customerToCreate);
            await _unitOfWork.CommitAsync();

            return Utility.GetSuccessMsg("Successfully saved", customerToCreate);
        }

        public async Task<ServiceResponse> DeleteCustomer(int id)
        {
            var customerToDelete = await _db.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.StatusId == (byte)StatusId.Active);
            if (customerToDelete == null)
            {
                return Utility.GetNoDataFoundMsg("Product not found");
            }
            customerToDelete.UpdatedBy = 1;
            customerToDelete.UpdatedAt = CommonMethods.GetBDCurrentTime();
            customerToDelete.StatusId = (byte)StatusId.Delete;
            await Task.Run(() => _db.Customers.Update(customerToDelete));
            await _unitOfWork.CommitAsync();

            return Utility.GetSuccessMsg("Successfully deleted", customerToDelete);
        }

        public async Task<ServiceResponse> GetAllCustomerList()
        {
            using IDbConnection dbConnection = _dapper.CreateConnection();
            string sp = "exec sp_Customer_GetAll";
            var products = await dbConnection.QueryAsync<CustomerVM>(sp, CommandType.StoredProcedure);
            //var response = _mapper.Map<ProductVM>(product);
            return Utility.GetSuccessMsg("Success", products.ToList());
        }

        public async Task<CustomerVM> GetCustomerById(int id)
        {
            using IDbConnection dbConnection = _dapper.CreateConnection();
            var sp = "exec sp_Customer_GetById @id";
            var parameters = new { id = id };
            var customer = await dbConnection.QueryFirstOrDefaultAsync<CustomerVM>(sp, parameters);
            if (customer == null)
            {
                return null;
            }
            return customer;
        }

        public async Task<ServiceResponse> UpdateCustomer(CustomerUpdateVM model)
        {
            var customerToUpdate = await _db.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == model.Id && x.StatusId == (byte)StatusId.Active);
            if (customerToUpdate == null)
            {
                return Utility.GetNoDataFoundMsg("Product not found");
            }
            _mapper.Map(model, customerToUpdate);
            customerToUpdate.UpdatedBy = 1;
            customerToUpdate.UpdatedAt = CommonMethods.GetBDCurrentTime();
            await Task.Run(() => _db.Customers.Update(customerToUpdate));
            await _unitOfWork.CommitAsync();

            return Utility.GetSuccessMsg("Successfully updated", customerToUpdate);
        }
    }
}