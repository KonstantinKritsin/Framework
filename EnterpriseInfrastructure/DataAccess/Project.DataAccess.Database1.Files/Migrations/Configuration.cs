using System.Data.Entity.Migrations;
namespace Project.DataAccess.Database1.Files.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<ProjectFilesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}
