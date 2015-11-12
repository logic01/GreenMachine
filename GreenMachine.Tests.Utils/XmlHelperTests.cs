using System;
using System.IO;
using System.Xml.Serialization;
using GreenMachine.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GreenMachine.Tests.Utils
{
    [TestClass]
    public class XmlHelperTest
    {
        public class TestingObject
        {
            public string _firstName = "First";
            public string _lastName = "Last";

            public TestingObject()
            {
            }

            public TestingObject(string firstName, string lastName)
            {
                _firstName = firstName;
                _lastName = lastName;
            }
        }

        #region Serialize To Xml String

        [TestMethod]
        public void Object_Serializes_To_XmlString()
        {
            var testObject = new TestingObject();
            string testString;

            using (var writer = new StringWriter())
            {
                var serializer = new XmlSerializer(testObject.GetType());

                serializer.Serialize(writer, testObject);

                testString = writer.ToString();
            }

            // Code Under Test
            var xmlString = XmlHelper.SerializeToXmlString(testObject);

            Assert.AreEqual(testString, xmlString);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Null_Object_Serializes_To_XmlString()
        {
            TestingObject testObject = null;

            var xmlString = XmlHelper.SerializeToXmlString(testObject);
        }

        #endregion
    }
}
