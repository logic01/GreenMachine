namespace GreenMachine.Layer.Data
{
    using System.Data.Entity;

    public class Context : IContext
    {
        private readonly GreenMachineContext _data;

        public Context()
        {
            _data = new GreenMachineContext();
        }

        public IDbSet<Log> Logs
        {
            get { return _data.Logs; }
        }

        public IDbSet<UserInfo> UserInfo
        {
            get { return _data.UserInfo; }
        }

        public IDbSet<User> User
        {
            get { return _data.User; }
        }

        public int SaveChanges()
        {
            return _data.SaveChanges();
        }
    }
}
