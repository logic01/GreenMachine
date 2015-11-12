using Microsoft.Practices.Unity;

namespace GreenMachine.DependencyInjection
{
    public interface IModule
    {
        void Initialize(IUnityContainer container);
    }
}
