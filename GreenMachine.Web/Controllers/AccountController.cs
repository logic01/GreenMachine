using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GreenMachine.Interfaces.ServiceLayer.Services;
using GreenMachine.Web.Models;

namespace GreenMachine.Web.Controllers
{
    public class AccountController : ControllerBase
    {
       private readonly IUserService _userService;

        public AccountController(
            HttpContextBase httpContext,
            IUserService userService)
            : base(httpContext)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterUserModel userModel)
        {
            if (!ModelState.IsValid) 
                return View(userModel);

            // Attempt to register the new user
            try
            {
                var registerResult = _userService.RegisterUser(userModel.UserName, userModel.Password);

                var loginResult = _userService.Login(userModel.UserName, userModel.Password);

                return RedirectToAction("Index", "Home");
            }
            catch (MembershipCreateUserException e)
            {
                ModelState.AddModelError("", e.StatusCode.ToString());
            }

            // If we got this far, something failed, redisplay form
            return View(userModel);
        }
    }
}