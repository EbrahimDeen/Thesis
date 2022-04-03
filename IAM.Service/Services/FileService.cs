using IAM.Data.Models;
using IAM.Storage.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<File> GetFileByIdAsync(int userId, int ID)
        {
            var data = await Provider.GetFileByIdAsync(userId, ID);
            return data;
        }

        public IEnumerable<FileMetaData> GetFilesMetaData(int userId)
        {
            return Provider.GetFilesMetaData(userId);
        }

        public int SaveFile(File file, int userId)
        {
            return Provider.AddFile(file, userId);
            
        }
    }
}
