using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GreenMachine.Layer.Service.Services;
using GreenMachine.Interfaces.BusinessLayer;
using GreenMachine.Interfaces.Logging;
using GreenMachine.Layer.Data;
using Moq;
using GreenMachine.Enums;

namespace GreenMachine.Tests.ServiceLayer
{
    [TestClass]
    public class UserServiceTests
    {
        private readonly UserService _userService;

        private readonly Mock<IConfiguration> _configMock = new Mock<IConfiguration>();
        private readonly Mock<ILoggingService> _loggingMock = new Mock<ILoggingService>();
        private readonly Mock<IContext> _contextMock = new Mock<IContext>();
        private readonly Mock<IUserBusiness> _userBusinessMock = new Mock<IUserBusiness>();

        public UserServiceTests()
        {
            _userService = new UserService(_contextMock.Object, _loggingMock.Object, _configMock.Object, _userBusinessMock.Object);
        }

        [TestMethod]
        public void Login_UserDoesNotExist()
        {
            _contextMock.Setup(m => m.User).Returns(new FakeDbSet<User> { new User() });

            var result = _userService.Login("doesnotexist", "");

            Assert.AreEqual(LoginStatus.DoesNotExist, result.Status);
        }

        [TestMethod]
        public void Login_UserNotActive()
        {
            var now = DateTime.Now;

            var user = new User()
            {
                UserId = 0,
                UserName = "Name",
                Password = "Password",
                LastLoginDate = now,
                LoginAttempts = 3,
                Locked = false,
                Active = false,
                CreatedOn = now,
                CreatedBy = "CreatedBy",
                ModifiedOn = now,
                ModifiedBy = "ModifiedBy"
            };

            _contextMock.Setup(m => m.User).Returns(new FakeDbSet<User> { user });

            var result = _userService.Login(user.UserName, user.Password);

            Assert.AreEqual(LoginStatus.InActive, result.Status);
        }

        [TestMethod]
        public void Login_UserIsLocked()
        {
            var now = DateTime.Now;

            var user = new User()
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

            _contextMock.Setup(m => m.User).Returns(new FakeDbSet<User> { user });

            var result = _userService.Login(user.UserName, user.Password);

            Assert.AreEqual(LoginStatus.Locked, result.Status);
        }

        [TestMethod]
        public void Login_InvalidPassword()
        {
            var now = DateTime.Now;

            var user = new User()
            {
                UserId = 0,
                UserName = "Name",
                Password = "Password",
                LastLoginDate = now,
                LoginAttempts = 3,
                Locked = false,
                Active = true,
                CreatedOn = now,
                CreatedBy = "CreatedBy",
                ModifiedOn = now,
                ModifiedBy = "ModifiedBy"
            };

            _contextMock.Setup(m => m.User).Returns(new FakeDbSet<User> { user });

            var result = _userService.Login(user.UserName, "wrongpassword");

            Assert.AreEqual(LoginStatus.InvalidPassword, result.Status);
        }

        [TestMethod]
        public void Login_Success()
        {
            var now = DateTime.Now;

            var user = new User()
            {
                UserId = 0,
                UserName = "Name",
                Password = "Password",
                LastLoginDate = now,
                LoginAttempts = 3,
                Locked = false,
                Active = true,
                CreatedOn = now,
                CreatedBy = "CreatedBy",
                ModifiedOn = now,
                ModifiedBy = "ModifiedBy"
            };

            _contextMock.Setup(m => m.User).Returns(new FakeDbSet<User> { user });

            //_userBusinessMock.Setup(m => m.SetSuccessfullLogin)
            var result = _userService.Login(user.UserName, user.Password);

            Assert.AreEqual(LoginStatus.Success, result.Status);
            Assert.AreEqual(_configMock.Object.LoginAttemptsAllowed, result.AttemptsRemaining);
        }
    }
}
