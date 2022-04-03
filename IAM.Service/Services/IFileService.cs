using IAM.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IAM.API.Services
{
    public interface IFileService
    {
        int SaveFile(File file, int userId);
        File GetFile(string fileName, string fileExt);
        Task<File> GetFileByIdAsync(int userId, int ID);
        IEnumerable<FileMetaData> GetFilesMetaData(int iD);
    }
}
