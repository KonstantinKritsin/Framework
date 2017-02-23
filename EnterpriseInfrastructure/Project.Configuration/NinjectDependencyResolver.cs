using System;
using System.Linq;
using Ninject;
using Ninject.Parameters;
using Project.Framework.Common.DependencyResolver;

namespace Project.Configuration
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }

        public void Dispose()
        {
            _kernel.Dispose();
        }

        public void Release(object instance)
        {
            _kernel.Release(instance);
        }

        public TValue Get<TValue>(string name = null) where TValue : class
        {
            return _kernel.Get<TValue>(name);
        }

        public TValue TryGet<TValue>(string name = null) where TValue : class
        {
            var request = _kernel.CreateRequest(typeof(TValue), b => b.Name == name, new IParameter[0], true, true);
            return _kernel.Resolve(request).Cast<TValue>().FirstOrDefault();
        }

        public TValue Get<TValue>(Framework.Common.DependencyResolver.ConstructorArgument[] arguments, string name = null) where TValue : class
        {
            return _kernel.Get<TValue>(name, arguments.Select(a => (IParameter)new Ninject.Parameters.ConstructorArgument(a.Name, a.Value)).ToArray());
        }

        public object Get(Type type, string name = null)
        {
            return _kernel.Get(type, name);
        }
    }
}