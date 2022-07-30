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
        FileMetaData GetFileMetaDataByID(int userId, int fileId);
        IEnumerable<FileMetaData> GetPublicFilesMeta();
        Task<File> GetFileByIdAsync(int userId, int id);
        void AddFileDownloadedAnalysis(string IP, string countryname, string cityname, string continentname, int downloadedby, int fileId);
        IEnumerable<AnalysisModel> GetFileAnalysisAsync(int fileId, int userId);
        void SetFilePublic(int fileId);
    }
}
