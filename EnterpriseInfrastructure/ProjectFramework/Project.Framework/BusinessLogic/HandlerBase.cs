using System;
using System.Threading.Tasks;
using Project.Framework.Common.Context;
using Project.Framework.Common.Enums;
using Project.Framework.Common.Exceptions;
using Project.Framework.DataAccess.Data;

namespace Project.Framework.BusinessLogic
{
    public abstract class HandlerBase
	{
		private HandlerContext _context;
		private bool _inTransaction;

		protected IDisposable Transaction(IsolationLevel level = IsolationLevel.ReadCommitted)
		{
		    var dbUow = _context.CurrentUnitOfWork as IDbUnitOfWork;
            if(dbUow == null)
                return new Disposable();
			if (dbUow.InTransaction() || dbUow.InGlobalTransaction())
				return new Disposable();

			_inTransaction = true;
			return dbUow.BeginTransaction(level);
		}

		protected IDisposable GlobalTransaction(IsolationLevel level = IsolationLevel.ReadCommitted)
		{
            var dbUow = _context.CurrentUnitOfWork as IDbUnitOfWork;
            if (dbUow == null)
                return new Disposable();

            if (dbUow.InGlobalTransaction())
				return new Disposable();

			_inTransaction = true;
			return dbUow.BegingGlobalTransaction(level);
		}

	    protected T Context<T>() where T : Context
	    {
	        return _context.CurrentUnitOfWork.Get<T>();
	    }

		internal void InitializeContext(HandlerContext context)
		{
			_context = context;
		}

		protected HandlerProcessor<THandler> GetProcessor<THandler>(bool useNewTransactionContext = false)
			where THandler : HandlerBase
		{
            var dbUow = _context.CurrentUnitOfWork as IDbUnitOfWork;
            if (useNewTransactionContext && dbUow?.InGlobalTransaction() == true)
				throw new DataAccessException("CannotStartNewTransactionContextInGlobalTransaction");

			return useNewTransactionContext
				? _context.HandlerProcessorFactory.Get<THandler>()
				: _context.HandlerProcessorFactory.Get<THandler>(_context);
		}

		protected T GetRepository<T>() where T : class, IRepository
		{
            var dbUow = _context.CurrentUnitOfWork as IDbUnitOfWork;
            if(dbUow == null)
                throw new DataAccessException("Db context is not supported by UOW.");
            return dbUow.GetRepository<T>();
		}

		protected Task CommitAsync()
		{
			return _inTransaction ? ((IDbUnitOfWork)_context.CurrentUnitOfWork).CommitAsync() : Task.CompletedTask;
		}

		protected void Commit()
		{
			if(_inTransaction) ((IDbUnitOfWork)_context.CurrentUnitOfWork).Commit();
		}

		private class Disposable : IDisposable
		{
			public void Dispose() { }
		}
	}
}