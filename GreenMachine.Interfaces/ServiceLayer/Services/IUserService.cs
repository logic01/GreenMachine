using GreenMachine.Interfaces.ServiceLayer.Results;

namespace GreenMachine.Interfaces.ServiceLayer.Services
{
    public interface IUserService
    {
        IRegisterUserResult RegisterUser(string userName, string password);

        /// <summary>
        /// Perform a login for the provided user
        /// </summary>
        /// <param name="userName">The user who is logging in</param>
        /// <param name="password">The password the user has used to log in</param>
        /// <returns>A result object containing information relative to the action requested</returns>
        ILoginResult Login(string userName, string password);
    }
}