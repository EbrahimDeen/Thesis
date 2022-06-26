using IAM.Data.Models;
using IAM.Data.RequestModels;
using IAM.Storage.Providers;

namespace IAM.API.Services
{
    public class AnalysisService : IAnalysisService
    {
        IAnalysisProvider Provider;
        public AnalysisService(IAnalysisProvider provider)
        {
            Provider = provider;
        }
        public void FileDownloaded(AnalysisModel requestInfo)
        {
            
            //Provider.FileDownloaded()
            Provider.FileDownloaded(requestInfo);
        }
        public object GetFileAnalysis(int fileId, int userId)
        {
            return Provider.GetFileAnalysis(fileId, userId);
        }
    }
}
