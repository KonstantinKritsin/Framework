using Project.Framework.Common.Context;

namespace Project.Framework.BusinessLogic
{
	public class HandlerContext
	{
		internal HandlerProcessorFactory HandlerProcessorFactory { get; }
		internal IUnitOfWork CurrentUnitOfWork { get; }

		public HandlerContext(HandlerProcessorFactory handlerProcessorFactory, IUnitOfWork currentUnitOfWork)
		{
			HandlerProcessorFactory = handlerProcessorFactory;
			CurrentUnitOfWork = currentUnitOfWork;
		}
	}
}