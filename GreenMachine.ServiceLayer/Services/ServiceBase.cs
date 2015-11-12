using GreenMachine.Interfaces.Logging;
using GreenMachine.Layer.Data;

namespace GreenMachine.Layer.Service.Services
{
    public class ServiceBase
    {
        public IContext _context;
        public ILoggingService _logingService;

        public ServiceBase(IContext context, ILoggingService logingService)
        {
            _context = context;
            _logingService = logingService;
        }
    }
}
