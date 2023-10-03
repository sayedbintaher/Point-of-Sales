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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpPost("AddCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerCreateVM customer)
        {
            try
            {
                var validationResult = new CustomerCreateValidator().Validate(customer);
                if (validationResult.IsValid)
                {
                    var response = await _customerRepository.AddCustomer(customer);
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

        [HttpPut("UpdateCustomer/{id}")]
        public async Task<IActionResult> updateCustomer(int id, [FromBody] CustomerUpdateVM customer)
        {
            try
            {
                if (id != customer.Id)
                {
                    var response = Utility.GetValidationFailedMsg("Id must be same as provided Id.");
                    return StatusCode((int)response.StatusCode, response);
                }
                var validationResult = new CustomerUpdateValidator().Validate(customer);
                if (validationResult.IsValid)
                {
                    var response = await _customerRepository.UpdateCustomer(customer);
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

        [HttpGet("CustomerGetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var response = await _customerRepository.GetCustomerById(id);
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


        [HttpGet("CustomerGetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _customerRepository.GetAllCustomerList();
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

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _customerRepository.DeleteCustomer(id);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utility.GetInternalServerErrorMsg(ex));
            }
        }
    }
}