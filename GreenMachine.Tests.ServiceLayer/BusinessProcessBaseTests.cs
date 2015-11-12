using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using GreenMachine.Interfaces.ServiceLayer.Results;
using GreenMachine.Layer.Service.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GreenMachine.Tests.ServiceLayer
{
    [TestClass]
    public class BusinessLayerBaseTests
    {
        [TestMethod]
        public void All_Business_Classes_Inherit_From_BusinessLayerBase()
        {
            Assembly businessProcessAssembly = typeof (ServiceBase).Assembly;

            foreach (Type type in businessProcessAssembly.GetTypes())
            {
                // We do not need any compiler generated classes
                if (type.IsDefined(typeof (CompilerGeneratedAttribute), false))
                    continue;

                // We know BusinessLayerBase inherits from BusinessLayerBase... duh
                if (type == typeof (ServiceBase))
                    continue;

                bool isBusinessProcess = type.IsSubclassOf(typeof (ServiceBase));
                bool isResultSet = typeof(IResult).IsAssignableFrom(type);

                if (isBusinessProcess || isResultSet)
                    continue;

                Assert.Fail("{0} should be either a BusinessProcessBase or IResult", type);
            }
        }
    }
}