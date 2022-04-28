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
        string connectionString = @"Data Source=DESKTOP-6L8H12A\SFEXPRESS;Initial Catalog=HR;User ID=smartface;Password=smartface;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        string connectionString2 = @"Data Source=DESKTOP-6L8H12A\SFEXPRESS;Initial Catalog=HAWK;User ID=smartface;Password=smartface;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        string token;
        public  bool SearchEmployee(UserModel user)
        {
            string phonenumber = "";
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
                        user.Dno = (int)(reader["Dno"]);
                        user.Name = (string)(reader["Name"]);
                        user.Email = (string)(reader["Email"]);
                        phonenumber = (string)reader["PhoneNumber"];
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
                command.Parameters.Add("@id", System.Data.SqlDbType.Int, 4).Value = user.Id;
                command.Parameters.Add("@name", System.Data.SqlDbType.VarChar, -1).Value = user.Name;
                command.Parameters.Add("@phone", System.Data.SqlDbType.VarChar, -1).Value = phonenumber;
                command.Parameters.Add("@role", System.Data.SqlDbType.VarChar, -1).Value = user.Role;
                command.Parameters.Add("@dno", System.Data.SqlDbType.Int, 4).Value = user.Dno;
                command.Parameters.Add("@email", System.Data.SqlDbType.VarChar, -1).Value = user.Email;
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
                command.Parameters.Add("@id", System.Data.SqlDbType.Int, 4).Value = user.Id;
                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, -1).Value = user.UserName;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, -1).Value = user.Password;
                
                    connection.Open();
                    command.ExecuteNonQuery();
                    x = true;
                    setToken(user);
                    SmartfaceRequest request = new SmartfaceRequest(token);
                    Watchlist watchlist = JsonSerializer.Deserialize<Watchlist>(request.requestNoBody("Watchlist/getWatchlistByName?name=" + user.Dno, "GET"));
                    string json = "{\"watchlistMember\": {\"displayName\":\"" + user.Name + "\",\"fullName\": \"" + user.Name + "\",\"note\":\"" + user.Email + "," + phonenumber + "," + user.Id + "\"},\"watchlistId\":\"" + watchlist.id + "\",\"img\":\"" + user.ImageData + "\"}";
                    request.requestWithBody("WatchlistMember/CreateAndResgister", "POST",json);
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

