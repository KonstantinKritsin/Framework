using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Project.DataAccess.Database1.Models;
using Project.DataAccess.EntityFramework;

namespace Project.DataAccess.Database1
{
    public class ProjectContext : DefaultDbContext
    {
        public ProjectContext() : this(nameof(ProjectContext))
        { }

        public ProjectContext(string configuration) : base(configuration)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();


            modelBuilder.Entity<User>().ToTable(nameof(User));
        }
    }
}
