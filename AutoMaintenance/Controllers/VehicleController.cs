﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoMaintenance.Controllers
{
    public class VehicleController : Controller
    {
        //GET: Vehicle
        public ActionResult Index()
        {
            return View();
        }

        //public string Index()
        //{
        //    return "This is my <b>default</b> action...";
        //}

        public ActionResult Welcome(string name, int numTimes = 1)
        {
            ViewBag.Message = "Hello " + name;
            ViewBag.NumTimes = numTimes;

            return View();
        }
    }
}