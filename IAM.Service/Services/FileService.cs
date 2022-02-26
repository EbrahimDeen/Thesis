using IAM.Data.Models;
using IAM.Storage.Providers;

namespace IAM.API.Services
{
    public class FileService : IFileService
    {
        readonly IFileProvider Provider;
        public FileService(IFileProvider provider)
        {
            Provider = provider;
        }
        public File GetFile(string fileName, string fileExt)
        {
            throw new System.NotImplementedException();
        }

        public File GetFileById(int ID)
        {
            throw new System.NotImplementedException();
        }

        public int SaveFile(File file)
        {
            return Provider.AddFile(file);
            
        }
    }
}
