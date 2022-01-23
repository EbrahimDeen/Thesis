using IAM.Data.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace IAM.API.Controllers
{
    public class BaseController : ControllerBase
    {
        public IConfiguration Configuration;
        public BaseController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        internal ObjectResult StatusCodeResult<T>(Result<T> result)
        {
            return StatusCode(result.StatusCode, result);
        }

        internal static Result<T> GetResult<T>(T res)
        {
            Result<T> result = new();


            if(res is UnauthorizedAccessException accessException)
            {
                result.Error = accessException.Message;
                result.Success = false;
                result.StatusCode = 401;

            }
            else if (res is ArgumentException ar)
            {
                result.Error = ar.Message;
                result.Success = false;
                result.StatusCode = 400;

            }
            else if (res is Exception ex)
            {
                result.Error = ex.Message;
                result.Success = false;
                result.StatusCode = 500;

            }
            else
            {
                result.Data = res;
            }
            return result; 
            //try
            //{
            //    var res = apiCall();
            //    result.Data = res;
            //    return Ok(result);
            //}
            //catch (UnauthorizedAccessException ex)
            //{
            //    result.Error = ex.Message;
            //    result.Success = false;
            //    result.StatusCode = 401;
            //}
            //catch (Exception ex)
            //{
            //    result.Error = ex.Message;
            //    result.Success = false;
            //    result.StatusCode = 500;
            //}
           // return StatusCode(result.StatusCode, result);
            //return result;

        }
        //internal static Result<object> GetResult()
        //{
        //    return GetResult<object>(null);
        //}
        
    }
}
