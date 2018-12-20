﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CBD.Administration.Controllers
{
    [Authorize]
    public class UnitController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}