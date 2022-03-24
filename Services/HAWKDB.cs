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
            userDAO.DeleteTemp(id);
            return true;
        }
        public bool UpdateTemp(int id, string PSdate, string PEdate)
        {
            userDAO.UpdateTemp(id, PSdate, PEdate);
            return true;
        }
        public bool AddTemp(TempModel temp)
        {
            Random rnd = new Random();
            int num = rnd.Next();
            temp.Id = num;
            userDAO.AddTemp(temp.Id, temp.PStartDate, temp.PEndDate, temp.Name, temp.Email);
            return true;
        }
        public TempModel[] GetAllTemp() // list for manager
        {
            
            return userDAO.SelectAllTemp();
        }
        public bool GetTemp(int id, string PSdate, string PEdate) //for update method
        {
            //
            return true;
        }
    }
}
