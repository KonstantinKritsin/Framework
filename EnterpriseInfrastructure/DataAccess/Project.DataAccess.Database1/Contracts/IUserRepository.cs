using Project.DataAccess.Database1.Models;
using Project.Framework.DataAccess.Data;

namespace Project.DataAccess.Database1.Contracts
{
    public interface IUserRepository : IDataAccessor<User, int>
    {
        User Create(string login, string name, string email);
    }
}