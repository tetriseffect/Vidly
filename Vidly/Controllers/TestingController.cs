﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using Vidly.Controllers;

namespace Vidly.Controllers
{
    public class TestingController : Controller
    {
        // GET: Test123
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Details1()
        {
            return View();
        }
    }
}