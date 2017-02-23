using System;

namespace Project.Framework.Common.Context
{
    public abstract class Context : IDisposable
    {
        private UnitOfWork _uow;

        internal void SetUnitOfWork(UnitOfWork uow)
        {
            _uow = uow;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _uow.InternalContexts.Remove(GetType());
            }
        }
    }

    public abstract class Context<T> : Context
    {
        public abstract Context<T> Set(T p1);
    }

    public abstract class Context<T1, T2> : Context
    {
        public abstract Context<T1,T2> Set(T1 p1, T2 p2);
    }

    // and so for T3,T4...
}