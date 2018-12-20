
using CBD.BAL.Managers;
using CBD.Model;
using CBD.Model.Common;
using CBD.Model.User;
using CBD.Model.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace CBD.Administration.ControllerAPIs
{
    //[Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly IUserManager _manager;
        private string WebUrl;
        private readonly string appDomain = HttpRuntime.AppDomainAppPath;
        private readonly string folderUpload = string.Format(@"{0}\Content\img\avatar", HttpRuntime.AppDomainAppPath);

        // GET: AdjustProductRequets
        public AccountController()
        {
            _manager = new UserManager();
            WebUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";
        }

        #region Public Methods
        [HttpPost]
        [Route("Login")]
        [ResponseType(typeof(LoginViewModel))]
        public HttpResponseMessage Login(LoginViewModel model)
        {
            HttpResponseMessage response = null;
            if (ModelState.IsValid)
            {
                var result = new Result();
                try
                {
                    var user = Membership.GetUser(model.UserName);
                    if (user == null)
                    {
                        result.Code = (short)HttpStatusCode.NotFound; ;
                        result.Message = "User's informations did not find, please check again!";
                        response = Request.CreateResponse(result);
                        return response;
                    }
                    var isvalid = Membership.ValidateUser(model.UserName, model.Password);
                    if (!isvalid)
                    {
                        result.Code = (short)HttpStatusCode.NotFound; ;
                        result.Message = "User's informations did not find, please check again!";
                        response = Request.CreateResponse(result);
                        return response;
                    }

                    object dbUser;
                    var token = CreateToken(user, out dbUser);
                    result.Code = 0;
                    result.Data = token;

                    response = Request.CreateResponse(result);
                }
                catch (Exception ex)
                {
                    result.Code = (short)HttpStatusCode.ExpectationFailed; ;
                    result.Message = "User's informations did not find, plaease check again!";
                    response = Request.CreateResponse(result);
                    return response;
                }

            }
            else
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            return response;
        }

        // POST api/User/Logout
        [HttpPost]
        [Route("Logout")]
        [ResponseType(typeof(string))]
        public HttpResponseMessage Logout(string token)
        {
            HttpResponseMessage response = null;
            try
            {
                this.Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);

                using (var client = new HttpClient())
                {
                    // New code:
                    //client.BaseAddress = new Uri(ConfigurationManager.AppSettings["notification_server"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "key=secretKey");

                    var postData = new
                    {
                        token = token
                    };

                    var result = new Result() { Code = (short)HttpStatusCode.OK, Data = postData, Message = "Logout successfully." };
                    response = Request.CreateResponse(result);

                }
            }
            catch (Exception ex)
            {
                var result = new Result() { Code = (short)HttpStatusCode.ExpectationFailed, Message = "Logout unsuccessfully." };
                response = Request.CreateResponse(result);
            }

            return response;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Create a Jwt with user information
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dbUser"></param>
        /// <returns></returns>
        private static string CreateToken(MembershipUser user, out object dbUser)
        {
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var expiry = Math.Round((DateTime.UtcNow.AddHours(12) - unixEpoch).TotalSeconds);
            var issuedAt = Math.Round((DateTime.UtcNow - unixEpoch).TotalSeconds);
            var notBefore = Math.Round((DateTime.UtcNow.AddHours(12) - unixEpoch).TotalSeconds);

            var payload = new Dictionary<string, object>
            {
                {"email", user.Email},
                {"username", user.UserName},
                {"fullname", user.UserName},
                {"userId", user.ProviderUserKey},
                {"role", "Admin"  },
                {"sub", user.ProviderUserKey},
                {"nbf", notBefore},
                {"iat", issuedAt},
                {"exp", expiry} 
            };

            //var secret = ConfigurationManager.AppSettings.Get("jwtKey");
            const string apikey = "secretKey";

            var serializer = new JsonNetSerializer();
            var urlEncoder = new JwtBase64UrlEncoder();
            var encoder = new JwtEncoder(new HMACSHA256Algorithm(), serializer, urlEncoder);

            var token = encoder.Encode(payload, apikey);

            dbUser = new { user.UserName, user.ProviderUserKey, };
            return token;
        }

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }
        #endregion
    }
}