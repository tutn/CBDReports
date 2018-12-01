
using CBD.BAL.Managers;
using CBD.Model;
using CBD.Model.Sys_Page;
using System.Web;
using System.Web.Http;

namespace CBD.Administration.ControllerAPIs
{
    //[Authorize]
    [RoutePrefix("api/Page")]
    public class PageController : ApiController
    {
        private readonly IPageManager _manager;
        private string WebUrl;

        // GET: AdjustProductRequets
        public PageController()
        {
            _manager = new PageManager();
            WebUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";
        }

        [HttpGet]
        [Route("Search")]
        public IHttpActionResult Search([FromUri] PAGEParams model)
        {
            var result = _manager.Search(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("Add")]
        public IHttpActionResult Add(SYS_PAGES model)
        {
            //model.CREATED_BY = IdentityHelper.UserName;
            var result = _manager.Add(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("Update")]
        public IHttpActionResult Update(SYS_PAGES model)
        {
            //model.MODIFIED_BY = IdentityHelper.UserName;
            var result = _manager.Update(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("Delete")]
        public IHttpActionResult Delete(SYS_PAGES model)
        {
            var result = _manager.Delete(model);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllPages")]
        public IHttpActionResult GetAllPages([FromUri] int? PageId)
        {
            var result = _manager.GetAllPages(PageId);
            return Ok(result);
        }
    }
}