using GreenMachine.Enums;
using GreenMachine.Interfaces.ServiceLayer.Results;

namespace GreenMachine.Layer.Service.Results
{
    public class LoginResult : ILoginResult
    {
        public LoginStatus Status { get; set; }
        public int AttemptsRemaining { get; set; }

        public LoginResult()
        {
            AttemptsRemaining = 0;
        }
    }
}