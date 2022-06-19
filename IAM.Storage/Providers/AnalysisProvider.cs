using IAM.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IAM.Storage.Providers
{
    public class AnalysisProvider : IAnalysisProvider
    {
        IDbContext Context;
        UserProvider UserProvider;
        static List<AnalysisTempDB.Analysis> DB = new List<AnalysisTempDB.Analysis>();

        public AnalysisProvider(IDbContext context)
        {
            UserProvider = new UserProvider(context);
        }

        public void FileDownloaded(AnalysisModel request)
        {
            DB.Add(new AnalysisTempDB.Analysis()
            {
                ID = new Guid().ToString(),
                IP = request.IP,
                DownloadedBy = request.DownloadedBy.ToString(),
                FileID = request.FileID.ToString(),
                CountryName = request.CountryName,
                CityName = request.CityName,
                ContinentName = request.ContinentName,
                OwnerID = request.OwnerID.ToString()
            });
        }

        public object GetFileAnalysis(int fileId)
        {
            var records = DB.Where(x => x.FileID == fileId.ToString());
            var noOfDownloads = records.Count();
            var downloadedBy = new List<string>();
            foreach (var rec in records)
            {
                string dUser = rec.DownloadedBy;
                // get user name
                var user = UserProvider.GetUser(dUser);
                if(user != null)
                {
                    downloadedBy.Add(user.FirstName + ", " + user.LastName);
                }
                else
                {
                    downloadedBy.Add("n/a");
                }
            }
            return records;
        }
    }
}
