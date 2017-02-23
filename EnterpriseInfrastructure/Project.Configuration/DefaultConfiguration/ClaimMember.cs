using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading;
using Project.BusinessLogic.UserHandlers;
using Project.Common.Extensions;
using Project.Common.InternalContracts.Authentication;
using Project.Framework.BusinessLogic;
using Project.Framework.CrossCuttingConcerns.Identity;

namespace Project.Configuration.DefaultConfiguration
{
    public class ClaimMember : IModelMember<UserIdentityModel>
    {
        public ClaimMember(HandlerProcessorFactory processorFactory)
        {
            var claimIdentity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
            if (claimIdentity == null)
                throw new AuthenticationException();

            var user = new UserIdentityModel
            {
                Login = CommonExtensions.GetPrincipalNameWithoutDomain(claimIdentity.Name),
                AuthenticationType = claimIdentity.AuthenticationType
            };

            if (string.IsNullOrWhiteSpace(user.Login))
                throw new AuthenticationException();

            var commonNameClaim = claimIdentity.Claims.FirstOrDefault(_ => _.Type == "http://schemas.xmlsoap.org/claims/CommonName");
            user.Name = commonNameClaim?.Value;

            var emailClaim = claimIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            user.Email = emailClaim?.Value;

            Model = processorFactory.Get<UserIdentityLoginHandler>().Process(h => h.Handle(user));
            Roles = claimIdentity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToArray();
        }

        public UserIdentityModel Model { get; }

        public int Id => Model.Id;
        public string Login => Model.Login;

        public string Name => Model.Name;
        public string[] Roles { get; }
    }
}