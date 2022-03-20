using IAM.Data.ResponseModel;
using System;
using System.Threading.Tasks;

namespace IAM.API.Handlers
{
    public class BaseHandler
    {
        public object ExecuteTryCatch(Action apiCall, string exceptionLogMessage = "")
        {
            try
            {
                apiCall();
            }
            catch (Exception ex)
            {
                //logError with exceptionLogMessage;
                return ex;
            }
            return null;
        }
        public async Task<object> ExecuteTryCatchAsync(Func<Task> apiCall, string exceptionLogMessage = "")
        {
            try
            {
                await apiCall();
            }
            catch (Exception ex)
            {
                //logError with exceptionLogMessage;
                return ex;
            }
            return null;
        }

    }
}
