using System.Threading.Tasks;
using Project.Framework.Common.Context;
using Project.Framework.Common.Enums;

namespace Project.Framework.DataAccess.Data
{
    public interface IDbUnitOfWork : IUnitOfWork
    {
        bool InTransaction();
        bool InGlobalTransaction();
        ITransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        ITransaction BegingGlobalTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        void Commit();
        Task CommitAsync();
        T GetRepository<T>() where T : class, IRepository;
    }
}