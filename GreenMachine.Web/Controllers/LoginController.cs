using System;
using System.Web;
using System.Web.Mvc;
using GreenMachine.Enums;
using GreenMachine.Interfaces.ServiceLayer.Services;
using GreenMachine.Web.Models;

namespace GreenMachine.Web.Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;

        public LoginController(HttpContextBase httpContext, IUserService userService)
            : base(httpContext)
        {
            _userService = userService;
        }

        /// <summary>
        /// Load the login view
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// Attempt to login
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid) 
                return View(model);

            var result = _userService.Login(model.UserName, model.Password);

            switch (result.Status)
            {
                case LoginStatus.Success:
                    return RedirectToLocal(returnUrl);
                case LoginStatus.DoesNotExist:
                    ModelState.AddModelError(String.Empty, String.Format("The user name or password provided is incorrect."));
                    break;
                case LoginStatus.InvalidPassword:
                    ModelState.AddModelError(String.Empty, String.Format("The user name or password provided is incorrect. You have {0} attempts remaining.", result.AttemptsRemaining));
                    break;
                case LoginStatus.InActive:
                    ModelState.AddModelError(String.Empty, String.Format("This account has been deleted."));
                    break;
                case LoginStatus.Locked:
                    ModelState.AddModelError(String.Empty, "This account is locked.");
                    break;
            }

            return View(model);
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
