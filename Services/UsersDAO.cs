using HAWK_v.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using HAWK_v.Helpers;

namespace HAWK_v.Services
{
    public class UsersDAO
    {
        string connectionString = @"Data Source=DESKTOP-6L8H12A\SFEXPRESS;Initial Catalog = HAWK; User ID = smartface; Password=smartface; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        List<TempModel> TempUsers;
        List<UserModel> Users;
        private string token = "";
        private UserModel theUser = new UserModel();
        private TempModel theTemp = new TempModel();
        public bool searchDB(UserModel user)
        {
            bool success = false;
            string sqlStatment = "SELECT * FROM [dbo].[Employees] WHERE UserName = @username AND Password = @password";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatment, connection);
                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, -1).Value = theUser.UserName = user.UserName;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, -1).Value = theUser.Password = user.Password;
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        user.Id = theUser.Id = (int)(reader["Id"]);
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
                            user.Role = theUser.Role = (string)(reader["Role"]);
                            user.Dno = theUser.Dno = (int)(reader["Dno"]);
                            user.Name = theUser.Name = (string)(reader["Name"]);
                            user.Email = theUser.Email = (string)(reader["Email"]);
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
        public bool UpdateTemp(int id, string PSdate, string PEdate, string Name, string Email, int Dno)
        {
            string sqlStatment = "UPDATE [dbo].[TempUser] SET PermissionStartDate = @PSdate, PermissionEndDate = @PEdate, Name = @name, Email = @email, Dno = @dno WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatment, connection);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int, 4).Value = id;
                command.Parameters.Add("@PSdate", System.Data.SqlDbType.VarChar, -1).Value = PSdate;
                command.Parameters.Add("@PEdate", System.Data.SqlDbType.VarChar, -1).Value = PEdate;
                command.Parameters.Add("@name", System.Data.SqlDbType.VarChar, -1).Value = Name;
                command.Parameters.Add("@email", System.Data.SqlDbType.VarChar, -1).Value = Email;
                command.Parameters.Add("@dno", System.Data.SqlDbType.Int, 4).Value = Dno;
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
        public bool AddTemp(int id, string PSdate, string PEdate, string Name, string Email, int Dno)
        {
            string sqlStatment = "INSERT INTO [dbo].[TempUser] (Id, PermissionStartDate, PermissionEndDate, Name, Email, Dno) VALUES(@id, @PSdate, @PEdate, @Name, @Email, @dno)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatment, connection);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int, 4).Value = id;
                command.Parameters.Add("@PSdate", System.Data.SqlDbType.VarChar, -1).Value = PSdate;
                command.Parameters.Add("@PEdate", System.Data.SqlDbType.VarChar, -1).Value = PEdate;
                command.Parameters.Add("@Name", System.Data.SqlDbType.VarChar, -1).Value = Name;
                command.Parameters.Add("@Email", System.Data.SqlDbType.VarChar, -1).Value = Email;
                command.Parameters.Add("@dno", System.Data.SqlDbType.Int, 4).Value = Dno;
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
        public List<TempModel> SelectAllTemp(int Dno)
        {
            string sqlStatment = "SELECT * FROM [dbo].[TempUser] Where Dno = @dno ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatment, connection);
                command.Parameters.Add("@dno", System.Data.SqlDbType.Int, 4).Value = Dno;
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    TempUsers = new List<TempModel>();
                    if (reader.HasRows)
                    {                     
                        while (reader.Read() != false)
                        {
                            TempModel temp = new TempModel();
                            temp.Id = (int)(reader["Id"]);
                            temp.PStartDate = (string)(reader["PermissionStartDate"]);
                            temp.PEndDate = (string)(reader["PermissionEndDate"]);
                            temp.Name = (string)(reader["Name"]);
                            temp.Email = (string)(reader["Email"]);
                            temp.Dno = (int)(reader["Dno"]);
                            TempUsers.Add(temp);
                        }

                    }
                    else
                    {
                        TempModel temp = new TempModel();
                        temp.Dno = Dno;
                        TempUsers.Add(temp);
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
                            temp.Id = theTemp.Id = (int)(reader["Id"]);
                            temp.PStartDate = theTemp.PStartDate = (string)(reader["PermissionStartDate"]);
                            temp.PEndDate = theTemp.PEndDate = (string)(reader["PermissionEndDate"]);
                            temp.Name = theTemp.Name = (string)(reader["Name"]);
                            temp.Email = theTemp.Email = (string)(reader["Email"]);
                            temp.Dno = (int)(reader["Dno"]);
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
                            user.Email = (string)(reader["Email"]);
                            user.Dno = (int)(reader["Dno"]);
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
        public UserModel SelectManager(int Dno)
        {
            UserModel user = new UserModel();
            string sqlStatment = "SELECT * FROM [dbo].[Users] WHERE Dno = @dno AND Role LIKE '%Manager%'";
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
                        reader.Read();
                        user.Id = (int)(reader["Id"]);
                        user.Name = (string)(reader["Name"]);
                        user.Role = (string)(reader["Role"]);
                        user.Email = (string)(reader["Email"]);
                        user.Dno = (int)(reader["Dno"]);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return user;
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
      /*  public void auth()
        {
            var user = {userName:}
            new SmartfaceRequest().requestWithBody("authenticate", "POST",);
        }*/
    }
}
