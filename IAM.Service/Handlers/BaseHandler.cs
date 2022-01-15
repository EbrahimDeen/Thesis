using IAM.Data.ResponseModel;
using System;

namespace IAM.API.Handlers
{
    public class BaseHandler
    {
        public object ExecuteTryCatch(Action apiCall)
        {
            try
            {
                apiCall();
            }
            catch (Exception ex)
            {
                //logError;
                return ex;
            }
            return null;
        } 
    }
}
