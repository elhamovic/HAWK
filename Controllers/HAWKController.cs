﻿using HAWK_v.Models;
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
        public IActionResult DBLogin(UserModel userModel)
        {
            if (hdb.isValid(userModel))
            {
                if (hdb.isManager(userModel))
                {
                    return View("LoginSuccess", userModel);
                }
                else if (hdb.isAdmin(userModel))
                {
                    return View("LoginSuccess", userModel);
                }
                else { 
                
                    return View("LoginSuccess", userModel);
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
                if (hdb.isManager(user))
                {
                    return View("LoginSuccess", user);
                }
                else if (hdb.isAdmin(user))
                {
                    return View("LoginSuccess", user);
                }
                else
                {
                    return View("LoginSuccess", user);
                }
            }
            else
            {
                return View("LoginFail", user);
            }
        }
       
    }
}
