
using CBD.BAL.Managers;
using CBD.Model;
using CBD.Model.Common;
using CBD.Model.Group;
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
    [RoutePrefix("api/Group")]
    public class GroupController : ApiController
    {
        private readonly IGroupManager _manager;
        private string WebUrl;

        // GET: AdjustProductRequets
        public GroupController()
        {
            _manager = new GroupManager();
            WebUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";
        }

        [HttpGet]
        [Route("Search")]
        public IHttpActionResult Search([FromUri] GROUPParams model)
        {
            var result = _manager.Search(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("Add")]
        public IHttpActionResult Add(SYS_GROUPS model)
        {
            //model.CREATED_BY = IdentityHelper.UserName;
            var result = _manager.Add(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("Update")]
        public IHttpActionResult Update(SYS_GROUPS model)
        {
            //model.MODIFIED_BY = IdentityHelper.UserName;
            var result = _manager.Update(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("Delete")]
        public IHttpActionResult Delete(SYS_GROUPS model)
        {
            var result = _manager.Delete(model);
            return Ok(result);
        }
    }
}