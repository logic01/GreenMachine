using System;
using System.Linq;
using GreenMachine.Interfaces.BusinessLayer;
using GreenMachine.Interfaces.Logging;
using GreenMachine.Layer.Data;

namespace GreenMachine.Layer.Business.BusinessObjects
{
    public class UserBusiness : BusinessBase, IUserBusiness
    {
        private readonly IConfiguration _configuration;

        public UserBusiness(
            IContext context, 
            ILoggingService logingService, 
            IConfiguration configuration) : base(context, logingService)
        {
            _configuration = configuration;
        }

        public void SetSuccessfullLogin(long userId)
        {
            var user = _context.User.First(u => u.UserId == userId);

            user.LoginAttempts = 0;
            user.Locked = false;
            user.LastLoginDate = DateTime.Now;

            _context.SaveChanges();
        }

        public void SetInvalidLoginAttempts(long userId)
        {
            var user = _context.User.First(u => u.UserId == userId);

            user.LoginAttempts++;

            if (user.LoginAttempts == _configuration.LoginAttemptsAllowed)
                user.Locked = true;

            _context.SaveChanges();
        }

        public void SaveNewUser(string username, string password)
        {
            var now = DateTime.Now;

            var user = new User
            {
                UserName = username,
                Password = password,
                LoginAttempts = 0,
                Active = true,
                Locked = false,
                LastLoginDate = now,
                CreatedBy = username,
                CreatedOn = now,
                ModifiedBy = username,
                ModifiedOn = now
            };

            _context.User.Add(user);
            _context.SaveChanges();
        }
    }
}