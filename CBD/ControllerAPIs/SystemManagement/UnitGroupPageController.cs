
using CBD.BAL.Managers;
using CBD.Model;
using CBD.Model.UnitGroupPage;
using System.Web;
using System.Web.Http;

namespace CBD.Administration.ControllerAPIs
{
    //[Authorize]
    [RoutePrefix("api/UnitGroupPage")]
    public class UnitGroupPageController : ApiController
    {
        private readonly IUnitGroupPageManager _manager;
        //private string WebUrl;

        // GET: AdjustProductRequets
        public UnitGroupPageController()
        {
            _manager = new UnitGroupPageManager();
            //WebUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";
        }

        [HttpGet]
        [Route("Search")]
        public IHttpActionResult Search([FromUri] UNITGROUPPAGEParams model)
        {
            var result = _manager.Search(model);
            return Ok(result);
        }

        //[HttpPost]
        //[Route("Add")]
        //public IHttpActionResult Add(SYS_UNITS model)
        //{
        //    //model.CREATED_BY = IdentityHelper.UserName;
        //    var result = _manager.Add(model);
        //    return Ok(result);
        //}

        //[HttpPost]
        //[Route("Update")]
        //public IHttpActionResult Update(SYS_UNITS model)
        //{
        //    //model.MODIFIED_BY = IdentityHelper.UserName;
        //    var result = _manager.Update(model);
        //    return Ok(result);
        //}

        //[HttpPost]
        //[Route("Delete")]
        //public IHttpActionResult Delete(SYS_UNITS model)
        //{
        //    var result = _manager.Delete(model);
        //    return Ok(result);
        //}
    }
}