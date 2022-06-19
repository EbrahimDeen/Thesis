using IAM.API.Services;
using IAM.Authenticator;
using IAM.Data;
using IAM.Data.Models;
using IAM.Data.RequestModels;
using IAM.Data.ResponseModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IAM.API.Handlers
{
    public class FileHandler : BaseHandler
    {
        readonly IConfiguration Configuration;
        readonly IFileService Service;
        readonly IAuthenticator Authenticator;
        readonly IAnalysisService AnalysisService;
        public FileHandler(IAuthenticator authenticator,IFileService service ,IConfiguration configuration, IAnalysisService analysisService)
        {
            Service = service;
            Configuration = configuration;
            Authenticator = authenticator;
            AnalysisService = analysisService;
        }

        internal async Task<object> GetFileByIdAsync(string token, int id)
        {
            File file = new File();
            DownloadFileResponseModel resFile = new DownloadFileResponseModel();
            var exp = await ExecuteTryCatchAsync(async () =>
            {
                var user = Authenticator.AuthToken(token);
                if (user != null)
                {
                    file = await Service.GetFileByIdAsync(user.ID, id);
                    if(file != null)
                    {
                        resFile.Data = Convert.ToBase64String(file.Data);
                        resFile.Ext = file.Ext;
                        resFile.Name = file.Name;
                    }
                    else { resFile = null; }
                }
                else
                {
                    throw new UnauthorizedAccessException(Constants.UnAuthorizedLogMessage);
                }
            });
            return exp ?? resFile;
        }


        internal object SaveFile(RequestSaveFile saveFile)
        {
            int fileID = -1;
            var exp = ExecuteTryCatch(() =>
            {
                var user = Authenticator.AuthToken(saveFile.Token);
                if (user == null) throw new UnauthorizedAccessException(Constants.UnAuthorizedLogMessage);
                File file = new File()
                {
                    Data = saveFile.File,
                    Ext = saveFile.Ext,
                    Name = saveFile.FileName
                };
                fileID = Service.SaveFile(file, user.ID);
            });
            return exp ?? fileID;
        }

        internal object GetAllFilesMeta(string token)
        {
            IEnumerable<FileMetaData> metaData = new List<FileMetaData>();
            var exp = ExecuteTryCatch(() =>
            {
                var user = Authenticator.AuthToken(token);
                if (user != null)
                {
                    metaData = Service.GetFilesMetaData(user.ID);
                }
                else
                {
                    throw new UnauthorizedAccessException(Constants.UnAuthorizedLogMessage);
                }
            });

            return exp ?? metaData;
           
        }

        internal void AddFileAnalysis(FileAnalysisRequest request,int fileId, string token)
        {
            var user = Authenticator.AuthToken(token);

            var analysis = new AnalysisModel()
            {
                DownloadedBy = user.ID,
                IP = request.IPAddress,
                FileID = fileId,
                ContinentName = request.Continent,
                CountryName = request.CountryName,
                CityName = request.CityName,
                OwnerID = -1
            };

            AnalysisService.FileDownloaded(analysis);
        }
        internal object GetFileAnalysis(string token, int fileId)
        {
            object analysis = null;
            var exp = ExecuteTryCatch(() =>
            {
                var user = Authenticator.AuthToken(token);
                if (user != null)
                {
                    analysis = AnalysisService.GetFileAnalysis(fileId);
                }
                else
                {
                    throw new UnauthorizedAccessException(Constants.UnAuthorizedLogMessage);
                }
            });
            return exp ?? analysis;
        }

    }
}
