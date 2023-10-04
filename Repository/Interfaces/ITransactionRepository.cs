using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PosAPI.Models.Common;
using PosAPI.ViewModels;

namespace PosAPI.Repository.Interfaces
{
    public interface ITransactionRepository
    {
        Task<ServiceResponse> AddTransaction(TransactionCreateVM model);
        Task<TransactionVM> GetTransactionById(int id);
    }
}