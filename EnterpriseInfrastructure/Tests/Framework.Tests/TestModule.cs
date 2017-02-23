using System.Data.Entity;
using System.Linq;
using Project.Common.InternalContracts.Authentication;
using Project.Configuration;
using Project.DataAccess.Database1.Models;
using Project.Framework.CrossCuttingConcerns.Identity;

namespace Framework.Tests
{
    public class TestModule : DefaultModule
    {
        public override void Load()
        {
            base.Load();
            KernelInstance.Bind<IMember>().To<Member>().InTransientScope();
        }

        private class Member : IMember
        {
            private readonly UserIdentityModel _user;
            public int Id => _user.Id;
            public string Login => _user.Login;
            public string Name => _user.Name;
            public string[] Roles { get; } = new string[0];

            public Member(DbContext context)
            {
                var user = context.Set<User>().FirstOrDefault(u => u.Login == "test@mail.ru" && u.Name == "Test User");
                _user = new UserIdentityModel { Id = user.Id, Login = user.Login, Name = user.Name };
            }
        }
    }
}