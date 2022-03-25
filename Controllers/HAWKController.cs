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
        HAWKDB hdb = new HAWKDB();
        HRDB hrdh = new HRDB();

        public IActionResult Index()
        {

            return View();
        }
        public IActionResult Manager()
        {

            return View();
        }
        public IActionResult Department()
        {

            return View();
        }
        public IActionResult TempUser()
        {
            
            return View(hdb.GetAllTemp());
        }
        public IActionResult AddTemp()
        {

            return View();
        }
        public IActionResult AddTempToDB(TempModel temp)
        {
            hdb.AddTemp(temp);
            // add smartface
            return View("TempUser", hdb.GetAllTemp());
        }
        [Route("HAWK/EditTemp/{id?}")]  
        public IActionResult EditTemp(int id)
        {
            return View(hdb.GetTemp(id));
        }
        public IActionResult DeleteTempToDB(TempModel temp)
        {
            hdb.DeleteTemp(temp.Id);
            // add smartface
            return View("TempUser", hdb.GetAllTemp());
        }
        public IActionResult EditTempToDB(TempModel temp)
        {
            hdb.UpdateTemp(temp);
            // add smartface
            return View("TempUser", hdb.GetAllTemp());
        }
        public IActionResult DBLogin(UserModel userModel)
        {
            if (hdb.isValid(userModel))
            {
                if (hdb.isManager(userModel))
                {
                    return View("Manager", userModel);
                }
                else if (hdb.isAdmin(userModel))
                {
                    // smartface controller
                    return View("EmployeeMain", userModel);
                }
                else { 
                
                    return View("EmployeeMain", userModel);
                }
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
            if (hrdh.Exist(user) != false)
            {
                //add smartface
                if (hdb.isManager(user))
                {
                    return View("Manager", user);
                }
                else if (hdb.isAdmin(user))
                {
                    return View("EmployeeMain", user);
                }
                else
                {
                    return View("EmployeeMain", user);
                }
            }
            else
            {
                return View("LoginFail", user);
            }
        }
       
    }
}
