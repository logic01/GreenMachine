using System;
using System.Linq;
using GreenMachine.Interfaces.BusinessLayer;
using GreenMachine.Interfaces.Logging;
using GreenMachine.Layer.Business.BusinessObjects;
using GreenMachine.Layer.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GreenMachine.Tests.BusinessLayer
{

    [TestClass]
    public class UserBusinessTests
    {
        private readonly UserBusiness _userBusiness;

        private readonly Mock<IConfiguration> _configMock = new Mock<IConfiguration>();
        private readonly Mock<ILoggingService> _loggingMock = new Mock<ILoggingService>();
        private readonly Mock<IContext> _contextMock = new Mock<IContext>();

        public UserBusinessTests()
        {
            _userBusiness = new UserBusiness(_contextMock.Object, _loggingMock.Object, _configMock.Object);
        }

        private User GetStandardUser()
        {
            var now = DateTime.Now.AddSeconds(-3); // We wan't to make sure the time actually changed

            return new User()
            {
                UserId = 0,
                UserName = "Name",
                Password = "Password",
                LastLoginDate = now,
                LoginAttempts = 3,
                Locked = true,
                Active = true,
                CreatedOn = now,
                CreatedBy = "CreatedBy",
                ModifiedOn = now,
                ModifiedBy = "ModifiedBy"
            };
        }

        [TestMethod]
        public void SetSuccessfullLogin_Success()
        {
            var user = GetStandardUser();

            var lastLoginOriginal = user.LastLoginDate;

            _contextMock.Setup(m => m.User).Returns(new FakeDbSet<User> { user });

            _userBusiness.SetSuccessfullLogin(user.UserId);

           var afterUser = _contextMock.Object.User.First(u => u.UserId == user.UserId);

            Assert.AreEqual(afterUser.LoginAttempts, 0);
            Assert.AreEqual(afterUser.Locked, false);
            Assert.AreNotEqual(lastLoginOriginal, afterUser.LastLoginDate);
        }        

        [TestMethod]
        public void SetInvalidLoginAttempts_NotLocked()
        {
            var user = GetStandardUser();

            var loginAttempts = user.LoginAttempts;

            _contextMock.Setup(m => m.User).Returns(new FakeDbSet<User> { user });

            _userBusiness.SetInvalidLoginAttempts(user.UserId);

            Assert.AreEqual(++loginAttempts, user.LoginAttempts);
        }

        [TestMethod]
        public void SetInvalidLoginAttempts_UserGetsLocked()
        {
            var user = GetStandardUser();

            // Set to one less then max
            user.LoginAttempts = _configMock.Object.LoginAttemptsAllowed - 1;

            var loginAttempts = user.LoginAttempts;

            _contextMock.Setup(m => m.User).Returns(new FakeDbSet<User> { user });

            _userBusiness.SetInvalidLoginAttempts(user.UserId);

            Assert.AreEqual(++loginAttempts, user.LoginAttempts);
            Assert.AreEqual(true, user.Locked);
        }

    }
}