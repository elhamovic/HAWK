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
    }
}
