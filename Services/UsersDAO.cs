using HAWK_v.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HAWK_v.Services
{
    public class UsersDAO
    {
        string connectionString = @"Data Source=DESKTOP-6L8H12A\SFEXPRESS;Initial Catalog = HAWK; User ID = smartface; Password=smartface; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public bool searchDB(UserModel user)
        {
            bool success = false;
            string sqlStatment = "SELECT * FROM [dbo].[Employees] WHERE UserName = @username AND Password = @password";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatment, connection);
                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, -1).Value = user.UserName;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, -1).Value = user.Password;
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        user.Id = (int)(reader["Id"]);
                        success = true; 
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return success;
        }
    }
}
