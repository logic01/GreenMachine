using System;
using System.Web;
using System.Web.Mvc;

namespace GreenMachine.Web.Controllers
{
    public class ControllerBase : Controller
    {
        protected readonly HttpContextBase _httpContext;

        public ControllerBase()
            : this(new HttpContextWrapper(System.Web.HttpContext.Current))
        {
        }

        public ControllerBase(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            _httpContext = httpContext;
        }
    }
}