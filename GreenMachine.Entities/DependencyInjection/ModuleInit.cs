using System.ComponentModel.Composition;
using GreenMachine.DependencyInjection;
using Microsoft.Practices.Unity;

namespace GreenMachine.Layer.Data.DependencyInjection
{
    [Export(typeof(IModule))]
    public class ModuleInit : IModule
    {
        public void Initialize(IUnityContainer container)
        {
            //PerResolve will use the same instance accross all uses of SmartMoveContext during the current Resolve Lifetime 
            container.RegisterType<IContext, Context>(new PerResolveLifetimeManager(), new InjectionConstructor());
        }
    }
}
