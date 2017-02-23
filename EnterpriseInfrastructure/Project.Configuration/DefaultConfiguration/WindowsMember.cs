using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Authentication;
using System.Security.Principal;
using System.Threading;
using Project.BusinessLogic.UserHandlers;
using Project.Common.Extensions;
using Project.Common.InternalContracts.Authentication;
using Project.Framework.BusinessLogic;
using Project.Framework.CrossCuttingConcerns.Identity;

namespace Project.Configuration.DefaultConfiguration
{
    public class WindowsMember : IModelMember<UserIdentityModel>
    {
        public WindowsMember(HandlerProcessorFactory processorFactory)
        {
            var windowsIdentity = Thread.CurrentPrincipal.Identity as WindowsIdentity;
            if (windowsIdentity == null)
                throw new AuthenticationException();

            var user = new UserIdentityModel
            {
                Login = CommonExtensions.GetPrincipalNameWithoutDomain(windowsIdentity.Name),
                AuthenticationType = windowsIdentity.AuthenticationType
            };

            if (string.IsNullOrWhiteSpace(user.Login))
                throw new AuthenticationException();

#if DEBUG
            using (var ctx = new PrincipalContext(ContextType.Machine))
#else
            using (var ctx = new PrincipalContext(ContextType.Domain))
#endif
            {
                var up = UserPrincipal.FindByIdentity(ctx, windowsIdentity.Name);
                user.Name = up?.DisplayName;
                user.Email = up?.EmailAddress;
            }

            Model = processorFactory.Get<UserIdentityLoginHandler>().Process(h => h.Handle(user));
            Roles = windowsIdentity.Groups?
                .Select(g => CommonExtensions.GetPrincipalNameWithoutDomain(g.Translate(typeof(NTAccount)).ToString()))
                .ToArray() ?? new string[0];
        }

        public UserIdentityModel Model { get; }

        public int Id => Model.Id;
        public string Login => Model.Login;
        public string Name => Model.Name;
        public string[] Roles { get; }
    }
}