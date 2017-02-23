using System;
using System.Collections.Generic;
using System.Linq;
using Project.Framework.Common.DependencyResolver;

namespace Project.Framework.Common.Context
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        internal readonly IDictionary<Type, Context> InternalContexts = new Dictionary<Type, Context>();

        protected readonly IDependencyResolver DependencyResolver;
        protected IDictionary<Type, Context> Contexts => InternalContexts;

        protected UnitOfWork(IDependencyResolver dependencyResolver)
        {
            DependencyResolver = dependencyResolver;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var context in InternalContexts.Values.ToList()) context.Dispose();
                InternalContexts.Clear();
            }
        }

        public bool In<TContext>() where TContext : Context
        {
            return InternalContexts.ContainsKey(typeof (TContext));
        }

        public TContext Get<TContext>() where TContext : Context
        {
            var context = DependencyResolver.Get<TContext>();
            context.SetUnitOfWork(this);
            InternalContexts.Add(typeof(TContext), context);
            return context;
        }
    }
}