using IAM.Data.Models;

namespace IAM.Storage.Providers
{
    public interface IAnalysisProvider
    {
        void FileDownloaded(AnalysisModel request);
        object GetFileAnalysis(int fileId);
    }
}
