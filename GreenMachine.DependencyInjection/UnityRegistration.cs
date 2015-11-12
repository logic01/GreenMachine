using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;

namespace GreenMachine.DependencyInjection
{
    public static class UnityRegistration
    {
        public static void Register(IUnityContainer container)
        {
            RegisterConventions(container); // MyClass : IMyClass
            RegisterUnconventional(container); // MyFunkyClass : ISomeFunkyInterface
        }

        /// <summary>
        /// Use the module loader to locate the IModule implementations and call their registrations 
        /// </summary>
        private static void RegisterUnconventional(IUnityContainer container)
        {
            ModuleLoader.LoadContainer(container, AppDomain.CurrentDomain.SetupInformation.PrivateBinPath, "GreenMachine.*.dll");
        }

        /// <summary>
        /// Register all interfaces to types that fit the unity conventions
        /// </summary>
        private static void RegisterConventions(IUnityContainer container)
        {
            // Get the assembly which contains all of our interfaces
            var interfaceAssembly = AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name.Contains("GreenMachine.Interface"));

            // Get all the interfaces in this assembly
            var interfaceTypes = interfaceAssembly.GetTypes().Where(t => t.IsInterface).ToList();

            // Get all of the applications assemblies loaded into the application domain
            var greenAssemblies =
                AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => a.GetName().Name.Contains("GreenMachine"));

            foreach (var assembly in greenAssemblies)
            {
                foreach (var interfaceType in interfaceTypes)
                {
                    var implementations = GetImplementationsOfInterface(assembly, interfaceType);

                    // Register the interface and implementation into our container
                    implementations.ForEach(i => container.RegisterType(interfaceType, i));
                }
            }
        }

        /// <summary>
        /// Retriev a list of types that implement the provided interface in the provided assembly
        /// </summary>
        private static List<Type> GetImplementationsOfInterface(Assembly assembly, Type interfaceType)
        {
            var implementations = new List<Type>();

            var concreteTypes = assembly.GetTypes().Where(t =>
                !t.IsInterface &&
                !t.IsAbstract &&
                interfaceType.IsAssignableFrom(t));

            concreteTypes.ToList().ForEach(implementations.Add);

            return implementations;
        }
    }
}