
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
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private readonly IUserManager _manager;
        private string WebUrl;
        private readonly string appDomain = HttpRuntime.AppDomainAppPath;
        private readonly string folderUpload = string.Format(@"{0}\Content\img\avatar", HttpRuntime.AppDomainAppPath);

        // GET: AdjustProductRequets
        public UserController()
        {
            _manager = new UserManager();
            WebUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";
        }

        [HttpGet]
        [Route("Search")]
        public IHttpActionResult Search([FromUri] USERParams model)
        {
            var result = _manager.Search(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("Add")]
        public IHttpActionResult Add(SYS_USERS model)
        {
            //model.CREATED_BY = IdentityHelper.UserName;
            var result = _manager.Add(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("Update")]
        public IHttpActionResult Update(SYS_USERS model)
        {
            //model.MODIFIED_BY = IdentityHelper.UserName;
            var result = _manager.Update(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("Delete")]
        public IHttpActionResult Delete(SYS_USERS model)
        {
            //Delete image avatar
            string targetFile = string.Format("{0}{1}",appDomain,model.AVATAR);
            if (File.Exists(targetFile))
                File.Delete(targetFile);

            var result = _manager.Delete(model);
            return Ok(result);
        }

        [Route("Upload")]
        public async Task<IHttpActionResult> Upload()
        {
            var result = new Result();
            try
            {
                var provider = FileUpload.GetMultipartProvider(folderUpload);
                var data = await Request.Content.ReadAsMultipartAsync(provider);
                // On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
                // so this is how you can get the original file name
                var originalFileName = FileUpload.GetDeserializedFileName(data.FileData.First());
                var fileExtension = Path.GetExtension(originalFileName).ToLower();

                // uploadedFileInfo object will give you some additional stuff like file length,
                // creation time, directory name, a few filesystem methods etc..
                var uploadedFileInfo = new FileInfo(data.FileData.First().LocalFileName);

                //string now = DateTime.Now.ConvertToTimestamp().ToString();
                var username = data.FormData.GetValues("username");

                string fileName = string.Format("{0}{1}", username[0], fileExtension);

                // Create full path for where to move the uploaded file
                string targetFile = Path.Combine(uploadedFileInfo.DirectoryName, fileName);

                //if (!fileExtension.Contains(".msg"))
                //{
                //    result.Code = (short)HttpStatusCode.BadGateway;
                //    result.Message = string.Format("Định dạng file không đúng. Hãy kiểm tra lại!");
                //    return Ok(result);
                //}

                // If the file in the full path exists, delete it first otherwise FileInfo.MoveTo() will throw exception
                if (File.Exists(targetFile))
                    File.Delete(targetFile);

                // Move the uploaded file to the target folder
                uploadedFileInfo.MoveTo(targetFile);

                var urlFileUpload = targetFile.Replace(HttpRuntime.AppDomainAppPath, "/").Replace("\\", "/");
                result.Code = (short)HttpStatusCode.OK;
                result.Data = urlFileUpload;
                result.Message = "Uploaded email successfully!";
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.Code = (short)HttpStatusCode.ExpectationFailed;
                result.Message = ex.Message;
                return Ok(result);
            }
        }
    }
}