using System.ComponentModel.Composition;
using System.Web;
       
using GreenMachine.DependencyInjection;
using Microsoft.Practices.Unity;

namespace GreenMachine.Web.DependencyInjection
{
    [Export(typeof(IModule))]
    public class ModuleInit : IModule
    {
        public void Initialize(IUnityContainer container)
        {
            container.RegisterType<HttpContextBase>(new InjectionFactory(_ => new HttpContextWrapper(HttpContext.Current)));
        }
    }
}