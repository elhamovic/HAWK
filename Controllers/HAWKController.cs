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
    /// <summary>
    /// this class manages the communication between the front and back end.
    /// </summary>
    public class HAWKController : Controller
    {
        /// <summary>
        /// creating the srvice proxy class object to be able to call the database methods 
        /// </summary>
        HAWKDB hdb = new HAWKDB();
        HRDB hrdh = new HRDB();

        /// <summary>
        /// The Index() controller displays the login page (the first page in the website)
        /// </summary>
        /// <returns> the Index.cshtml view </returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// The Manager(int dno) controller shows the manager homepage, where the manager can track his/her own attendance statistics.
        /// there are 2 methods used in this controller:
        /// GetManager: 
        /// GetAttendance: 
        /// </summary>
        /// <param name="dno"></param>
        /// <returns>the Manager.cshtml view</returns>
        [Route("HAWK/Manager/{dno?}")]
        public IActionResult Manager(int dno)
        {
            UserModel manager = hdb.GetManager(dno);
            ViewBag.Attendnce = hdb.GetAttendnce(manager.Id);
            return View(manager);
        }
        /// <summary>
        /// The Department(int dno) controller shows the manager department page, where the manager can track the attendance of the department employees.  
        /// </summary>
        /// <param name="dno"></param>
        /// <returns>the Department.cshtml view</returns>
        [Route("HAWK/Department/{dno?}")]
        public IActionResult Department(int dno)
        {
            return View(hdb.GetDepartmentEmps(dno));
        }
        /// <summary>
        /// this controller opens the tempuser main page
        /// </summary>
        /// <param name="dno"></param>
        /// <returns></returns>
        [Route("HAWK/TempUser/{dno?}")]
        public IActionResult TempUser(int dno)
        {
            return View(hdb.GetAllTemp(dno));
        }
        /// <summary>
        /// this class opens the add temp page
        /// </summary>
        /// <param name="dno"></param>
        /// <returns></returns>
        [Route("HAWK/AddTemp/{dno?}")]
        public IActionResult AddTemp(int dno)
        {
            ViewBag.tempDno = dno;
            return View();
        }
        /// <summary>
        /// this class calls the database class and adds the temp to the database
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
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
        /// <summary>
        /// this class opens the edit temp page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("HAWK/EditTemp/{id?}")]  
        public IActionResult EditTemp(int id)
        {
            return View(hdb.GetTemp(id));
        }
        /// <summary>
        ///  this class calls the database class and deletes the temp to the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("HAWK/DeleteTempToDB/{id?}")]
        public IActionResult DeleteTempToDB(int id)
        {
            int depnum = hdb.GetTemp(id).Dno;
            hdb.DeleteTemp(id);
            return View("TempUser", hdb.GetAllTemp(depnum));
        }
        /// <summary>
        ///  this class calls the database class and edites the temp to the database
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public IActionResult EditTempToDB(TempModel temp)
        {
            hdb.UpdateTemp(temp);
            return View("TempUser", hdb.GetAllTemp(temp.Dno));
        }
        /// <summary>
        /// this methods validates the user when he/she signs in and send them to the correct side
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
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
                    return Redirect("http://localhost:63342/HAWK_SmartFace_Solution/Front/HomePage.html");
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
        /// <summary>
        /// this controller opens signup page 
        /// </summary>
        /// <returns></returns>
        public IActionResult SignUp()
        {
            return View();
        }
        /// <summary>
        /// this methods validates the user information when he/she signs up in the website and send them to the correct side
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IActionResult RegistrationCheck(UserModel user)
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
                    return Redirect("http://localhost:63342/HAWK_SmartFace_Solution/Front/HomePage.html");
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
