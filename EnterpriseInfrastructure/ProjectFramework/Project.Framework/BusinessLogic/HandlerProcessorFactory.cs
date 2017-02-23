using Project.Framework.Common.DependencyResolver;

namespace Project.Framework.BusinessLogic
{
	public sealed class HandlerProcessorFactory
	{
		private readonly IDependencyResolver _dependencyResolver;

		public HandlerProcessorFactory(IDependencyResolver dependencyResolver)
		{
			_dependencyResolver = dependencyResolver;
		}

		public HandlerProcessor<THandler> Get<THandler>() where THandler : HandlerBase
		{
			var handler = _dependencyResolver.Get<THandler>();
			return new HandlerProcessor<THandler>(handler, _dependencyResolver, this);
		}

		public HandlerProcessor<THandler> Get<THandler>(HandlerContext handlerContext)
			where THandler : HandlerBase
		{
			var handler = _dependencyResolver.Get<THandler>();
			return new HandlerProcessor<THandler>(handler, _dependencyResolver, this, handlerContext);
		}
	}
}