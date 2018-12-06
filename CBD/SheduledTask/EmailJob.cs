using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using Quartz;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Linq;

namespace CBD.SheduledTask
{
    public class EmailJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            //SmtpClient client = new SmtpClient();
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.EnableSsl = true;
            //client.Host = "smtp.office365.com";
            //client.Port = 25;

            //// setup Smtp authentication
            //System.Net.NetworkCredential credentials =
            //    new System.Net.NetworkCredential("tutn5@vpbank.com.vn", "vianh@12345");
            //client.UseDefaultCredentials = false;
            //client.Credentials = credentials;

            //MailMessage msg = new MailMessage();
            //msg.From = new MailAddress("tutn5@vpbank.com.vn");
            //msg.To.Add(new MailAddress("ngoctu.tran83@gmail.com"));

            //msg.Subject = "This is a test Email subject";
            //msg.IsBodyHtml = true;
            //msg.Body = string.Format("<html><head></head><body><b>Test HTML Email</b></body>");

            //try
            //{
            //    client.Send(msg);
            //    //lblMsg.Text = "Your message has been successfully sent.";
            //}
            //catch (Exception ex)
            //{
            //    //lblMsg.ForeColor = Color.Red;
            //    //lblMsg.Text = "Error occured while sending your message." + ex.Message;
            //}

            //#region Techcombank
            //HtmlWeb htmlWeb = new HtmlWeb()
            //{
            //    AutoDetectEncoding = false,
            //    OverrideEncoding = Encoding.UTF8,  //Set UTF8 để hiển thị tiếng Việt
            //    PreRequest = request =>
            //    {
            //        // Make any changes to the request object that will be used.
            //        request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            //        return true;
            //    }
            //};
            //HtmlDocument doctech = htmlWeb.Load("https://www.techcombank.com.vn/cong-cu-tien-ich/ti-gia/ti-gia-chot");

            //var url = "http://data.tradingcharts.com/futures/quotes/kc.html";
            //var weburrl = new MyWebClient().DownloadString(url);

            //HtmlDocument htmlDocument = htmlWeb.Load("http://data.tradingcharts.com/futures/quotes/kc.html");
            //var data = htmlDocument.DocumentNode.SelectSingleNode("//*[@id='fe_content']").SelectSingleNode("//table[@class='fe_quotes'");

            ////HtmlNodeCollection data = htmlDocument.DocumentNode.SelectNodes("//table");
            //var rows = data.Descendants("tr").ToList();
            //List<List<string>> rowValues = new List<List<string>>();
            //foreach (var row in rows)
            //{
            //    List<string> currentRowValues = new List<string>();
            //    foreach (var column in row.ChildNodes)
            //    {
            //        currentRowValues.Add(column.InnerText);
            //    }
            //    rowValues.Add(currentRowValues);
            //}
            //var result = rowValues;

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
            //#endregion
        }
    }

    public class MyWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = base.GetWebRequest(address) as HttpWebRequest;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            return request;
        }
    }
}