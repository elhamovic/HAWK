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
            if (user.Role.Contains("Manager"))
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
            if (user.Role == "Admin")
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
        public bool AddTemp(int id, string PSdate, string PEdate)
        {
            userDAO.AddTemp(id, PSdate, PEdate);
            return true;
        }
    }
}
