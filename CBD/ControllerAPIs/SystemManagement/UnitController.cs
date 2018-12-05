
using CBD.BAL.Managers;
using CBD.Model;
using CBD.Model.Unit;
using System.Web;
using System.Web.Http;

namespace CBD.Administration.ControllerAPIs
{
    //[Authorize]
    [RoutePrefix("api/Unit")]
    public class UnitController : ApiController
    {
        private readonly IUnitManager _manager;
        private string WebUrl;

        // GET: AdjustProductRequets
        public UnitController()
        {
            _manager = new UnitManager();
            WebUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";
        }

        [HttpGet]
        [Route("Search")]
        public IHttpActionResult Search([FromUri] UNITParams model)
        {
            var result = _manager.Search(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("Add")]
        public IHttpActionResult Add(SYS_UNITS model)
        {
            //model.CREATED_BY = IdentityHelper.UserName;
            var result = _manager.Add(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("Update")]
        public IHttpActionResult Update(SYS_UNITS model)
        {
            //model.MODIFIED_BY = IdentityHelper.UserName;
            var result = _manager.Update(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("Delete")]
        public IHttpActionResult Delete(SYS_UNITS model)
        {
            var result = _manager.Delete(model);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllUnits")]
        public IHttpActionResult GetAllUnits([FromUri] int? UnitId)
        {
            var result = _manager.GetAllUnits(UnitId);
            return Ok(result);
        }

        //[HttpGet]
        //[Route("GetUnitNodes")]
        //public IHttpActionResult GetUnitNodes()
        //{
        //    var result = _manager.GetUnitNodes();
        //    return Ok(result);
        //}
    }
}