using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using GreenMachine.Layer.Business.BusinessObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GreenMachine.Tests.BusinessLayer
{
    [TestClass]
    public class BusinessLayerBaseTests
    {
        [TestMethod]
        public void All_Business_Classes_Inherit_From_BusinessLayerBase()
        {
            Assembly businessLayerAssembly = typeof(BusinessBase).Assembly;

            foreach (Type type in businessLayerAssembly.GetTypes())
            {
                // We do not need any compiler generated classes
                if (type.IsDefined(typeof(CompilerGeneratedAttribute), false))
                    continue;

                // We know BusinessLayerBase inherits from BusinessLayerBase... duh
                if (type == typeof(BusinessBase))
                    continue;

                Assert.IsTrue(type.IsSubclassOf(typeof(BusinessBase)), String.Format("{0} is not a subclass of BusinessBase", type));
            }
        }
    }
}