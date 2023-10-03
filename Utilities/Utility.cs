using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PosAPI.Models.Common;

namespace PosAPI.Utilities
{
    public static class Utility
    {
        public static ServiceResponse GetSuccessMsg(string message, object data = null)
        {
            return new ServiceResponse
            {
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                Message = message,
                Data = data
            };
        }

        public static ServiceResponse GetAlreadyExistMsg(string? module = "Data")
        {
            return new ServiceResponse
            {
                Success = false,
                StatusCode = StatusCodes.Status409Conflict,
                Message = module + " already exist!",
                Data = null
            };
        }

        public static ServiceResponse GetErrorMsg(string message)
        {
            return new ServiceResponse
            {
                Success = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = message
            };
        }

        public static ServiceResponse GetInternalServerErrorMsg(Exception ex, string customMessage = null)
        {
            return new ServiceResponse
            {
                Success = false,
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = string.IsNullOrWhiteSpace(customMessage) ? (ex.Message + (ex.InnerException != null ? " --> InnerException: " + ex.InnerException.Message : "")) : customMessage,
                Data = null
            };
        }

        public static ServiceResponse GetValidationFailedMsg(List<string> msg)
        {
            return new ServiceResponse
            {
                Success = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Validation failed",
                Data = msg
            };
        }

        public static ServiceResponse GetValidationFailedMsg(string msg)
        {
            return new ServiceResponse
            {
                Success = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Validation failed",
                Data = msg
            };
        }

        public static ServiceResponse GetNoDataFoundMsg(string? msg = "No data found!")
        {
            return new ServiceResponse
            {
                Success = false,
                StatusCode = StatusCodes.Status404NotFound,
                Message = msg,
                Data = null
            };
        }
    }
}