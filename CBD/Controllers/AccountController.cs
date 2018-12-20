using CBD.Model.Configuration;
using CBD.Models;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CBD.BAL.Managers;
using CBD.Model.Common;
using CBD.Model;
using CBD.Model.Utilities;
using System.Net;

namespace CBD.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserManager _manager;
        public AccountController()
        {
            _manager = new UserManager();
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            var isLogin = false;
            if (ModelState.IsValid)
            {
                Result result = new Result();
                //var user = (HR_LOGIN)_manager.GetUserbyUsername(model.UserName, model.Password).Data;
                var user = new SYS_USERS
                {
                    USER_NAME = model.UserName,
                    PASSWORD = SHAUtils.ToSHAPassword(model.Password),
                };
               
                if (SystemConfiguration.UserNotLdap.Contains(model.UserName))
                {
                    if (model.Password.Equals("VPBank" + model.UserName.ToLower()))
                    {
                        //Save cookie when Login success
                        isLogin = true;
                        var data = _manager.GetByUser(user);
                        if (data.Code == (short)HttpStatusCode.OK)
                        {
                            var us = (SYS_USERS)data.Data;
                            FormsAuthentication.SetAuthCookie(us.USER_NAME + '|' + us.GROUP_NAME + '|' + us.UNIT_NAME, true);
                        }
                        else
                        {
                            FormsAuthentication.SetAuthCookie(model.UserName + '|' + model.UserName + '|' + model.UserName, true);
                        }
                    }
                }
                else
                {
                    var data = _manager.GetByUser(user);
                    if (data.Code != (short)HttpStatusCode.OK)
                    {
                        var us = (SYS_USERS)data.Data;
                        FormsAuthentication.SetAuthCookie(us.USER_NAME + '|' + us.GROUP_NAME + '|' + us.UNIT_NAME, true);
                    }

                }

                //FormsAuthentication.SetAuthCookie(user.USER_NAME + '|' + user.TOKEN, true);
                //ViewBag.UserID = user.USER_NAME;
                if (returnUrl == null || returnUrl == "")
                    return RedirectToAction("Index", "Home");
                return RedirectToLocal(returnUrl);
               
               

               
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or Password.");
                return View(model);
            }
        }

        public ActionResult LogOff()
        {
            //AuthenticationManager.SignOut();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}