﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CBD.Administration.Controllers
{
    [Authorize]
    public class GroupUserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}