using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AspNetCore.Reporting;
using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosAPI.Data;
using PosAPI.Models;
using PosAPI.Models.Common;
using PosAPI.Repository.Interfaces;
using PosAPI.UnitOfWork;
using PosAPI.Utilities;
using PosAPI.ViewModels;
using static PosAPI.Models.Common.AppConstants;
using Transaction = PosAPI.Models.Transaction;

namespace PosAPI.Repository.Implementation
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DapperContext _dapper;
        private readonly IWebHostEnvironment _host;
        private readonly IReportRepository _report;
        public TransactionRepository(IMapper mapper, ApplicationDbContext db, IUnitOfWork unitOfWork, DapperContext dapper, IWebHostEnvironment host, IReportRepository report)
        {
            _mapper = mapper;
            _db = db;
            _unitOfWork = unitOfWork;
            _dapper = dapper;
            _host = host;
            _report = report;
        }
        public async Task<object> AddTransaction(TransactionCreateVM model)
        {

            //Customer Part Here
            var mapCustomer = _mapper.Map<Customer>(model);
            var customerExist = await _db.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.PhoneNo == model.PhoneNo);
            if (customerExist == null)
            { 
                mapCustomer.CreatedAt = CommonMethods.GetBDCurrentTime();
                mapCustomer.CreatedBy = 1;
                mapCustomer.StatusId = (byte)StatusId.Active;
                await _db.Customers.AddAsync(mapCustomer);
            }

            //Transaction Part 
            var totalAmount = model.Items.Sum(x => x.SubTotal);
            var mappedModel = _mapper.Map<Transaction>(model);
            mappedModel.TotalAmount = totalAmount;
            mappedModel.InvoiceNo = GenerateRandomNumberSequence(6);
            mappedModel.CreatedBy = 1;
            mappedModel.CreatedAt = CommonMethods.GetBDCurrentTime();
            mappedModel.StatusId = (byte)StatusId.Active;
            if(customerExist != null)
            {
                mappedModel.CustomerId = customerExist.Id;
            }
            else
            {
                mappedModel.Customer = mapCustomer;
            }

            await _db.Transactions.AddAsync(mappedModel);

            foreach (var item in model.Items)
            {
                var mappedItem = _mapper.Map<TransactionItems>(item);
                var product = await _db.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == item.ProductId && x.StatusId == (byte)StatusId.Active);
                if (product == null)
                {
                    return Utility.GetValidationFailedMsg("Product not found");
                }
                if(product.Stock < item.Quantity && product.Stock <= 0)
                {
                    return Utility.GetValidationFailedMsg("Product not enough in stock");
                }
                mappedItem.CreatedBy = 1;
                mappedItem.SubTotal = product.Price * item.Quantity;
                mappedItem.CreatedAt = CommonMethods.GetBDCurrentTime();
                mappedItem.StatusId = (byte)StatusId.Active;
                mappedItem.Transaction = mappedModel;

                product.Stock -= item.Quantity;
                await Task.Run(() => _db.Products.Update(product));
                await _db.TransactionItems.AddAsync(mappedItem);
            }
            await _unitOfWork.CommitAsync();
            var reportResult = await _report.GetInvoiceReport(mappedModel.Id);
            return reportResult;
        }

        public async Task<TransactionVM> GetTransactionById(int id)
        {
            var transaction = new TransactionVM();
            var sp = "sp_Transaction_GetById";
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@id", id);
            var data = await _dapper.GetAsyncMultiple<TransactionVM, TransactionItemVM>(sp, parms);
            transaction = data.Item1;
            transaction.Items = data.Item2;
            return transaction;
        }

        public static string GenerateRandomNumberSequence(int length)
        {
            string characterPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random random = new Random();
            char[] randomChars = new char[length];
            string result = "";

            for (int i = 0; i < length; i++)
            {
                int randomNumber = random.Next(0, 10); // Generate a random number between 0 and 9
                result += randomNumber.ToString();
                int index = random.Next(0, characterPool.Length);
                randomChars[i] = characterPool[index];
            }
            string str = new string(randomChars);
            return str+"-"+result;
        }
    }
}