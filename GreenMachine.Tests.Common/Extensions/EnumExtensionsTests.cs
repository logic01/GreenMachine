using GreenMachine.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GreenMachine.Tests.Common.Extensions
{
    [TestClass]
    public class EnumExtensionsTest
    {
        #region ToDescription Tests

        private enum TestEnum
        {
            Default = -1,
            NoDescription = 0,
            [System.ComponentModel.Description("Two Words")]
            TwoWords = 2
        }

        [TestMethod]
        public void Enum_Description_Missing()
        {
            var noDescription = TestEnum.NoDescription.ToDescription();
            Assert.AreEqual(noDescription, TestEnum.NoDescription.ToString());
        }

        [TestMethod]
        public void Enum_Description_Is_Two_Words()
        {
            var twoWords = TestEnum.TwoWords.ToDescription();
            Assert.AreEqual(twoWords, "Two Words");
        }

        [TestMethod]
        public void String_Converts_To_Enum()
        {
            var myEnumValue = "Two Words".ToEnum(TestEnum.Default);

            Assert.AreEqual(myEnumValue, TestEnum.TwoWords);
        }

        [TestMethod]
        public void String_Converts_To_Enum_Returns_Default()
        {
            var myEnumValue = "".ToEnum(TestEnum.Default);

            Assert.AreEqual(myEnumValue, TestEnum.Default);
        }

        #endregion
    }
}
