using IAM.Data.ResponseModel;
using System;

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
    }
}
