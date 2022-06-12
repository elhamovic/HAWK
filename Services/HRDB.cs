using HAWK_v.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAWK_v.Services
{
    /// <summary>
    /// This class is a proxy between the database class and the controller
    /// </summary>
    public class HRDB
    {
        SignUpDB signup = new SignUpDB();
        public HRDB()
        {
        }
        public bool Exist(UserModel user)
        {
            return signup.SearchEmployee(user);
        }
    }
}
