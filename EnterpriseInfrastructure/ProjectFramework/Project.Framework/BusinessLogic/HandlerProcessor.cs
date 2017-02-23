using System;
using System.Threading.Tasks;
using Project.Framework.Common.Context;
using Project.Framework.Common.DependencyResolver;
using Project.Framework.Common.Exceptions;
using Project.Framework.CrossCuttingConcerns;

namespace Project.Framework.BusinessLogic
{
	public class HandlerProcessor<THandler> where THandler : HandlerBase
	{
		private readonly IDependencyResolver _dependencyResolver;
		private readonly THandler _handler;
		private readonly HandlerContext _parentContext;
		private readonly HandlerProcessorFactory _handlerProcessorFactory;

		internal HandlerProcessor(THandler handler, IDependencyResolver dependencyResolver, HandlerProcessorFactory handlerProcessorFactory)
		{
			_handler = handler;
			_dependencyResolver = dependencyResolver;
			_handlerProcessorFactory = handlerProcessorFactory;
		}

		internal HandlerProcessor(THandler handler, IDependencyResolver dependencyResolver, HandlerProcessorFactory handlerProcessorFactory, HandlerContext parentContext)
		{
			if(parentContext == null)
				throw new ParentContextIsNullException();

			_handler = handler;
			_dependencyResolver = dependencyResolver;
			_handlerProcessorFactory = handlerProcessorFactory;
			_parentContext = parentContext;
		}

		public TResult Process<TResult>(Func<THandler, TResult> func)
		{
			if (_parentContext != null)
				return ProcessInternalInParentScope(func);

			return ProcessInternalNewScope(func);
		}

		public void Process(Action<THandler> func)
		{
			Process(h =>
			{
				func(h);
				return 0;
			});
		}

		public Task<TResult> ProcessAsync<TResult>(Func<THandler, Task<TResult>> func)
		{
			if (_parentContext != null)
				return ProcessInternalInParentScope(func);

			return ProcessInternalNewScope(func);
		}

		public Task ProcessAsync(Func<THandler, Task> func)
		{
			if (_parentContext != null)
				return ProcessInternalInParentScope(func);

			return ProcessInternalNewScope(func);
		}

		private Task ProcessInternalNewScope(Func<THandler, Task> func)
		{
			return ProcessInternalNewScope(async h =>
			{
				await func(h);
				return 0;
			});
		}

		private async Task<TResult> ProcessInternalNewScope<TResult>(Func<THandler, Task<TResult>> func)
		{
			IUnitOfWork unitOfWork = null;
			try
			{
				LogMessage("StartOfTransactionWithType", _handler.GetType());

				unitOfWork = _dependencyResolver.Get<IUnitOfWork>();

				_handler.InitializeContext(new HandlerContext(_handlerProcessorFactory, unitOfWork));

				var result = await func(_handler);

				LogMessage("BusinessTransactionSuccessfullyCompleted", _handler.GetType());

				return result;
			}
			catch (Exception ex)
			{
				LogError("ErrorWhileExecutingBusinessTransaction", ex);
				throw;
			}
			finally
			{
				unitOfWork?.Dispose();

				LogMessage("CompletingTransactionWithType", _handler.GetType());
			}
		}

		private TResult ProcessInternalNewScope<TResult>(Func<THandler, TResult> func)
		{
			IUnitOfWork unitOfWork = null;
			try
			{
				LogMessage("StartOfTransactionWithType", _handler.GetType());

				unitOfWork = _dependencyResolver.Get<IUnitOfWork>();

				_handler.InitializeContext(new HandlerContext(_handlerProcessorFactory, unitOfWork));

				var result = func(_handler);

				LogMessage("BusinessTransactionSuccessfullyCompleted", _handler.GetType());

				return result;
			}
			catch (Exception ex)
			{
				LogError("ErrorWhileExecutingBusinessTransaction", ex);
				throw;
			}
			finally
			{
				unitOfWork?.Dispose();

				LogMessage("CompletingTransactionWithType", _handler.GetType());
			}
		}

		private TResult ProcessInternalInParentScope<TResult>(Func<THandler, TResult> func)
		{
			try
			{
				LogMessage("StartWithOutTransactionWithType", _handler.GetType());

				_handler.InitializeContext(_parentContext);

				var result = func(_handler);

				LogMessage("MethodProcessingCompleted", _handler.GetType());

				return result;
			}
			catch (Exception ex)
			{
				LogError("ErrorWhileExecutingType", ex);
				throw;
			}
		}

		private static void LogMessage(string template, params object[] args)
		{
			//AC.Inst.Log.DebugFormat(template, args);
		}

		private static void LogError(string template, Exception ex)
		{
			AC.Inst.Log.Error(template, ex);
		}
	}
}