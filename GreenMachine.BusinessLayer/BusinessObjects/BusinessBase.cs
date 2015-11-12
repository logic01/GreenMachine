using GreenMachine.Interfaces.Logging;
using GreenMachine.Layer.Data;

namespace GreenMachine.Layer.Business.BusinessObjects
{
    public class BusinessBase
    {
        public IContext _context;
        public ILoggingService _logingService;

        public BusinessBase(IContext context, ILoggingService logingService)
        {
            _context = context;
            _logingService = logingService;
        }
    }
}
