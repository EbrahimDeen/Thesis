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
        public int AddFile(File file)
        {
            return Context.AddFile(file);
        }
    }
}
