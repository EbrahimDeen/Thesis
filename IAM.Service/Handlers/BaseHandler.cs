using IAM.Data.ResponseModel;
using System;

namespace IAM.API.Handlers
{
    public class BaseHandler
    {
        public Result<T> HandleResult<T>(Func<T> apiCall)
        {
            Result<T> result = new();
            try
            {
                var res = apiCall();
                result.Data = res;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
                result.Success = false;
            }
            return result;

        } 
    }
}
