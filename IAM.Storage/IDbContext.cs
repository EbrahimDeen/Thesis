using IAM.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAM.Storage
{
    public interface IDbContext
    {
        public IEnumerable<User> GetUsers();
        void AddUser(User user);
        int AddFile(File file, int userId);
        IEnumerable<FileMetaData> GetFilesMetaData(int userId);
        Task<File> GetFileByIdAsync(int userId, int id);
    }
}
