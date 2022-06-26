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
        //static List<AnalysisTempDB.Analysis> DB = new List<AnalysisTempDB.Analysis>();

        public AnalysisProvider(IDbContext context)
        {
            Context = context;
        }

        public void FileDownloaded(AnalysisModel request)
        {
            Context.AddFileDownloadedAnalysis(request.IP, request.CountryName, request.CityName, request.ContinentName, request.DownloadedBy, request.FileID);
        }

        public object GetFileAnalysis(int fileId, int userId)
        {
            var records = Context.GetFileAnalysisAsync(fileId, userId);
            return records;
        }
    }
}
