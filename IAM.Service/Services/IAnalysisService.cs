using IAM.Data.Models;

namespace IAM.API.Services
{
    public interface IAnalysisService
    {
        void FileDownloaded(AnalysisModel requestInfo);
        object GetFileAnalysis(int fileId);
    }
}
