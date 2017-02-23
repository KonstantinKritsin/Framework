using System.Linq;
using System.Security.Authentication;
using Project.Common.InternalContracts.Authentication;
using Project.DataAccess.Database1.Contracts;
using Project.DataAccess.Database1.Models;
using Project.Framework.BusinessLogic;
using Project.Framework.CrossCuttingConcerns;

namespace Project.BusinessLogic.UserHandlers
{
    public class UserIdentityLoginHandler : HandlerBase
    {
        public UserIdentityModel Handle(UserIdentityModel model)
        {
            if (string.IsNullOrWhiteSpace(model?.Login))
                throw new AuthenticationException();

            var userRepo = GetRepository<IUserRepository>();
            var userDb = userRepo.Query().FirstOrDefault(u => u.Login == model.Login) ?? userRepo.Create(model.Login, null, null);

            Update(userDb, model);

            return model;
        }

        private void Update(User userDb, UserIdentityModel model)
        {
            var isNew = userDb.Id == 0;

            if (isNew && string.IsNullOrWhiteSpace(model.Name))
                model.Name = model.Login;

            if (!string.IsNullOrWhiteSpace(model.Name) && userDb.Name != model.Name)
                userDb.Name = model.Name;


            if (string.IsNullOrWhiteSpace(model.Email))
                model.Email = null;

            if (userDb.Email != model.Email)
                userDb.Email = model.Email;

            using (Transaction())
            {
                userDb.LastActivityDate = AC.Inst.Offset.Now;

                if (isNew)
                    GetRepository<IUserRepository>().Insert(userDb);
                else
                    GetRepository<IUserRepository>().Update(userDb);

                Commit();
            }

            model.Id = userDb.Id;
            model.Lang = userDb.Lang;
        }
    }
}