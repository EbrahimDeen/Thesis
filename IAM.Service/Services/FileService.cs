﻿using IAM.Data.Models;
using IAM.Storage.Providers;
using System.Collections.Generic;

namespace IAM.API.Services
{
    public class FileService : IFileService
    {
        readonly IFileProvider Provider;
        public FileService(IFileProvider provider)
        {
            Provider = provider;
        }
        public File GetFile(string fileName, string fileExt)
        {
            throw new System.NotImplementedException();
        }

        public File GetFileById(int ID)
        {
            throw new System.NotImplementedException();
        }

        public List<FileMetaData> GetFilesMetaData(int userId)
        {
            return Provider.GetFilesMetaData(userId);
        }

        public int SaveFile(File file, int userId)
        {
            return Provider.AddFile(file, userId);
            
        }
    }
}