using Project.DataAccess.Database1.Contracts;
using Project.DataAccess.Database1.Models;
using Project.DataAccess.EntityFramework;

namespace Project.DataAccess.Database1.Repositories
{
    public class UserRepository : EntityFrameworkRepositoryBase<User, int>, IUserRepository
    {
        public UserRepository() : base(typeof(ProjectContext))
        { }

        public User Create(string login, string name, string email)
        {
            return new User
            {
                Login = login,
                Name = name,
                Email = email
            };
        }
    }
}