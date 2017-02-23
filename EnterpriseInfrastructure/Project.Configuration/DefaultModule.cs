using System.Data.Entity;
using Ninject.Modules;
using Project.Configuration.DefaultConfiguration;
using Project.DataAccess.Database1;
using Project.DataAccess.Database1.Contracts;
using Project.DataAccess.Database1.Files;
using Project.DataAccess.Database1.Files.Contracts;
using Project.DataAccess.Database1.Files.Repositories;
using Project.DataAccess.Database1.Repositories;
using Project.DataAccess.EntityFramework;
using Project.Framework.Common.Context;
using Project.Framework.Common.DateTime;
using Project.Framework.Common.DependencyResolver;
using Project.Framework.Common.Guid;
using Project.Framework.Common.JsonConverter;
using Project.Framework.CrossCuttingConcerns;
using Project.Framework.CrossCuttingConcerns.Caching;
using Project.Framework.CrossCuttingConcerns.Logging;
using Project.Framework.DataAccess.Data;

namespace Project.Configuration
{
    public class DefaultModule : NinjectModule
    {
        public override void Load()
        {
            BindAmbientObjects();
            BindRepositories();
            ConfigureBusinessLayerInfrastructure();
        }

        private void BindAmbientObjects()
        {
            KernelInstance.Bind<IDependencyResolver>().To<NinjectDependencyResolver>().InSingletonScope();
            KernelInstance.Bind<IUnitOfWork, IDbUnitOfWork>().To<EntityFrameworkUnitOfWork>().InTransientScope();
            KernelInstance.Bind<DbContext>().To<ProjectContext>().InTransientScope().WithConstructorArgument("configuration", nameof(ProjectContext));
            KernelInstance.Bind<ProjectContext>().ToSelf().InTransientScope().WithConstructorArgument("configuration", nameof(ProjectContext));
            KernelInstance.Bind<ProjectFilesContext>().ToSelf().InTransientScope().WithConstructorArgument("configuration", nameof(ProjectFilesContext));
            KernelInstance.Bind<ILogger>().To<TraceLog>().InSingletonScope();
            KernelInstance.Bind<ICache>().To<InMemoryCache>().InSingletonScope();
            KernelInstance.Bind<IGuidProvider>().To<GuidProvider>().InSingletonScope();
            KernelInstance.Bind<IDateTimeProvider>().To<LocalDateTimeProvider>().InSingletonScope();
            KernelInstance.Bind<IDateTimeOffsetProvider>().To<LocalDateTimeOffsetProvider>().InSingletonScope();
            KernelInstance.Bind<AC>().ToSelf().InSingletonScope();
        }

        private void BindRepositories()
        {
            KernelInstance.Bind<IBinaryMetaRepository>().To<BinaryMetaRepository>().InTransientScope();
            KernelInstance.Bind<IUserRepository>().To<UserRepository>().InTransientScope();
        }

        private void ConfigureBusinessLayerInfrastructure()
        {
            KernelInstance.Bind<IJsonConverter>().To<JsonConverter>().InSingletonScope();
        }
    }
}