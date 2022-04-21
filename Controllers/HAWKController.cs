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
        [Route("HAWK/Manager/{dno?}")]
        public IActionResult Manager(int dno)
        {
            ViewBag.Attendnce = hdb.GetAttendnce(hdb.GetManager(dno).Id);
            return View(hdb.GetManager(dno));
        }
        [Route("HAWK/Department/{dno?}")]
        public IActionResult Department(int dno)
        {
            return View(hdb.GetDepartmentEmps(dno));
        }
        [Route("HAWK/TempUser/{dno?}")]
        public IActionResult TempUser(int dno)
        {
            //ViewBag.MangDno = dno;
            return View(hdb.GetAllTemp(dno));
        }
        public IActionResult AddTemp()
        {
            return View();
        }
        public IActionResult AddTempToDB(TempModel temp)
        {
            hdb.AddTemp(temp);
            // add smartface
            return View("TempUser", hdb.GetAllTemp(temp.Dno));
        }
        [Route("HAWK/EditTemp/{id?}")]  
        public IActionResult EditTemp(int id)
        {
            return View(hdb.GetTemp(id));
        }
        [Route("HAWK/DeleteTempToDB/{id?}")]

        public IActionResult DeleteTempToDB(int id)
        {
            hdb.DeleteTemp(id);
            // add smartface
            return View("TempUser", hdb.GetAllTemp(hdb.GetTemp(id).Dno));
        }
        public IActionResult EditTempToDB(TempModel temp)
        {
            hdb.UpdateTemp(temp);
            // add smartface
            return View("TempUser", hdb.GetAllTemp(temp.Dno));
        }
        public IActionResult DBLogin(UserModel userModel)
        {
            if (hdb.isValid(userModel))
            {
                if (hdb.isManager(userModel))
                {
                    ViewBag.Attendnce=  hdb.GetAttendnce(userModel.Id);
                    ViewBag.MangDno= userModel.Dno;

                    return View("Manager", userModel);
                }
                else if (hdb.isAdmin(userModel))
                {
                    return Redirect("http://localhost:63342/SmartfaceSolution/SmartfaceSolution/Front/HomePage.html");
                   // return View("EmployeeMain", userModel);
                }
                else {
                    ViewBag.Attendnce = hdb.GetAttendnce(userModel.Id);

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
                    ViewBag.Attendnce = hdb.GetAttendnce(user.Id);
                    return View("Manager", user);
                }
                else if (hdb.isAdmin(user))
                {
                    return View("EmployeeMain", user);
                }
                else
                {
                    ViewBag.Attendnce = hdb.GetAttendnce(user.Id);
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
