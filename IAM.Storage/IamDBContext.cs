//using Microsoft.EntityFrameworkCore;


using IAM.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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

        public List<FileMetaData> GetFilesMetaData(int userId)
        {
            using var connection = CreateConnection();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = $"[{DBSCHEMA}].SP_GetFileMetaData";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserId", userId);
            connection.Open();
            var reader = command.ExecuteReader();
            List<FileMetaData> lst = new List<FileMetaData>();
            while (reader.Read())
            {
                lst.Add(new FileMetaData
                {
                    FileName = reader["FileName"].ToString(),
                    Ext = reader["Ext"].ToString(),
                    CreatedBy = reader["CreatedBy"].ToString(),
                    FileSize = Convert.ToInt64(reader["FileSize"]),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                });
            }
            return lst;
        }

    }
}
