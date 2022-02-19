using HAWK_v.Models;
using HAWK_v.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAWK_v.Controllers
{
    public class HAWKController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult DBLogin(UserModel userModel)
        {
            HAWKDB SS = new HAWKDB();
            if (SS.isValid(userModel))
            {
                return View("LoginSuccess", userModel);
            }
            else
            {
                return View("LoginFail", userModel);
            }
        }
        public IActionResult SignUp()
        {
            return View();

        }
        public IActionResult EmployeeMain(UserModel user)
        {
            HRDB em = new HRDB();
            if (em.Exist(user) != false)
            {
                return View("LoginSuccess", user);
            }
            else
            {
                return View("LoginFail", user);
            }
        }
    }
}
