using IAM.Data.Models;
using System.Collections.Generic;

namespace IAM.API.Services
{
    public interface IFileService
    {
        int SaveFile(File file, int userId);
        File GetFile(string fileName, string fileExt);
        File GetFileById(int ID);
        List<FileMetaData> GetFilesMetaData(int iD);
    }
}
