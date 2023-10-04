using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PosAPI.Repository.Interfaces;
using PosAPI.Utilities;
using PosAPI.Validator;
using PosAPI.ViewModels;

namespace PosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmOrder(TransactionCreateVM transaction)
        {
            try
            {
                var validationResult = new TransactionCreateValidator().Validate(transaction);
                if (validationResult.IsValid)
                {
                    var response = await _transactionRepository.AddTransaction(transaction);
                    return StatusCode((int)response.StatusCode, response);
                }
                else
                {
                    var response = Utility.GetValidationFailedMsg(FluentValidationHelper.GetErrorMessage(validationResult.Errors));
                    return StatusCode((int)response.StatusCode, response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utility.GetInternalServerErrorMsg(ex));
            }
        }

        [HttpGet("TransactionGetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var response = await _transactionRepository.GetTransactionById(id);
                if (response == null)
                {
                    var msg = Utility.GetNoDataFoundMsg();
                    return StatusCode((int)msg.StatusCode, msg);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utility.GetInternalServerErrorMsg(ex));
            }
        }
    }
}