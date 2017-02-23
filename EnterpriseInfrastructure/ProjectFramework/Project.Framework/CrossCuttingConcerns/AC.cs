using System;
using Project.Framework.Common.DateTime;
using Project.Framework.Common.DependencyResolver;
using Project.Framework.Common.Guid;
using Project.Framework.CrossCuttingConcerns.Caching;
using Project.Framework.CrossCuttingConcerns.Identity;
using Project.Framework.CrossCuttingConcerns.Logging;

// ReSharper disable UnusedMember.Global

namespace Project.Framework.CrossCuttingConcerns
{
	public sealed class AC : IDisposable
    {
        private readonly IDependencyResolver _dependencyResolver;

        private static volatile AC _current;
        private static readonly object Lock = new object();

		private bool _disposed;

		public AC(IDependencyResolver dependencyResolver)
        {
			_dependencyResolver = dependencyResolver;
            Inst = this;
        }

        public static AC Inst
        {
            get
            {
                if (_current == null)
                {
                    throw new InvalidOperationException("Context is not inited");
                }
                if (_current._disposed)
                {
                    throw new InvalidOperationException("Context was disposed");
                }
                return _current;
            }
            private set
            {
	            if (_current != null)
					return;

	            lock (Lock)
		            if (_current == null)
			            _current = value;
            }
        }

        public ICache Cache => _dependencyResolver.Get<ICache>();
	    public ILogger Log => _dependencyResolver.Get<ILogger>();
	    public IDateTimeProvider DateTime => _dependencyResolver.Get<IDateTimeProvider>();
	    public IDateTimeOffsetProvider Offset => _dependencyResolver.Get<IDateTimeOffsetProvider>();
	    public IGuidProvider Guid => _dependencyResolver.Get<IGuidProvider>();
		public IPrincipal<IMember> Principal => _dependencyResolver.Get<IPrincipal<IMember>>();
		public T GetPrincipal<T>() where T : IPrincipal<IMember> => (T)Principal;

		public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~AC()
        {
            Dispose(false);
        }
        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
				_dependencyResolver?.Dispose();
            
            _current = null;
            _disposed = true;
        }
    }
}