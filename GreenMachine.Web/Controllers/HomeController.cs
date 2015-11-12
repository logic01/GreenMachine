using System;
using System.Web.Mvc;

namespace GreenMachine.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// Load the primary home page.
        /// </summary>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///  Redirect the user to the login page.
        /// </summary>
        [HttpGet]
        public ActionResult Login()
        {
            var url = String.Empty;

            if (Request != null && Request.Url != null)
                url = Request.Url.AbsoluteUri;

            return RedirectToAction("Login", "Login", url);
        }
    }
}
