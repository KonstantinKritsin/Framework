using System.Data.Entity;
using Project.DataAccess.Database1.Files.Models;
using Project.DataAccess.EntityFramework;

namespace Project.DataAccess.Database1.Files
{
    public class ProjectFilesContext : DefaultDbContext
    {
        public ProjectFilesContext() : this(nameof(ProjectFilesContext))
        { }

        public ProjectFilesContext(string configuration) : base(configuration)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BinaryMeta>().ToTable(nameof(BinaryMeta));

            modelBuilder.Entity<File1MData>().ToTable(nameof(File1MData));

            modelBuilder.Entity<FileStreamData>().ToTable(nameof(FileStreamData));
        }
    }
}
