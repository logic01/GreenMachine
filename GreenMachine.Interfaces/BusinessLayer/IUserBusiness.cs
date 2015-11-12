namespace GreenMachine.Interfaces.BusinessLayer
{
    public interface IUserBusiness
    {
        /// <summary>
        ///  Mark a user account as successfully logged in
        /// </summary>
        /// <param name="userId"></param>
        void SetSuccessfullLogin(long userId);

        /// <summary>
        /// Mark a user account as an invalid login attempt
        /// </summary>
        /// <param name="userId"></param>
        void SetInvalidLoginAttempts(long userId);

        /// <summary>
        /// Save a new user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        void SaveNewUser(string userName, string password);
    }
}
