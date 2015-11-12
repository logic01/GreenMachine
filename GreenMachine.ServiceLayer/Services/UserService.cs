using System;
using System.Linq;
using GreenMachine.Enums;
using GreenMachine.Interfaces.BusinessLayer;
using GreenMachine.Interfaces.Logging;
using GreenMachine.Interfaces.ServiceLayer.Results;
using GreenMachine.Interfaces.ServiceLayer.Services;
using GreenMachine.Layer.Data;
using GreenMachine.Layer.Service.Results;

namespace GreenMachine.Layer.Service.Services
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly IUserBusiness _userBusiness;
        private readonly IConfiguration _configuration;

        public UserService(
            IContext context, 
            ILoggingService logingService,
            IConfiguration configuration,
            IUserBusiness userBusiness)
            : base(context, logingService)
        {
            _userBusiness = userBusiness;
            _configuration = configuration;
        }

        public IRegisterUserResult RegisterUser(string userName, string password)
        {
            var result = new RegisterUserResult();

            try
            {
                var existingUser = _context.User.FirstOrDefault(u => u.UserName == userName);

                if (existingUser != null)
                {
                    result.Status = RegisterUserStatus.ExistingUser;
                }
                else
                {
                    _userBusiness.SaveNewUser(userName, password);

                    result.Status = RegisterUserStatus.Success;
                }
            }
            catch (Exception ex)
            {
                _logingService.Log(ex);

                result.Status = RegisterUserStatus.Fail;
            }

            return result;
        }

        public ILoginResult Login(string userName, string password)
        {
            var result = new LoginResult();

            try
            {
                var user = _context.User.FirstOrDefault(u => u.UserName == userName);

                // Order is important!
                if (user == null)
                {
                    result.Status = LoginStatus.DoesNotExist;
                }
                else if (!user.Active)
                {
                    result.Status = LoginStatus.InActive;
                }
                else if (user.Locked)
                {
                    result.Status = LoginStatus.Locked;
                }
                else if (user.Password != password)
                {
                    _userBusiness.SetInvalidLoginAttempts(user.UserId);

                    result.AttemptsRemaining = _configuration.LoginAttemptsAllowed - user.LoginAttempts;
                    result.Status = LoginStatus.InvalidPassword;
                }
                else
                {
                    _userBusiness.SetSuccessfullLogin(user.UserId);

                    result.AttemptsRemaining = _configuration.LoginAttemptsAllowed;
                    result.Status = LoginStatus.Success;
                }
            }
            catch (Exception ex)
            {
                _logingService.Log(ex);

                result.Status = LoginStatus.Fail;
            }

            return result;
        }
    }
}
