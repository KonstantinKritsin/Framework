using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using Project.Framework.Common.Context;
using Project.Framework.Common.Exceptions;
using Project.Framework.DataAccess.Data;

namespace Project.DataAccess.EntityFramework
{
    internal class Transaction : Context<IDictionary<Type, DbContext>, System.Data.IsolationLevel>, ITransaction
    {
        private IDictionary<Type, DbContext> _dbContexts;
        private DbContextTransaction _dbtransaction;
        private bool _commited;
        private bool _disposed;

        public DbTransaction UnderlyingTransaction => _dbtransaction?.UnderlyingTransaction;

        public override Context<IDictionary<Type, DbContext>, System.Data.IsolationLevel> Set(IDictionary<Type, DbContext> dbContexts, System.Data.IsolationLevel level)
        {
            _dbContexts = dbContexts;

            var contexts = dbContexts.Values.ToArray();
            if (contexts.Length > 1)
                throw new DataAccessException("UseGlobalTransaction");
            // todo: средствами ef6 нельзя объединить dbContext с разными sqlConnection в одной транзакции

            for (var i = 0; i < contexts.Length; i++)
            {
                contexts[i].Configuration.AutoDetectChangesEnabled = true;
                if (i == 0)
                    _dbtransaction = contexts[i].Database.BeginTransaction(level);
                else
                    contexts[i].Database.UseTransaction(_dbtransaction.UnderlyingTransaction);
            }

            return this;
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            _disposed = true;

            if (disposing)
            {
                if (!_commited)
                    _dbtransaction.Rollback();

                _dbtransaction.Dispose();
                foreach (var dbContext in _dbContexts.Values)
                    dbContext.Configuration.AutoDetectChangesEnabled = false;
            }

            base.Dispose(disposing);
        }

        public void Commit()
        {
            _dbtransaction.Commit();
            _commited = true;
        }
    }
}