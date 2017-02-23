using System.Linq;
using Project.Common.InternalContracts.Authentication;
using Project.DataAccess.Database1.Contracts;
using Project.Framework.BusinessLogic;

namespace Project.BusinessLogic.UserHandlers
{
    public class GetUsersHandler : HandlerBase
    {
        public UserIdentityModel[] Handle()
        {
            return GetRepository<IUserRepository>().Query().Select(u => new UserIdentityModel
            {
                Id = u.Id,
                Login = u.Login,
                Name = u.Name,
                Email = u.Email,
                Lang = u.Lang
            }).ToArray();
        }
    }
}