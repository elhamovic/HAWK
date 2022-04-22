using HAWK_v.helpers;
using HAWK_v.Helpers;
using HAWK_v.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HAWK_v.Services
{
    public class SignUpDB
    {
        string connectionString = @"Data Source=DESKTOP-6L8H12A\SFEXPRESS;Initial Catalog=HAWKSYS;User ID=smartface;Password=smartface;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        string connectionString2 = @"Data Source=DESKTOP-6L8H12A\SFEXPRESS;Initial Catalog=HAWK;User ID=smartface;Password=smartface;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        string token;
        public  bool SearchEmployee(UserModel user)
        {
            List<string> userInfo = new List<string>();
            bool x = false;
            string sqlStatment = "SELECT * FROM [dbo].[Employees] WHERE UserName = @username AND Password = @password";
            string sqlStatment2 = "INSERT INTO [dbo].[Users] ([Id], [Name] ,[PhoneNumber] ,[Role] ,[Dno],[Email]) VALUES (@id ,@name ,@phone ,@role ,@dno,@email)";
            string sqlStatment3 =  "INSERT INTO [dbo].[Employees] ([Id],[UserName],[Password]) VALUES (@id, @username,@password)";
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
                        user.Role = (string)(reader["Role"]);
                        userInfo.Add(String.Format("{0}", reader["Id"]));
                        userInfo.Add(String.Format("{0}", reader["Name"]));
                        userInfo.Add(String.Format("{0}", reader["PhoneNumber"]));
                        userInfo.Add(String.Format("{0}", reader["Role"]));
                        userInfo.Add(String.Format("{0}", reader["Dno"]));
                        userInfo.Add(String.Format("{0}", reader["Email"]));
                        userInfo.Add(String.Format("{0}", reader["UserName"]));
                        userInfo.Add(String.Format("{0}", reader["Password"]));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            //----------------------------------------------------------------------------------------------------------------
            using (SqlConnection connection = new SqlConnection(connectionString2))
            {
                try
                {

                    SqlCommand command = new SqlCommand(sqlStatment2, connection);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int, 4).Value = userInfo[0];
                command.Parameters.Add("@name", System.Data.SqlDbType.VarChar, -1).Value = userInfo[1];
                command.Parameters.Add("@phone", System.Data.SqlDbType.VarChar, -1).Value = userInfo[2];
                command.Parameters.Add("@role", System.Data.SqlDbType.VarChar, -1).Value = userInfo[3];
                command.Parameters.Add("@dno", System.Data.SqlDbType.Int, 4).Value = userInfo[4];
                command.Parameters.Add("@email", System.Data.SqlDbType.VarChar, -1).Value = userInfo[5];
                    connection.Open();
                    command.ExecuteNonQuery();
                    
                    
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            //-----------------------------------------------------------------------------------
            using (SqlConnection connection = new SqlConnection(connectionString2))
            {
                try
                {
                    SqlCommand command = new SqlCommand(sqlStatment3, connection);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int, 4).Value = userInfo[0];
                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, -1).Value = userInfo[6];
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, -1).Value = userInfo[7];
                
                    connection.Open();
                    command.ExecuteNonQuery();
                    x = true;
                    setToken(user);
                   new SmartfaceRequest(token).requestNoBody("WatchlistMember/CreateAndResgister?displayName=" + userInfo[1] + "&fullName=" + userInfo[1] + "&note=" + userInfo[5] + "," + userInfo[2] + "," + userInfo[0] +
                        "&watchlistId=b1af7331-7cbf-4335-b682-d32bb18541e7&imgUrl=C://SmartFaceImages//Mom.png", "POST");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return x;


        }
        private async void setToken(UserModel user)
        {
            try
            {
                string json = "{\"username\":\"" + user.UserName + "\",\"password\":\"" + user.Password + "\"}";
                AuthenticateResponse response = JsonSerializer.Deserialize<AuthenticateResponse>(new SmartfaceRequest().requestWithBody("authenticate", "POST", json));
                
                token = response.token;

            }
            catch (Exception e)
            {

            }
        }
    }
}

