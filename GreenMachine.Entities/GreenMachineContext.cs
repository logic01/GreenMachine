using System.Data.Common;

namespace GreenMachine.Layer.Data
{
    public partial class GreenMachineContext
    {
        public GreenMachineContext(DbConnection connection)
            : base(connection, true)
		{
		    this.Configuration.LazyLoadingEnabled = false;
		}
    }
}
