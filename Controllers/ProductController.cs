using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PosAPI.Models;
using PosAPI.Models.Common;
using PosAPI.Repository.Interfaces;
using PosAPI.Utilities;
using PosAPI.Validator;
using PosAPI.ViewModels;

namespace PosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] ProductCreateVM product)
        {
            try
            {
                var validationResult = new ProductCreateValidator().Validate(product);
                if (validationResult.IsValid)
                {
                    var response = await _productRepository.AddProduct(product);
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

        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> updateProduct(int id, [FromBody] ProductUpdateVM product)
        {
            try
            {
                if (id != product.Id)
                {
                    var response = Utility.GetValidationFailedMsg("Id must be same as provided Id.");
                    return StatusCode((int)response.StatusCode, response);
                }
                var validationResult = new ProductUpdateValidator().Validate(product);
                if (validationResult.IsValid)
                {
                    var response = await _productRepository.UpdateProduct(product);
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

        [HttpGet("ProductGetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var response = await _productRepository.GetProductById(id);
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


        [HttpGet("ProductGetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _productRepository.GetAllProductList();
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
                var response = await _productRepository.DeleteProduct(id);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utility.GetInternalServerErrorMsg(ex));
            }
        }

    }
}
