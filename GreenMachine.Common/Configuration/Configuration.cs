using System;
using System.Configuration;
using GreenMachine.Interfaces.BusinessLayer;

namespace GreenMachine.Common.Configuration
{
    public class Configuration : IConfiguration
    {
        public int LoginAttemptsAllowed
        {
            get
            {
                var attempts = ConfigurationManager.AppSettings["LoginAttemptsAllowed"];

                if (String.IsNullOrEmpty(attempts))
                    return 3;

                return Convert.ToInt32(attempts);
            }
        }
    }
}