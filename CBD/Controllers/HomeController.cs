using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBD.BAL.Managers;
using CBD.Model;

namespace CBD.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SideBar()
        {
            var _manager = new PageManager();
            var sidebar = new Sidebar();
            sidebar.Nodes = (List<Node>)_manager.GetNodes().Data;
            return PartialView("Sidebar",sidebar);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}