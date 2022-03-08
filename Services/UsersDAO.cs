﻿using HAWK_v.Models;
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
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                string connectionString2 = @"Data Source=DESKTOP-6L8H12A\SFEXPRESS;Initial Catalog=HAWK;User ID=smartface;Password=smartface;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                string sqlStatment2 = "SELECT Role FROM [dbo].[Users] WHERE Id = @id";
                using (SqlConnection connection2 = new SqlConnection(connectionString2))
                {
                    try
                    {
                        SqlCommand command2 = new SqlCommand(sqlStatment2, connection2);
                        command2.Parameters.Add("@id", System.Data.SqlDbType.Int, 4).Value = user.Id;

                        connection2.Open();
                        SqlDataReader reader = command2.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            user.Role = (string)(reader["Role"]);
                            success = true;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            return success;
        }

        public bool DeleteTemp(int id)
        {
            string sqlStatment = "DELETE FROM [dbo].[TempUser] WHERE condition Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatment, connection);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int, 4).Value = id;
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return true;
            }
        }
        public bool UpdateTemp(int id, string PSdate, string PEdate)
        {
            string sqlStatment = "UPDATE [dbo].[TempUser] SET PermissionStartDate = @PSdate, PermissionEndDate = @PEdate WHERE Id = @id"; 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatment, connection);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int, 4).Value = id;
                command.Parameters.Add("@PSdate", System.Data.SqlDbType.VarChar, -1).Value = PSdate;
                command.Parameters.Add("@PEdate", System.Data.SqlDbType.VarChar, -1).Value = PEdate;
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return true;
            }
        }
        public bool AddTemp(int id, string PSdate, string PEdate)
        {
            string sqlStatment = "INSERT INTO [dbo].[TempUser] (Id, PermissionStartDate, PermissionEndDate) VALUES(@id, @PSdate, @PEdate)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatment, connection);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int, 4).Value = id;
                command.Parameters.Add("@PSdate", System.Data.SqlDbType.VarChar, -1).Value = PSdate;
                command.Parameters.Add("@PEdate", System.Data.SqlDbType.VarChar, -1).Value = PEdate;
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return true;
            }
        }
    }
}
