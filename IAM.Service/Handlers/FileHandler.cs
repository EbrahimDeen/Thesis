using IAM.API.Services;
using IAM.Authenticator;
using IAM.Data;
using IAM.Data.Models;
using IAM.Data.RequestModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace IAM.API.Handlers
{
    public class FileHandler : BaseHandler
    {
        readonly IConfiguration Configuration;
        readonly IFileService Service;
        readonly IAuthenticator Authenticator;
        public FileHandler(IAuthenticator authenticator,IFileService service ,IConfiguration configuration)
        {
            Service = service;
            Configuration = configuration;
            Authenticator = authenticator;
        }

        internal object SaveFile(RequestSaveFile saveFile)
        {
            
            var exp = ExecuteTryCatch(() =>
            {
                var user = Authenticator.AuthToken(saveFile.Token);
                if (user == null) throw new UnauthorizedAccessException(Constants.UnAuthorizedLogMessage);
                File file = new File()
                {
                    Data = Convert.FromBase64String(saveFile.File),
                    Ext = saveFile.Ext,
                    Name = saveFile.FileName
                };
                Service.SaveFile(file, user.ID);
            });
            if (exp == null)
            {
                return new Exception("File Not Saved!");
            }
            return exp;
        }

        internal object GetAllFilesMeta(string token)
        {
            List<FileMetaData> metaData = new List<FileMetaData>();
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
    }
}
