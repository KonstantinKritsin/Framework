using System.Data.Entity;
using Ninject;
using Ninject.Modules;
using Project.DataAccess.Database1;
using Project.DataAccess.Database1.Files;
using Project.Framework.CrossCuttingConcerns;

namespace Project.Configuration
{
    public static class KernelInit
    {
        private static readonly object Lock = new object();
        private static volatile IKernel _kernel;

        public static IKernel GetKernel<T>() where T : NinjectModule, new()
        {
            if (_kernel == null)
                lock (Lock)
                    if (_kernel == null)
                    {
                        _kernel = new StandardKernel();
                        try
                        {
                            _kernel.Load(new T());
                            _kernel.Get<AC>();
                            var dbContext = _kernel.Get<ProjectContext>();
                            var dbFileContext = _kernel.Get<ProjectFilesContext>();
                            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ProjectContext, DataAccess.Database1.Migrations.Configuration>());
                            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ProjectFilesContext, DataAccess.Database1.Files.Migrations.Configuration>());
                            dbContext.Database.Initialize(false);
                            dbFileContext.Database.Initialize(false);
                        }
                        catch
                        {
                            _kernel.Dispose();
                            throw;
                        }
                    }
            return _kernel;
        }
    }
}