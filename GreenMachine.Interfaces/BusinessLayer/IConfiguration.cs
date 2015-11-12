namespace GreenMachine.Interfaces.BusinessLayer
{
    public interface IConfiguration
    {
        /// <summary>
        /// The number of times a user can attempt to login before we lock the account
        /// </summary>
        /// <returns></returns>
        int LoginAttemptsAllowed {  get; }
    }
}