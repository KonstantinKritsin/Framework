using System;

namespace Project.Framework.Common.Context
{
	public interface IUnitOfWork : IDisposable
	{
	    bool In<TContext>() where TContext : Context;
        TContext Get<TContext>() where TContext : Context;
	}
}