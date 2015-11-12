using GreenMachine.Enums;

namespace GreenMachine.Interfaces.ServiceLayer.Results
{
    public interface IRegisterUserResult : IResult
    {
        /// <summary>
        /// The status of the action requested
        /// </summary>
        RegisterUserStatus Status { get; set; }
    }
}