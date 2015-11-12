using GreenMachine.Enums;

namespace GreenMachine.Interfaces.ServiceLayer.Results
{
    public interface ILoginResult : IResult
    {
        /// <summary>
        /// The status of the login attempt
        /// </summary>
        LoginStatus Status { get; set; }

        /// <summary>
        /// How mayn attempts are available before the account is locked
        /// </summary>
        int AttemptsRemaining { get; set; }
    }
}