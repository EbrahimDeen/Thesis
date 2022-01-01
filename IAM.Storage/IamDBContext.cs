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
                    User_Email = reader["User_Email"].ToString(),
                    User_fristName = reader["User_fristName"].ToString(),
                    User_ID = Convert.ToInt32(reader["User_ID"]),
                    User_lastName = reader["User_lastName"].ToString()
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
            command.Parameters.AddWithValue("@User_fristName ", user.User_fristName);
            command.Parameters.AddWithValue("@User_lastName ", user.User_lastName);
            command.Parameters.AddWithValue("@User_Email ", user.User_Email);
            command.Parameters.AddWithValue("@DOB", user.DOB);
            command.Parameters.AddWithValue("@Country ",user.Country);
            command.Parameters.AddWithValue("@Password ",user.Password);
            command.Parameters.AddWithValue("@Status ", user.Status);
            command.ExecuteNonQuery();

        }
    }
}
