using HAWK_v.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAWK_v.Services
{
    public class HAWKDB
    {
        UsersDAO userDAO = new UsersDAO();
        public HAWKDB()
        {
        }
        public bool isValid(UserModel user)
        {
            return userDAO.searchDB(user);
        }
        public bool isManager(UserModel user)
        {
            if (user.Role.ToLower().Contains("manager"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool isAdmin(UserModel user)
        {
            if (user.Role.ToLower() == "admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteTemp(int id)
        {
            return userDAO.DeleteTemp(id);
        }
        public bool UpdateTemp(TempModel temp)
        {
            return userDAO.UpdateTemp(temp.Id, temp.PStartDate, temp.PEndDate, temp.Name, temp.Email, temp.Dno);
        }
        public bool AddTemp(TempModel temp)
        {
            Random rnd = new Random();
            int num = rnd.Next();
            temp.Id = num;
            userDAO.AddTemp(temp.Id, temp.PStartDate, temp.PEndDate, temp.Name, temp.Email, temp.Dno);
            return true;
        }
        public List<TempModel> GetAllTemp(int Dno) // list for manager
        {
            return userDAO.SelectAllTemp(Dno);
        }
        public TempModel GetTemp(int id) //for update
        {
            return userDAO.SelectTemp(id);
        }
        public List<UserModel> GetDepartmentEmps(int Dno)
        {
            return userDAO.GetDepartmentEmps(Dno);
        }
        public List<string> GetAttendnce(int id)
        {
            return userDAO.GetAttendance(id);
        }
        public UserModel GetManager(int Dno)
        {
            return userDAO.SelectManager(Dno);
        }
    }
}
