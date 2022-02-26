using IAM.API.Services;
using IAM.Data.Models;
using IAM.Data.RequestModels;
using Microsoft.Extensions.Configuration;
using System;

namespace IAM.API.Handlers
{
    public class FileHandler : BaseHandler
    {
        readonly IConfiguration Configuration;
        readonly IFileService Service;
        public FileHandler(IFileService service ,IConfiguration configuration)
        {
            Service = service;
            Configuration = configuration;
        }

        public object SaveFile(RequestSaveFile saveFile)
        {
            
            var exp = ExecuteTryCatch(() =>
            {
                File file = new File()
                {
                    Data = Convert.FromBase64String(saveFile.File),
                    Ext = saveFile.Ext,
                    Name = saveFile.FileName
                };
                Service.SaveFile(file);
            });
            if (exp == null)
            {
                return new Exception("File Not Saved!");
            }
            return exp;
        }

    }
}
