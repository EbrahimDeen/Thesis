using IAM.Data.Models;

namespace IAM.API.Services
{
    public interface IFileService
    {
        int SaveFile(File file);
        File GetFile(string fileName, string fileExt);
        File GetFileById(int ID);
    }
}
