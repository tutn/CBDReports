
using CBD.BAL.Managers;
using CBD.Model;
using CBD.Model.Common;
using CBD.Model.User;
using CBD.Model.Utilities;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CBD.Administration.ControllerAPIs
{
    //[Authorize]
    [RoutePrefix("api/GroupUser")]
    public class GroupUserController : ApiController
    {
        private readonly IGroupUserManager _manager;
        //private string WebUrl;
        private readonly string appDomain = HttpRuntime.AppDomainAppPath;
        private readonly string folderUpload = string.Format(@"{0}\Content\img\avatar", HttpRuntime.AppDomainAppPath);

        // GET: AdjustProductRequets
        public GroupUserController()
        {
            _manager = new GroupUserManager();
            //WebUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";
        }

        [HttpGet]
        [Route("Search")]
        public IHttpActionResult Search([FromUri] USERParams model)
        {
            var result = _manager.Search(model);
            return Ok(result);
        }

        //[HttpPost]
        //[Route("Add")]
        //public IHttpActionResult Add(SYS_USERS model)
        //{
        //    //model.CREATED_BY = IdentityHelper.UserName;
        //    var result = _manager.Add(model);
        //    return Ok(result);
        //}

        //[HttpPost]
        //[Route("Update")]
        //public IHttpActionResult Update(SYS_USERS model)
        //{
        //    //model.MODIFIED_BY = IdentityHelper.UserName;
        //    var result = _manager.Update(model);
        //    return Ok(result);
        //}

        //[HttpPost]
        //[Route("Delete")]
        //public IHttpActionResult Delete(SYS_USERS model)
        //{
        //    //Delete image avatar
        //    string targetFile = string.Format("{0}{1}",appDomain,model.AVATAR);
        //    if (File.Exists(targetFile))
        //        File.Delete(targetFile);

        //    var result = _manager.Delete(model);
        //    return Ok(result);
        //}
    }
}