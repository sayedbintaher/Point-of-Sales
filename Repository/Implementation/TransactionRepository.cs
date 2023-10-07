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
        public TransactionRepository(IMapper mapper, ApplicationDbContext db, IUnitOfWork unitOfWork, DapperContext dapper, IWebHostEnvironment host)
        {
            _mapper = mapper;
            _db = db;
            _unitOfWork = unitOfWork;
            _dapper = dapper;
            _host = host;
        }
        public async Task<object> AddTransaction(TransactionCreateVM model)
        {
            var customerExist = await _db.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.PhoneNo == model.PhoneNo && x.StatusId == (byte)StatusId.Active);
            if (customerExist != null)
            {
                return Utility.GetAlreadyExistMsg("Customer");
            }
            //Customer Part Here
            var mapCustomer = _mapper.Map<Customer>(model);
            mapCustomer.CreatedAt = CommonMethods.GetBDCurrentTime();
            mapCustomer.CreatedBy = 1;
            await _db.Customers.AddAsync(mapCustomer);

            //Transaction Part 
            var totalAmount = model.Items.Sum(x => x.SubTotal);
            model.TotalAmount = totalAmount;
            var mappedModel = _mapper.Map<Transaction>(model);
            mappedModel.CreatedBy = 1;
            mappedModel.CreatedAt = CommonMethods.GetBDCurrentTime();
            mappedModel.StatusId = (byte)StatusId.Active;
            mappedModel.Customer = mapCustomer;

            await _db.Transactions.AddAsync(mappedModel);

            foreach (var item in model.Items)
            {
                var mappedItem = _mapper.Map<TransactionItems>(item);
                var product = await _db.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == item.ProductId && x.StatusId == (byte)StatusId.Active);
                if (product == null)
                {
                    return Utility.GetValidationFailedMsg("Product not found");
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
            var reportResult = GenerateReport(model);
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

        public object GenerateReport(TransactionCreateVM model)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var data = model.Items;
            string rdlcFilePath = Path.Combine(_host.ContentRootPath, "Reports", "TransactionDetailsReport.rdlc");
            LocalReport report = new LocalReport(rdlcFilePath);
            report.AddDataSource("TransactionDetailsDS", data);

            var parameters = GetReportHeaderData(model.FullName, model.TransactionDate, model.PhoneNo);
            var reportResult = report.Execute(ReportUtility.GetRenderType(), 1, parameters);

            var result = new FileContentResult(reportResult.MainStream, MediaTypeNames.Application.Pdf);
            result.FileDownloadName = ReportUtility.GenerateReportName("PDF");
            return result;
        }

        public Dictionary<string, string> GetReportHeaderData(string customerName, DateTime? TransactionDate, string PhoneNo)
        {
            Dictionary<string, string> parameters = new()
            {
                {"CustomerName",customerName },
                {"InvoiceDate", TransactionDate != null ? TransactionDate.Value.ToString("dd-MMMM-yyyy") : ""},
                {"PhoneNo", PhoneNo }
            };
            return parameters;
        }
    }
}