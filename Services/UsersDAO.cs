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
        List<TempModel> TempUsers;
        List<UserModel> Users;
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
                string sqlStatment2 = "SELECT * FROM [dbo].[Users] WHERE Id = @id";
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
                            user.Dno = (int)(reader["Dno"]);
                            user.Name = (string)(reader["Name"]);
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
            string sqlStatment = "DELETE FROM [dbo].[TempUser] WHERE Id = @id";
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
        public bool UpdateTemp(int id, string PSdate, string PEdate, string Name, string Email)
        {
            string sqlStatment = "UPDATE [dbo].[TempUser] SET PermissionStartDate = @PSdate, PermissionEndDate = @PEdate, Name = @name, Email = @email WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatment, connection);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int, 4).Value = id;
                command.Parameters.Add("@PSdate", System.Data.SqlDbType.VarChar, -1).Value = PSdate;
                command.Parameters.Add("@PEdate", System.Data.SqlDbType.VarChar, -1).Value = PEdate;
                command.Parameters.Add("@name", System.Data.SqlDbType.VarChar, -1).Value = Name;
                command.Parameters.Add("@email", System.Data.SqlDbType.VarChar, -1).Value = Email;
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
        public bool AddTemp(int id, string PSdate, string PEdate, string Name, string Email)
        {
            string sqlStatment = "INSERT INTO [dbo].[TempUser] (Id, PermissionStartDate, PermissionEndDate, Name, Email) VALUES(@id, @PSdate, @PEdate, @Name, @Email)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatment, connection);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int, 4).Value = id;
                command.Parameters.Add("@PSdate", System.Data.SqlDbType.VarChar, -1).Value = PSdate;
                command.Parameters.Add("@PEdate", System.Data.SqlDbType.VarChar, -1).Value = PEdate;
                command.Parameters.Add("@Name", System.Data.SqlDbType.VarChar, -1).Value = Name;
                command.Parameters.Add("@Email", System.Data.SqlDbType.VarChar, -1).Value = Email;
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
        public List<TempModel> SelectAllTemp()
        {
            string sqlStatment = "SELECT * FROM [dbo].[TempUser] ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatment, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        TempUsers = new List<TempModel>();
                        //reader.Read();
                        while (reader.Read() != false)
                        {
                            TempModel temp = new TempModel();
                            temp.Id = (int)(reader["Id"]);
                            temp.PStartDate = (string)(reader["PermissionStartDate"]);
                            temp.PEndDate = (string)(reader["PermissionEndDate"]);
                            temp.Name = (string)(reader["Name"]);
                            temp.Email = (string)(reader["Email"]);

                            TempUsers.Add(temp);
                        }
                        
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return TempUsers;
            }
        }
        public TempModel SelectTemp(int id)
        {
            TempModel temp = new TempModel();
            string sqlStatment = "SELECT * FROM [dbo].[TempUser] WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatment, connection);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int, 4).Value = id;
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                            reader.Read();
                            temp.Id = (int)(reader["Id"]);
                            temp.PStartDate = (string)(reader["PermissionStartDate"]);
                            temp.PEndDate = (string)(reader["PermissionEndDate"]);
                            temp.Name = (string)(reader["Name"]);
                            temp.Email = (string)(reader["Email"]);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return temp;
            }
        }
        public List<UserModel> GetDepartmentEmps(int Dno)
        {
            string sqlStatment = "SELECT * FROM [dbo].[Users] WHERE Dno = @dno ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatment, connection);
                command.Parameters.Add("@dno", System.Data.SqlDbType.Int, 4).Value = Dno;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        Users = new List<UserModel>();
                        while (reader.Read() != false)
                        {
                            UserModel user = new UserModel();
                            user.Id = (int)(reader["Id"]);
                            user.Name = (string)(reader["Name"]);
                            user.Role = (string)(reader["Role"]);
                            Users.Add(user);
                        }

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return Users;
            }
        }
        public List<string> GetAttendance(int id)
        {
            List<string> AttendanceList = new List<string>(); ;
            string sqlStatment = "SELECT * FROM [dbo].[Attendance] WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatment, connection);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int, 4).Value = id;
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read() != false)
                    {
                        string attendance = (string)reader["EnterTime"] + "," + reader["EnterDate"] + "," + reader["ExitTime"] + "," + reader["ExitDate"];
                        AttendanceList.Add(attendance);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return AttendanceList;
            }
        }
    }
}
