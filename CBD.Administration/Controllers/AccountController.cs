using CBD.Model.Configuration;
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
using CBD.Administration.Models;
using ldapif;

namespace CBD.Administration.Controllers
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
            Result result = new Result();
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new SYS_USERS
                    {
                        USER_NAME = model.UserName,
                        PASSWORD = SHAUtils.ToSHAPassword(model.Password),
                    };
                    if (SystemConfiguration.UserNotLdap.Contains(user.USER_NAME))
                    {
                        var defaultPassword = SHAUtils.ToSHAPassword(string.Format("{0}{1}", SystemConfiguration.PREDEFAULTPASSWORD, model.UserName.ToLower()));
                        if (user.PASSWORD.Equals(defaultPassword))
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
                        if (data.Code == (short)HttpStatusCode.OK)
                        {
                            var us = (SYS_USERS)data.Data;
                            FormsAuthentication.SetAuthCookie(us.USER_NAME + '|' + us.GROUP_NAME + '|' + us.UNIT_NAME, true);
                            if (!string.IsNullOrEmpty(us.PASSWORD))
                            {
                                string name = "uid=" + us.USER_NAME.ToUpper() + ",ou=people,ou=user,dc=vpb,dc=com,dc=vn";
                                LDAPUSER._ldapServerName = "10.36.10.101";
                                LDAPUSER._port = 389;
                                string strAuthen = LDAPUSER.IsAuthenticate(name, model.Password).Trim();
                                if (strAuthen.Equals("Thực hiện thành công"))
                                    isLogin = true;
                                else
                                    result.Message = strAuthen;
                            }
                            else
                            {
                                if (model.Password.ToSHAPassword() == us.PASSWORD)
                                    isLogin = true;
                                else
                                    result.Message = string.Format("User or Password incorrect. Please check again!");
                            }
                        }

                    }

                    //FormsAuthentication.SetAuthCookie(user.USER_NAME + '|' + user.TOKEN, true);
                    //ViewBag.UserID = user.USER_NAME;
                    if (returnUrl == null || returnUrl == "")
                        return RedirectToAction("Index", "Home");
                    return RedirectToLocal(returnUrl);
                }
                catch (Exception ex)
                {
                    return View(result);
                }               
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