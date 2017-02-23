using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Transactions;
using Project.Framework.Common.Context;
using Project.Framework.DataAccess.Data;

namespace Project.DataAccess.EntityFramework
{
    internal class GlobalTransaction : Context<IDictionary<Type, DbContext>, IsolationLevel>, ITransaction
    {
        private IDictionary<Type, DbContext> _dbContexts;
        private TransactionScope _transactionScope;

        public override Context<IDictionary<Type, DbContext>, IsolationLevel> Set(IDictionary<Type, DbContext> dbContexts, IsolationLevel level)
        {
            foreach (var dbContext in dbContexts.Values)
                dbContext.Configuration.AutoDetectChangesEnabled = true;

            _dbContexts = dbContexts;

            _transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions
            {
                IsolationLevel = level
            }, TransactionScopeAsyncFlowOption.Enabled);
            return this;
        }

        public void Commit()
        {
            _transactionScope.Complete();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _transactionScope.Dispose();
                foreach (var dbContext in _dbContexts.Values)
                    dbContext.Configuration.AutoDetectChangesEnabled = false;
            }
            base.Dispose(disposing);
        }
    }
}