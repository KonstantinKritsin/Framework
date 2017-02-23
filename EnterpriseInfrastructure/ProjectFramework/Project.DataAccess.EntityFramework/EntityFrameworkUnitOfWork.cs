using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Project.Framework.Common.Context;
using Project.Framework.Common.DependencyResolver;
using Project.Framework.Common.Exceptions;
using Project.Framework.DataAccess.Data;

namespace Project.DataAccess.EntityFramework
{
    public class EntityFrameworkUnitOfWork : UnitOfWork, IDbUnitOfWork
    {
        internal readonly IDictionary<Type, DbContext> DbContexts = new Dictionary<Type, DbContext>();

        public EntityFrameworkUnitOfWork(IDependencyResolver dependencyResolver) :
            base(dependencyResolver)
        { }

        public DbContext GetDbContext(Type type)
        {
            DbContext context;
            if (!DbContexts.TryGetValue(type, out context))
            {
                context = (DbContext)DependencyResolver.Get(type);
                context.Configuration.AutoDetectChangesEnabled = In<GlobalTransaction>();

                if (In<Transaction>())
                    throw new DataAccessException("UseGlobalTransaction");
                DbContexts.Add(type, context);
            }
            return context;
        }
        public bool InTransaction() => In<Transaction>();
        public bool InGlobalTransaction() => In<GlobalTransaction>();

        public ITransaction BegingGlobalTransaction(Framework.Common.Enums.IsolationLevel isolationLevel = Framework.Common.Enums.IsolationLevel.ReadCommitted)
        {
            if (In<Transaction>())
                throw new DataAccessException("Cannot enlist in the transaction because a local transaction is in progress on the connection.  Finish local transaction and retry.");
            var context = Get<GlobalTransaction>();
            context.Set(DbContexts, GetGlobalIsolationLevel(isolationLevel));
            return context;
        }

        public ITransaction BeginTransaction(Framework.Common.Enums.IsolationLevel isolationLevel = Framework.Common.Enums.IsolationLevel.ReadCommitted)
        {
            Context transactionContext;
            if (!Contexts.TryGetValue(typeof(Transaction), out transactionContext))
            {
                if (DbContexts.Count == 0)
                {
                    var defaultContext = DependencyResolver.Get<DbContext>();
                    DbContexts.Add(defaultContext.GetType(), defaultContext);
                }
                transactionContext = Get<Transaction>().Set(DbContexts, GetIsolationLevel(isolationLevel));
            }
            return (ITransaction)transactionContext;
        }

        public void Commit()
        {
            if (In<Transaction>() || In<GlobalTransaction>())
            {
                foreach (var dbContext in DbContexts.Values) dbContext.SaveChanges();
                //DbContexts.Clear();
                if (In<Transaction>())
                {
                    var transaction = (Transaction)Contexts[typeof(Transaction)];
                    transaction.Commit();
                    transaction.Dispose();
                }
                if (In<GlobalTransaction>())
                {
                    var transaction = (GlobalTransaction)Contexts[typeof(GlobalTransaction)];
                    transaction.Commit();
                    transaction.Dispose();
                }
            }
        }

        public async Task CommitAsync()
        {
            if (In<Transaction>() || In<GlobalTransaction>())
            {
                await Task.WhenAll(DbContexts.Values.Select(c => c.SaveChangesAsync()));
                //DbContexts.Clear();
                if (In<Transaction>())
                {
                    var transaction = (Transaction)Contexts[typeof(Transaction)];
                    transaction.Commit();
                    transaction.Dispose();
                }
                if (In<GlobalTransaction>())
                {
                    var transaction = (GlobalTransaction)Contexts[typeof(GlobalTransaction)];
                    transaction.Commit();
                    transaction.Dispose();
                }
            }
        }

        public T GetRepository<T>() where T : class, IRepository
        {
            var repo = DependencyResolver.Get<T>();
            repo.SetUnitOfWork(this);
            return repo;
        }

        public override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var dbContext in DbContexts.Values) dbContext.Dispose();
                DbContexts.Clear();
            }

            base.Dispose(disposing);
        }

        #region Isolation level map

        private static System.Transactions.IsolationLevel GetGlobalIsolationLevel(Framework.Common.Enums.IsolationLevel isolationLevel)
        {
            switch (isolationLevel)
            {
                case Framework.Common.Enums.IsolationLevel.Unspecified:
                    return System.Transactions.IsolationLevel.Unspecified;
                case Framework.Common.Enums.IsolationLevel.RepeatableRead:
                    return System.Transactions.IsolationLevel.RepeatableRead;
                case Framework.Common.Enums.IsolationLevel.ReadCommitted:
                    return System.Transactions.IsolationLevel.ReadCommitted;
                case Framework.Common.Enums.IsolationLevel.ReadUncommitted:
                    return System.Transactions.IsolationLevel.ReadUncommitted;
                case Framework.Common.Enums.IsolationLevel.Snapshot:
                    return System.Transactions.IsolationLevel.Snapshot;
                case Framework.Common.Enums.IsolationLevel.Chaos:
                    return System.Transactions.IsolationLevel.Chaos;
                default:
                    return System.Transactions.IsolationLevel.Serializable;
            }
        }
        private static System.Data.IsolationLevel GetIsolationLevel(Framework.Common.Enums.IsolationLevel isolationLevel)
        {
            switch (isolationLevel)
            {
                case Framework.Common.Enums.IsolationLevel.Unspecified:
                    return System.Data.IsolationLevel.Unspecified;
                case Framework.Common.Enums.IsolationLevel.RepeatableRead:
                    return System.Data.IsolationLevel.RepeatableRead;
                case Framework.Common.Enums.IsolationLevel.ReadCommitted:
                    return System.Data.IsolationLevel.ReadCommitted;
                case Framework.Common.Enums.IsolationLevel.ReadUncommitted:
                    return System.Data.IsolationLevel.ReadUncommitted;
                case Framework.Common.Enums.IsolationLevel.Snapshot:
                    return System.Data.IsolationLevel.Snapshot;
                case Framework.Common.Enums.IsolationLevel.Chaos:
                    return System.Data.IsolationLevel.Chaos;
                default:
                    return System.Data.IsolationLevel.Serializable;
            }
        }

        #endregion
    }
}