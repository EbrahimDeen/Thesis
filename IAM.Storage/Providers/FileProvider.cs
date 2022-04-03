using IAM.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAM.Storage.Providers
{
    public class FileProvider : IFileProvider
    {
        IDbContext Context;

        public FileProvider(IDbContext context)
        {
            Context = context;
        }
        public int AddFile(File file, int userId)
        {
            return Context.AddFile(file,userId);
        }

        public Task<File> GetFileByIdAsync(int userId, int fileId)
        {
            return Context.GetFileByIdAsync(userId,fileId);
        }

        public IEnumerable<FileMetaData> GetFilesMetaData(int userId)
        {
            return Context.GetFilesMetaData(userId);
        }
    }
}
