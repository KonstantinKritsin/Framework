using System;

// ReSharper disable UnusedMember.Global

namespace Project.Framework.Common.DependencyResolver
{
    public interface IDependencyResolver : IDisposable
    {
        void Release(object instance);
        TValue Get<TValue>(string name = null) where TValue : class;
        TValue TryGet<TValue>(string name = null) where TValue : class;
        TValue Get<TValue>(ConstructorArgument[] arguments, string name = null) where TValue : class;
        object Get(Type type, string name = null);
    }
}