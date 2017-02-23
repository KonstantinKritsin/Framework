using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Project.Common.InternalContracts.Authentication;
using Project.Configuration;
using Project.Configuration.DefaultConfiguration;
using Project.Endpoint.Web;
using Project.Framework.CrossCuttingConcerns.Identity;
using WebActivatorEx;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: ApplicationShutdownMethod(typeof(NinjectWebCommon), "Stop")]


namespace Project.Endpoint.Web
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = KernelInit.GetKernel<WebApplicationModule>();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                GlobalConfiguration.Configuration.DependencyResolver = new Ninject.Web.WebApi.NinjectDependencyResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }
    }

    public class WebApplicationModule : DefaultModule
    {
        public override void Load()
        {
            base.Load();
            KernelInstance.Bind<IPrincipal, IPrincipal<IMember>>().To<ProjectPrincipal>().InRequestScope();
            KernelInstance.Bind<IMember>().To<WindowsMember>().When(r => Thread.CurrentPrincipal.Identity.GetType() == typeof(WindowsIdentity)).InRequestScope();
            KernelInstance.Bind<IMember>().To<ClaimMember>().When(r => Thread.CurrentPrincipal.Identity.GetType() == typeof(ClaimsIdentity)).InRequestScope();
        }
    }
}