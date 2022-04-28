using HAWK_v.Models;
using HAWK_v.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
            return View(hdb.GetAllTemp(dno));
        }
        [Route("HAWK/AddTemp/{dno?}")]
        public IActionResult AddTemp(int dno)
        {
            ViewBag.tempDno = dno;
            return View();
        }
        [HttpPost("AddTempToDB")]
        public IActionResult AddTempToDB(TempModel temp)
        {
            if (temp.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    temp.Image.CopyToAsync(memoryStream);
                    temp.ImageData = Convert.ToBase64String(memoryStream.ToArray());

                }
            }
            hdb.AddTemp(temp);
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
            int depnum = hdb.GetTemp(id).Dno;
            hdb.DeleteTemp(id);
            return View("TempUser", hdb.GetAllTemp(depnum));
        }
        public IActionResult EditTempToDB(TempModel temp)
        {
            hdb.UpdateTemp(temp);
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
                    return Redirect("http://localhost:63342/SmartfaceSolution/Front/HomePage.html");
                }
                else {
                    ViewBag.Attendnce = hdb.GetAttendnce(userModel.Id);
                    return View("EmployeeMain", userModel);
                }
            }
            else
            {
                return View("Fail", userModel);
            }
        }
        public IActionResult SignUp()
        {
            return View();

        }
        public IActionResult EmployeeMain(UserModel user)
        {

            if (user.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    user.Image.CopyToAsync(memoryStream);
                    user.ImageData = Convert.ToBase64String(memoryStream.ToArray());
                   
                }
            }
            if (hrdh.Exist(user) != false)
            {
                if (hdb.isManager(user))
                {
                    ViewBag.Attendnce = hdb.GetAttendnce(user.Id);
                    ViewBag.MangDno = user.Dno;
                    return View("Manager", user);
                }
                else if (hdb.isAdmin(user))
                {
                    return Redirect("http://localhost:63342/SmartfaceSolution/Front/HomePage.html");
                }
                else
                {
                    ViewBag.Attendnce = hdb.GetAttendnce(user.Id);
                    return View("EmployeeMain", user);
                }
            }
            else
            {
                return View("Fail", user);
            }
        }
       
    }
}
