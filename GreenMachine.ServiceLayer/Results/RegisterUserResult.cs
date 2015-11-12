using GreenMachine.Enums;
using GreenMachine.Interfaces.ServiceLayer.Results;

namespace GreenMachine.Layer.Service.Results
{
    public class RegisterUserResult : IRegisterUserResult
    {
        public RegisterUserStatus Status { get; set; }
    }
}