﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RequestFlow.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Authenticate()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Calc(int val = 0)
        {
            decimal result = 100 / val;
            return View(result);
        }
    }
}