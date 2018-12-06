using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CBD.BAL.Managers;
using CBD.Model;
using HtmlAgilityPack;

namespace CBD.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            #region Techcombank
            HtmlWeb htmlWeb = new HtmlWeb()
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8,  //Set UTF8 để hiển thị tiếng Việt
                //PreRequest = request =>
                //{
                //    // Make any changes to the request object that will be used.
                //    request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                //    return true;
                //}
            };
            //HtmlDocument doctech = htmlWeb.Load("https://www.techcombank.com.vn/cong-cu-tien-ich/ti-gia/ti-gia-chot");

            //var url = "http://data.tradingcharts.com/futures/quotes/kc.html";
            //var weburrl = new MyWebClient().DownloadString(url);

            HtmlDocument htmlDocument = htmlWeb.Load("https://giacaphe.com/gia-ca-phe-truc-tuyen/");
            //var data = htmlDocument.DocumentNode.SelectSingleNode("//table[@class='fe_quotes'");

            HtmlNodeCollection data = htmlDocument.DocumentNode.SelectNodes("//table");
            var rows = data.Descendants("tr").ToList();
            List<List<string>> rowValues = new List<List<string>>();
            foreach (var row in rows)
            {
                List<string> currentRowValues = new List<string>();
                foreach (var column in row.ChildNodes)
                {
                    currentRowValues.Add(column.InnerText);
                }
                rowValues.Add(currentRowValues);
            }
            var result = rowValues;

            //var UserTable = htmlDocument.DocumentNode.SelectSingleNode("//div[@id='update_quote']").SelectSingleNode("//table[@id='coffee_liffe']").SelectSingleNode("//tbody").SelectNodes("//tr");
            //foreach (var row in UserTable)
            //{
            //    if (row.Attributes["data-source"] != null)
            //    {
            //        string Source = row.Attributes["data-source"].Value;
            //        //string UserName = row.SelectSingleNode("td[@class='tdleft']").InnerText;
            //        //string Points = row.SelectSingleNode("td[@class='tdLast']").InnerText;
            //        //Console.WriteLine(Source + "\t" + UserName + "\t" + Points);
            //    }
            //}
            #endregion

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