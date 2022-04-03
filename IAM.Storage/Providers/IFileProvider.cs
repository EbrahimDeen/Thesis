using IAM.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAM.Storage.Providers
{
    public interface IFileProvider
    {
        int AddFile(File file, int userId);
        IEnumerable<FileMetaData> GetFilesMetaData(int userId);
        Task<File> GetFileByIdAsync(int userId, int fileId);
    }
}
