//using Microsoft.EntityFrameworkCore;


using IAM.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace IAM.Storage
{
    public class IamDBContext : IDbContext
    {
        private readonly IConfiguration Configuration;
        private readonly string DBSCHEMA;

        public IamDBContext(IConfiguration configuration)
        {
            Configuration = configuration;
            DBSCHEMA = Configuration["DBSCHEMA"];
        }
        private SqlConnection CreateConnection()
        {
            return new SqlConnection(Configuration.GetConnectionString("Dev"));
        }

        public IEnumerable<User> GetUsers() 
        {
            using var connection = CreateConnection();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM [{DBSCHEMA}].[USERS]";

            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                User user = new()
                {
                    Country = reader["Country"].ToString(),
                    DOB = Convert.ToDateTime(reader["DOB"]),
                    Password = reader["Password"].ToString(),
                    Status = reader["Status"].ToString(),
                    Email = reader["User_Email"].ToString(),
                    FirstName = reader["User_fristName"].ToString(),
                    ID = Convert.ToInt32(reader["User_ID"]),
                    LastName = reader["User_lastName"].ToString()
                };
                yield return user;
            }
        }

        public void AddUser(User user)
        {
            using var connection = CreateConnection();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = $"[{DBSCHEMA}].SP_RegisterUser";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            command.Parameters.AddWithValue("@User_fristName ", user.FirstName);
            command.Parameters.AddWithValue("@User_lastName ", user.LastName);
            command.Parameters.AddWithValue("@User_Email ", user.Email);
            command.Parameters.AddWithValue("@DOB", user.DOB);
            command.Parameters.AddWithValue("@Country ",user.Country);
            command.Parameters.AddWithValue("@Password ",user.Password);
            command.Parameters.AddWithValue("@Status ", user.Status);
            command.ExecuteNonQuery();

        }

        public int AddFile(File file, int userId)
        {
            using var connection = CreateConnection();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = $"[{DBSCHEMA}].SP_AddFile";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Data", file.Data);
            command.Parameters.AddWithValue("@Name", file.Name);
            command.Parameters.AddWithValue("@Ext", file.Ext);
            command.Parameters.AddWithValue("@UserId", userId);
            connection.Open();
            var id = (int)command.ExecuteScalar();
            return id;
        }

        public IEnumerable<FileMetaData> GetFilesMetaData(int userId)
        {
            using var connection = CreateConnection();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = $"[{DBSCHEMA}].SP_GetFileMetaData";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserId", userId);
            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var fmd = new FileMetaData
                {
                    FileId = (int)reader["FileId"],
                    FileName = reader["FileName"].ToString(),
                    Ext = reader["Ext"].ToString(),
                    CreatedBy = reader["CreatedBy"].ToString(),
                    FileSize = Convert.ToInt64(reader["FileSize"]),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                };
                yield return fmd;
            }
        }

        public FileMetaData GetFileMetaDataByID(int userId, int fileId)
        {
            using var connection = CreateConnection();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = $"[{DBSCHEMA}].SP_GetFileMetaDataByID";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@FileId", fileId);
            command.Parameters.AddWithValue("@UserId", userId);
            connection.Open();
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var fmd = new FileMetaData
                {
                    FileId = (int)reader["FileId"],
                    FileName = reader["FileName"].ToString(),
                    Ext = reader["Ext"].ToString(),
                    CreatedBy = reader["CreatedBy"].ToString(),
                    FileSize = Convert.ToInt64(reader["FileSize"]),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                };
                return fmd;
            }
            return null;
        }


        public async Task<File> GetFileByIdAsync(int userId, int fileId)
        {
            using var connection = CreateConnection();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = $"[{DBSCHEMA}].SP_GetFileById";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@FileId", fileId);
            connection.Open();
            var reader = await command.ExecuteReaderAsync();

            if (reader.Read())
            {
                return new File()
                {
                    Data = (byte[])reader["FileData"],
                    Ext = reader["Ext"].ToString(),
                    Name = reader["FileName"].ToString()
                };
            }
            return null;
        }

        public void AddFileDownloadedAnalysis(string IP, string countryname, string cityname, string continentname, int downloadedby, int fileId)
        {
            using var connection = CreateConnection();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = $"[dbo].SP_AddAnalysis";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            command.Parameters.AddWithValue("@IP ", IP);
            command.Parameters.AddWithValue("@CountryName ", countryname);
            command.Parameters.AddWithValue("@CityName ", cityname);
            command.Parameters.AddWithValue("@ContinentName", continentname);
            command.Parameters.AddWithValue("@DownloadedBy ", downloadedby);
            command.Parameters.AddWithValue("@FileId ", fileId);
            command.ExecuteNonQuery();
        }

        public IEnumerable<AnalysisModel> GetFileAnalysisAsync(int fileId, int userId)
        {
            using var connection = CreateConnection();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = $"[dbo].SP_GetFileAnalysis";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@FileId", fileId);
            command.Parameters.AddWithValue("@UserId", userId);
            connection.Open();
            //IEnumerable<AnalysisModel> analysisModels
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                yield return new AnalysisModel()
                {
                    IP = reader["IP"].ToString(),
                    OwnerID = Convert.ToInt32(reader["OwnerID"]),
                    FileID = Convert.ToInt32(reader["FileId"]),
                    CityName = reader["CityName"].ToString(),
                    ContinentName = reader["ContinentName"].ToString(),
                    CountryName = reader["CountryName"].ToString(),
                    DownloadedBy = Convert.ToInt32(reader["DownloadedBy"])
                };
            }
        }

        public IEnumerable<FileMetaData> GetPublicFilesMeta()
        {
            using var connection = CreateConnection();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = $"[{DBSCHEMA}].SP_GetPublicFilesMeta";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var fmd = new FileMetaData
                {
                    FileId = (int)reader["FileId"],
                    FileName = reader["FileName"].ToString(),
                    Ext = reader["Ext"].ToString(),
                    CreatedBy = reader["CreatedBy"].ToString(),
                    FileSize = Convert.ToInt64(reader["FileSize"]),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                };
                yield return fmd;
            }
        }

        public void SetFilePublic(int fileId)
        {
            using var connection = CreateConnection();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = $"[dbo].SP_SetFilePublic";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            command.Parameters.AddWithValue("@FileId ", fileId);
            command.ExecuteNonQuery();
        }
    }
}
