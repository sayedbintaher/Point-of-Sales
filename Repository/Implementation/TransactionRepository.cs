using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
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
using Transaction = PosAPI.Models.Transaction;

namespace PosAPI.Repository.Implementation
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DapperContext _dapper;
        public TransactionRepository(IMapper mapper, ApplicationDbContext db, IUnitOfWork unitOfWork, DapperContext dapper)
        {
            _mapper = mapper;
            _db = db;
            _unitOfWork = unitOfWork;
            _dapper = dapper;
        }
        public async Task<ServiceResponse> AddTransaction(TransactionCreateVM model)
        {
            var totalAmount = model.Items.Sum(x => x.SubTotal);
            var mappedModel =_mapper.Map<Transaction>(model);
            mappedModel.TotalAmount = totalAmount;
            mappedModel.CreatedBy = 1;
            mappedModel.CreatedAt = CommonMethods.GetBDCurrentTime();
            mappedModel.StatusId = (byte)StatusId.Active;
            await _db.Transactions.AddAsync(mappedModel);
            
            foreach(var item in model.Items){
                var mappedItem = _mapper.Map<TransactionItems>(item);
                var product = await _db.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == item.ProductId && x.StatusId == (byte)StatusId.Active);
                if(product == null){
                    return Utility.GetValidationFailedMsg("Product not found");
                }
                mappedItem.CreatedBy = 1;
                mappedItem.SubTotal = product.Price*item.Quantity;
                mappedItem.CreatedAt = CommonMethods.GetBDCurrentTime();
                mappedItem.StatusId = (byte)StatusId.Active;
                mappedItem.Transaction = mappedModel;
                
                product.Stock -= item.Quantity;
                await Task.Run(() => _db.Products.Update(product));
                await _db.TransactionItems.AddAsync(mappedItem);
            }
            await _unitOfWork.CommitAsync();
            return Utility.GetSuccessMsg("Successfully saved", model);
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
    }
}