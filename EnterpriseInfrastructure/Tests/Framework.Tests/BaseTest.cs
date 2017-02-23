using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Project.DataAccess.Database1;
using Project.DataAccess.Database1.Migrations;
using Project.DataAccess.Database1.Models;
using Project.Framework.CrossCuttingConcerns;

namespace Framework.Tests
{
    [TestClass]
    public abstract class BaseTest
    {
        protected static IKernel Kernel;


        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            var kernel = new StandardKernel();
            kernel.Load(new TestModule());
            kernel.Get<AC>();

            var dbContext = kernel.Get<ProjectContext>();
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ProjectContext, Configuration>());
            dbContext.Database.Initialize(false);

            var user = new User
            {
                Login = "wssvsm@vsm.s7",
                Name = "Sharepoint VSM",
                Email = ""
            };
            dbContext.Set<User>().Add(user);
            dbContext.SaveChanges();

            Kernel = kernel;
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            var dbContext = Kernel.Get<ProjectContext>();

            dbContext.Database.ExecuteSqlCommand($"delete from dbo.[User]");
            Kernel.Dispose();
        }
    }
}