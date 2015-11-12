namespace GreenMachine.Layer.Data
{
    using System.Data.Entity;

    public interface IContext
    {
        IDbSet<Log> Logs { get; }
        IDbSet<UserInfo> UserInfo { get; }
        IDbSet<User> User { get; }

        int SaveChanges();
    }
}