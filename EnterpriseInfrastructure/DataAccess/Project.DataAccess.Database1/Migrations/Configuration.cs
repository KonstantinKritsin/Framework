using Project.DataAccess.Database1.Models;
using Project.Framework.CrossCuttingConcerns;

namespace Project.DataAccess.Database1.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<ProjectContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProjectContext context)
        {
            context.Set<User>().AddOrUpdate(u => u.Login, new User
            {
                Login = "login",
                Email = "email@mail.ru",
                Name = "name",
                Lang = "en",
                CreatedAt = AC.Inst.Offset.Now,
                LastActivityDate = AC.Inst.Offset.Now
            });
        }
    }
}
