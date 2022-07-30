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
        IEnumerable<FileMetaData> GetPublicFilesMeta();
        Task<File> GetFileByIdAsync(int userId, int fileId);
        void SetFilePublic(int fileId);
        FileMetaData GetFileMetaDataByID(int userId, int fileId);
    }
}
